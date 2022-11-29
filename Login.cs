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
    public partial class Login : Form {
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
        public Login() {
            InitializeComponent();
            populateDictionary();
            return;
        }
        Dictionary<String, String> DIC = new Dictionary<string, string>();
        private void populateDictionary() {
            string[] lines1 = File.ReadAllLines("pass.txt");
            string[] lines = File.ReadAllLines("user.txt");
            for (int i = 0; i < lines.Length; i++) {
                DIC.Add(lines[i], lines1[i]);
            }
        }
        private bool canLogin(string user, string pass)
        {
            if (!DIC.ContainsKey(user))
            {
                return false;
            }
            string? passd = DIC.GetValueOrDefault(user);
            if (passd == null) return false;
            if (passd.Equals(pass))
            {
                return true;
            }
            return false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string user = txtUserName.Text;
            string pass = txtPassword.Text;
            if (String.IsNullOrEmpty(user) || String.IsNullOrWhiteSpace(pass)) {
                return;
            }
            if (canLogin(user, pass)) {
                new Main().Show();
                this.Hide();
               
            } else { 
                MessageBox.Show("Try Again!");
            }   
            txtUserName.Clear();
            txtPassword.Clear();
            txtUserName.Focus();
            return;
        }

        private void label2_Click(object sender, EventArgs e) {
            txtUserName.Clear();
            txtPassword.Clear();
            txtUserName.Focus();
            return;
        }

        private void label3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}