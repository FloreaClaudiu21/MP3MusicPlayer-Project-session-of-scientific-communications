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
        Player player = new Player();
        private const int WM_NCHITTEST = 0x84;
        private const int HTCLIENT = 0x1;
        private const int HTCAPTION = 0x2;
        // MOVE THE WINDOW AROUND METHOD
        protected override void WndProc(ref Message message)
        {
            base.WndProc(ref message);
            if (message.Msg == WM_NCHITTEST && (int)message.Result == HTCLIENT)
                message.Result = (IntPtr)HTCAPTION;
        }
        public Main() {
            InitializeComponent();
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
                try
                {
                    player.playlist.Add(fname, ofd.FileName);
                } catch(Exception EX) {
                    MessageBox.Show("Error: Song already exists in the playlist.");
                    return;
                }
                songlist.Items.Add(fname);
                PlayBtn.Text = "⏹ Stop";
                SongPlayLbl.Text = fname;
                /////////////////////////
                player.stop();
                player.clear();
                player.open(ofd.FileName);
                player.playingSong = ofd.FileName;
                player.playingIndex = player.playlist.Count - 1;
                player.play();
                return;
            }
            return;
        }
        private void CloseBtn_Click(object sender, EventArgs e) {
            DialogResult RESULT = MessageBox.Show("Are you sure you want to close the application?", "Close the app...", MessageBoxButtons.YesNo);
            if (RESULT == DialogResult.Yes)
                Application.Exit();
                return;
        }
        private void MinimizeBtn_Click(object sender, EventArgs e) {
            this.WindowState = FormWindowState.Minimized;
            return;
        }
        private void songlist_SelectedIndexChanged(object sender, EventArgs e) {
            KeyValuePair<string, string> song = player.playlist.ElementAt(songlist.SelectedIndex);
            string fname = song.Key;
            string ofd = song.Value;
            // CHECK IF SONG IS ALREADY PLAYING
            if (ofd.Equals(player.playingSong)) {
                return;
            }
            PlayBtn.Text = "⏹ Stop";
            SongPlayLbl.Text = fname;
            /////////////////////////
            player.stop();
            player.clear();
            player.open(ofd);
            player.playingSong = ofd;
            player.playingIndex = player.playingIndex + 1;
            player.play();
            return;
        }
        private void PlayBtn_Click(object sender, EventArgs e) {
            string bt = PlayBtn.Text;
            if (bt.Equals("⏹ Stop"))
            {
                player.stop();
                PlayBtn.Text = "⏵Play";
                SongPlayLbl.Text += " (paused)";
            }
            else
            {
                player.play();
                PlayBtn.Text = "⏹ Stop";
                SongPlayLbl.Text = split_path(player.playingSong + "");
            }
            return;
        }
        private void NextBtn_Click(object sender, EventArgs e)
        {
            if ((player.playingIndex + 1) >= player.playlist.Count)
            {
                MessageBox.Show("End of playlist :(");
                return;
            }
            KeyValuePair<string,string> song= player.playlist.ElementAt(player.playingIndex + 1);
            string fname = song.Key;
            string ofd = song.Value;
            PlayBtn.Text = "⏹ Stop";
            SongPlayLbl.Text = fname;
            /////////////////////////
            player.stop();
            player.clear();
            player.open(ofd);
            player.playingSong = ofd;
            player.playingIndex = player.playingIndex + 1;
            player.play();
            return;
        }
        private void PrevBtn_Click(object sender, EventArgs e)
        {
            if ((player.playingIndex - 1) < 0)
            {
                MessageBox.Show("End of playlist :(");
                return;
            }
            KeyValuePair<string, string> song = player.playlist.ElementAt(player.playingIndex - 1);
            string fname = song.Key;
            string ofd = song.Value;
            PlayBtn.Text = "⏹ Stop";
            SongPlayLbl.Text = fname;
            /////////////////////////
            player.stop();
            player.clear();
            player.open(ofd);
            player.playingSong = ofd;
            player.playingIndex = player.playingIndex - 1;
            player.play();
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
            }
            return;
        }
        private void volumeBar_Scroll(object sender, EventArgs e) {
            int volume = volumeBar.Value;
            player.volume = volume;
            player.setVolume(volume);
            return;
        }
        class Player {
            [DllImport("winmm.dll")]
            private static extern long mciSendString(string lpstrCommand, StringBuilder lpstrReturnString, int uReturnLength, int hwdCallBack);
            public Dictionary<String, String> playlist = new Dictionary<String, String>();
            public String playingSong = "";
            public int playingIndex = 0;
            public int volume = 200;

            public void open(string File) {
                string Format = @"open ""{0}"" type MPEGVideo alias MediaFile";
                string command = string.Format(Format, File);
                mciSendString(command, null, 0, 0);
                playingSong = File;
            }
            public void play() {
                setVolume(volume);
                string command = "play MediaFile";
                mciSendString(command, null, 0, 0);
            }
            public void stop() {
                string command = "stop MediaFile";
                mciSendString(command, null, 0, 0);
            }
            public void clear() {
                string command = "close MediaFile";
                mciSendString(command, null, 0, 0);
            }
            public void setVolume(int volume) {
                var command = "setaudio MediaFile volume to " + volume.ToString();
                mciSendString(command, null, 0, 0);
            }
        }
    }
}
