using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows;
using System.IO;
using System.Runtime.InteropServices;

namespace SongPlayer_Project {
    public partial class Main : Form {
        private Player player;
        private const int HTCLIENT = 0x1;
        private const int HTCAPTION = 0x2;
        private const int WM_NCHITTEST = 0x84;
        // MOVE THE WINDOW AROUND METHOD
        protected override void WndProc(ref Message message)
        {
            base.WndProc(ref message);
            if (message.Msg == WM_NCHITTEST && (int)message.Result == HTCLIENT)
                message.Result = (IntPtr)HTCAPTION;
        }
        public Main() {
            InitializeComponent();
            player = new Player(this);
            return;
        }
        private String split_path(string name) {
            if (!name.Contains(Path.DirectorySeparatorChar)) return name;
            string[] parts = name.Split(Path.DirectorySeparatorChar);
            return parts[parts.Length - 1];
        }
        private void AddBtn_Click(object sender, EventArgs e) {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "MP3-Files|*.mp3";
            DialogResult R = ofd.ShowDialog();
            if (R == DialogResult.OK) {
                if (player.playlist.Count <= 0) {
                    playpanel.Show();
                }
                string fname = split_path(ofd.FileName);
                Song song = new Song(ofd.FileName, fname, player.getLength(ofd.FileName));
                try
                {
                    player.playlist.Add(fname, song);
                } catch(Exception EX) {
                    MessageBox.Show("Error: Song already exists in the playlist.");
                    return;
                }
                PlayBtn.Text = "⏹ Stop";
                SongPlayLbl.Text = fname;
                int location = player.playlist.Count;
                /////////////////////////////////////
                player.stop();
                player.clear();
                player.open(ofd.FileName);
                player.playingSong = song;
                player.playingIndex = player.playlist.Count - 1;
                songlist.Items.Add(location + ". " + song.duration_string + " | " + fname);
                songlist.SelectedIndex = location - 1;
                player.play();
                return;
            }
            return;
        }
        private void CloseBtn_Click(object sender, EventArgs e) {
            DialogResult RESULT = MessageBox.Show("Are you sure you want to close the application?", "Close the app...", MessageBoxButtons.YesNo);
            if (RESULT == DialogResult.Yes)
                Application.Exit();
                player.stop_timer();
                return;
        }
        private void MinimizeBtn_Click(object sender, EventArgs e) {
            this.WindowState = FormWindowState.Minimized;
            return;
        }
        private void songlist_SelectedIndexChanged(object sender, EventArgs e) {
            KeyValuePair<string, Song> songs = player.playlist.ElementAt(songlist.SelectedIndex);
            string fname = songs.Key;
            Song song = songs.Value;
            // CHECK IF SONG IS ALREADY PLAYING
            if (player.isPlaying(song)) {
                return;
            }
            PlayBtn.Text = "⏹ Stop";
            SongPlayLbl.Text = fname;
            /////////////////////////
            player.stop();
            player.clear();
            player.open(song.path);
            player.playingSong = song;
            player.playingIndex = songlist.SelectedIndex;
            player.play();
            return;
        }
        private void PlayBtn_Click(object sender, EventArgs e) {
            string bt = PlayBtn.Text;
            if (player.playingSong == null) { return; }
            if (bt.Equals("⏹ Stop")) {
                player.stop();
                PlayBtn.Text = "⏵Play";
                SongPlayLbl.Text += " (paused)";
            } else {
                player.play();
                PlayBtn.Text = "⏹ Stop";
                SongPlayLbl.Text = player.playingSong.name;
            }
            return;
        }
        private void NextBtn_Click(object sender, EventArgs e) {
            if (player.playingSong == null) return;
            if (player.next()) {
                PlayBtn.Text = "⏹ Stop";
                SongPlayLbl.Text = player.playingSong.name;
                songlist.SelectedIndex = player.playingIndex;
            }
            return;
        }
        private void PrevBtn_Click(object sender, EventArgs e) {
            if (player.playingSong == null) return;
            if (player.prev()) {
                PlayBtn.Text = "⏹ Stop";
                SongPlayLbl.Text = player.playingSong.name;
                songlist.SelectedIndex = player.playingIndex;
            }
            return;
        }
        private void button2_Click(object sender, EventArgs e) {
            DialogResult RESULT = MessageBox.Show("Are you sure you want to clear the playlist?", "Clear the playlist...", MessageBoxButtons.YesNo);
            if (RESULT == DialogResult.Yes) {
                player.stop();
                player.clear();
                playpanel.Hide();
                songlist.Items.Clear();
                player.playlist.Clear();
                player.playingIndex = 0;
                player.playingSong = null;
            }
            return;
        }
        private void volumeBar_Scroll(object sender, EventArgs e) {
            int volume = volumeBar.Value;
            player.songVolume = volume;
            player.setVolume(volume);
            return;
        }
        public static string translateMs(long ms) {
            TimeSpan t = TimeSpan.FromMilliseconds(ms);
            if (t.Hours > 0) return $"{t.Hours}:{t.Minutes}:{t.Seconds}s";
            else if (t.Minutes > 0) return $"{t.Minutes}:{t.Seconds}s";
            else if (t.Seconds > 0) return $"00:{t.Seconds}s";
            else return $"{t.Milliseconds}s";
        }
        class Song {
            public string path;
            public string name;
            public long duration;
            public long position = 0;
            public bool paused = false;
            public string duration_string;

            public Song(string path, string name, long duration) {
                this.path = path;
                this.name = name;
                this.duration = duration;
                this.duration_string = translateMs(duration);
            }
        }
        class Player {
            private Main MAIN;
            public Song? playingSong;
            public int songVolume = 200;
            public int playingIndex = 0;
            public long playingPosition = 0;
            private static System.Timers.Timer TIMER = new System.Timers.Timer();
            public Dictionary<String, Song> playlist = new Dictionary<String, Song>();
            [DllImport("winmm.dll")]
            private static extern long mciSendString(string lpstrCommand, StringBuilder lpstrReturnString, int uReturnLength, int hwdCallBack);

            public Player(Main m) {
                start_timer();
                this.MAIN = m;
            }
            public void stop_timer() {
                TIMER.Stop();
                return;
            }
            private void start_timer() {
                TIMER.Interval = 1000;
                TIMER.Elapsed += OnTimedEvent;
                TIMER.Enabled = true;
                TIMER.Start();
                return;
            }
            private void OnTimedEvent(Object source, System.Timers.ElapsedEventArgs e) {
                if (playingSong == null || playingSong.paused) return;
                long length = playingSong.duration;
                long add = playingPosition + 1000;
                if (add >= length) {
                    playingSong = null;
                    playingPosition = 0;
                    if (next()) { // PLAY THE NEXT SONG
                        MAIN.songlist.SelectedIndex = playingIndex;
                    }
                    return;
                }
                playingPosition = add;
                MAIN.SongPlayLbl.Text = "(" + translateMs(playingPosition) + "/" + playingSong.duration_string + ") " + playingSong.name;
                return;
            }

            public void open(string File) {
                playingPosition = 0;
                string Format = @"open ""{0}"" type MPEGVideo alias MediaFile";
                string command = string.Format(Format, File);
                mciSendString(command, null, 0, 0);
            }
            public bool isPlaying(Song S) {
                if (playingSong == null) {
                    return false;
                }
                if(S.name.Equals(playingSong.name)) {
                    return true;
                }
                return false;
            }
            public void play() {
                if (playingSong == null) return;
                setVolume(songVolume);
                playingSong.paused = false;
                string command = "play MediaFile";
                mciSendString(command, null, 0, 0);
            }
            public void stop() {
                if (playingSong == null) return;
                playingSong.paused = true;
                string command = "stop MediaFile";
                mciSendString(command, null, 0, 0);
            }
            public void clear() {
                playingPosition = 0;
                string command = "close MediaFile";
                mciSendString(command, null, 0, 0);
            }
            public bool next() {
                if ((playingIndex + 1) >= playlist.Count) {
                    MessageBox.Show("End of playlist :(");
                    return false;
                }
                KeyValuePair<string, Song> songs = playlist.ElementAt(playingIndex + 1);
                string fname = songs.Key;
                Song song = songs.Value;
                ////////////////////////
                stop();
                clear();
                open(song.path);
                playingSong = song;
                playingIndex = playingIndex + 1;
                play();
                return true;
            }
            public bool prev() {
                if ((playingIndex - 1) < 0) {
                    MessageBox.Show("End of playlist :(");
                    return false;
                }
                KeyValuePair<string, Song> songs = playlist.ElementAt(playingIndex - 1);
                string fname = songs.Key;
                Song song = songs.Value;
                ////////////////////////
                stop();
                clear();
                open(song.path);
                playingSong = song;
                playingIndex = playingIndex - 1;
                play();
                return true;
            }
            public void setVolume(int volume) {
                var command = "setaudio MediaFile volume to " + volume.ToString();
                mciSendString(command, null, 0, 0);
            }
            public long getLength(string File) {
                StringBuilder mssg = new StringBuilder(255);
                string Format = @"open ""{0}"" type MPEGVideo alias MediaFile";
                string command = string.Format(Format, File);
                mciSendString(command, null, 0, 0);
                mciSendString("set MediaFile time format ms", null, 0, 0);
                mciSendString("status MediaFile length", mssg, mssg.Capacity, 0);
                return UInt32.Parse(mssg.ToString());
            }
        }
    }
}
