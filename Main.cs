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
        private bool scrollClicked = false;
        private const int WM_NCHITTEST = 0x84;
        // MOVE THE WINDOW AROUND METHOD
        protected override void WndProc(ref Message message) {
            base.WndProc(ref message);
            if (message.Msg == WM_NCHITTEST && (int)message.Result == HTCLIENT)
                message.Result = (IntPtr)HTCAPTION;
        }
        public Main() {
            InitializeComponent();
            player = new Player(this);
            return;
        }
        protected override void OnPaint(PaintEventArgs e) {
            base.OnPaint(e);
            if (player.requestNextSong) {
                player.requestNextSong = false;
                NextBtn.PerformClick();
                return;
            } else if(player.requestRepeatSong) {
                player.requestRepeatSong = false;
                player.repeat();
                return;
            }
            return;
        }
        private String split_path(string name) {
            if (!name.Contains(Path.DirectorySeparatorChar)) return name;
            string[] parts = name.Split(Path.DirectorySeparatorChar);
            return parts[parts.Length - 1];
        }
        private void AddBtn_Click(object sender, EventArgs e) {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "MP3-Files|*.mp3|WAV-Files|*.wav";
            DialogResult R = ofd.ShowDialog();
            if (R == DialogResult.OK) {
                if (player.playlist.Count <= 0) {
                    playpanel.Show();
                }
                string fname = split_path(ofd.FileName);
                Song song = new Song(ofd.FileName, fname);
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
                SongTrackBar.Value = 0;
                player.open(ofd.FileName);
                player.playingSong = song;
                player.playingIndex = player.playlist.Count - 1;
                song.setDuration(player.getLength(ofd.FileName));
                songlist.Items.Add(location + ". " + song.duration_string + " | " + fname);
                songlist.SelectedIndex = location - 1;
                SongTrackBar.Maximum = (int)song.duration;
                endTrackLbl.Text = song.duration_string;
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
            SongTrackBar.Value = 0;
            player.playingSong = song;
            player.playingIndex = songlist.SelectedIndex;
            endTrackLbl.Text = song.duration_string;
            SongTrackBar.Maximum = (int)song.duration;
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
                if (player.playingSong.ended) {
                    player.repeat();
                } else {
                    player.play();
                }
                PlayBtn.Text = "⏹ Stop";
                SongPlayLbl.Text = player.playingSong.name;
            }
            return;
        }
        private void NextBtn_Click(object sender, EventArgs e) {
            if (player.playingSong == null) return;
            if (player.next()) {
                SongTrackBar.Value = 0;
                PlayBtn.Text = "⏹ Stop";
                SongPlayLbl.Text = player.playingSong.name;
                songlist.SelectedIndex = player.playingIndex;
                endTrackLbl.Text = player.playingSong.duration_string;
                SongTrackBar.Maximum = (int)player.playingSong.duration;
            }
            return;
        }
        private void PrevBtn_Click(object sender, EventArgs e) {
            if (player.playingSong == null) return;
            if (player.prev()) { 
                SongTrackBar.Value = 0;
                PlayBtn.Text = "⏹ Stop";
                SongPlayLbl.Text = player.playingSong.name;
                songlist.SelectedIndex = player.playingIndex;
                endTrackLbl.Text = player.playingSong.duration_string;
                SongTrackBar.Maximum = (int)player.playingSong.duration;
            }
            return;
        }
        private void button2_Click(object sender, EventArgs e) {
            DialogResult RESULT = MessageBox.Show("Are you sure you want to clear the playlist?", "Clear the playlist...", MessageBoxButtons.YesNo);
            if (RESULT == DialogResult.Yes) {
                player.stop();
                player.clear();
                playpanel.Hide();
                endTrackLbl.Text = "";
                songlist.Items.Clear();
                player.playlist.Clear();
                player.playingIndex = 0;
                player.playingSong = null;
                SongTrackBar.Maximum = (int)0;
            }
            return;
        }
        private void volumeBar_Scroll(object sender, EventArgs e) {
            int volume = volumeBar.Value;
            player.songVolume = volume;
            player.setVolume(volume);
            soundLbl.Text = (volume / 2) + "%";
            return;
        }
        private void SongTrackBar_Scroll(object sender, EventArgs e) {
            scrollClicked = true;
            int value = SongTrackBar.Value;
            minLabel.Text = translateMs(value);
        }
        private void SongTrackBar_MouseUp(object sender, MouseEventArgs e) {
            scrollClicked = false;
        }
        private void SongTrackBar_ValueChanged(object sender, EventArgs e) {
            if (!scrollClicked || player.playingSong == null) return;
            int value = SongTrackBar.Value;
            if ((value - 5) == (int)(player.playingPosition)) {
                return;
            }
            player.seekTo(value);
            scrollClicked = false;
            SongPlayLbl.Text = "(" + translateMs(value) + "/" + player.playingSong.duration_string + ") " + player.playingSong.name;
            return;
        }
        private void SongTrackBar_MouseMove(object sender, MouseEventArgs e) {
            if (!scrollClicked || player.playingSong == null) return;
            moveMinLabel(e.X);
            return;
        }
        public static string translateMs(long ms) {
            TimeSpan t = TimeSpan.FromMilliseconds(ms);
            if (t.Hours > 0) return $"{t.Hours}:{t.Minutes}:{t.Seconds}s";
            else if (t.Minutes > 0) return $"{t.Minutes}:{t.Seconds}s";
            else if (t.Seconds > 0) return $"00:{t.Seconds}s";
            else return $"{t.Milliseconds}s";
        }
        private void moveMinLabel(int X) {
            minLabel.Location = new Point(X, minLabel.Location.Y);
            return;
        }
        class Song {
            public string path;
            public string name;
            public long duration;
            public long position = 0;
            public bool ended = false;
            public bool paused = false;
            public string? duration_string;

            public Song(string path, string name) {
                this.path = path;
                this.name = name;
            }
            public void setDuration(long duration) {
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
            public bool requestNextSong = false;
            public bool requestRepeatSong = false;
            private static System.Timers.Timer TIMER = new System.Timers.Timer();
            public Dictionary<String, Song> playlist = new Dictionary<String, Song>();
            [DllImport("winmm.dll")]
            private static extern long mciSendString(string lpstrCommand, StringBuilder lpstrReturnString, int uReturnLength, int hwdCallBack);

            public Player(Main m) {
                start_timer();
                this.MAIN = m;
            }
            public int progress_x() {
                int value = MAIN.SongTrackBar.Value;
                long max = playingSong.duration;
                long progress = (value / max) * 100;
                int left = (int)progress - MAIN.SongTrackBar.Width;
                return 0;
            }
            public void stop_timer() {
                TIMER.Stop();
                return;
            }
            private void start_timer() {
                TIMER.Interval = 1000;
                TIMER.Elapsed += new System.Timers.ElapsedEventHandler(OnTimedEvent);
                TIMER.Enabled = true;
                TIMER.Start();
                return;
            }
            private async void OnTimedEvent(Object source, System.Timers.ElapsedEventArgs e) {
                if (playingSong == null || playingSong.paused || playingSong.ended) return;
                long length = playingSong.duration;
                long add = playingPosition + 1000;
                if (add >= length) {
                    playingSong.ended = true;
                    MAIN.SongTrackBar.Value = (int)length;
                    if ((playingIndex + 1) < playlist.Count) {
                        await Task.Delay(2000);
                        requestNextSong = true;
                        MAIN.Refresh();
                    } else if (MAIN.RepeatBtn.Checked) {
                        await Task.Delay(2000);
                        requestRepeatSong = true;
                        MAIN.Refresh();
                    } else {
                        MAIN.PlayBtn.Text = "⏵Play";
                    } 
                    playingPosition = 0;
                    MAIN.SongPlayLbl.Text = "(" + playingSong.duration_string + "/" + playingSong.duration_string + ") " + playingSong.name;
                    return;
                }
                playingPosition = add;
                if (!MAIN.scrollClicked) {
                    MAIN.moveMinLabel(84);
                    MAIN.SongTrackBar.Value = (int)playingPosition;
                    MAIN.minLabel.Text = translateMs(playingPosition);
                }
                MAIN.SongPlayLbl.Text = "(" + translateMs(playingPosition) + "/" + playingSong.duration_string + ") " + playingSong.name;
                return;
            }

            public void open(string File) {
                playingPosition = 0;
                string Format = @"open ""{0}"" type MPEGVideo alias MediaFile";
                string command = string.Format(Format, File);
                mciSendString(command, null, 0, 0);
                return;
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
                playingSong.ended = false;
                playingSong.paused = false;
                string command = "play MediaFile";
                mciSendString(command, null, 0, 0);
                MAIN.soundLbl.Text = (songVolume / 2) + "%";
                return;
            }
            public void stop() {
                if (playingSong == null) return;
                playingSong.paused = true;
                string command = "stop MediaFile";
                mciSendString(command, null, 0, 0);
                return;
            }
            public void clear() {
                if (playingSong == null) return;
                playingPosition = 0;
                playingSong.ended = false;
                string command = "close MediaFile";
                mciSendString(command, null, 0, 0);
                return;
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
                MAIN.songlist.SelectedIndex = playingIndex;
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
            public void repeat() {
                if (playingSong == null || !playingSong.ended) return;
                stop();
                clear();
                open(playingSong.path);
                play();
                return;
            }
            public void setVolume(int volume) {
                var command = "setaudio MediaFile volume to " + volume.ToString();
                mciSendString(command, null, 0, 0);
            }
            public void seekTo(int position) {
                if (playingSong == null) return;
                stop();
                clear();
                open(playingSong.path);
                mciSendString("seek MediaFile to " + position, null, 0, 0);
                playingPosition = (long)position; 
                play();
                return;
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
