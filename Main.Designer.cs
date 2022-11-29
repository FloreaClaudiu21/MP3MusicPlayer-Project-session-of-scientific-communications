namespace SongPlayer_Project
{
    partial class Main
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.label1 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.MinimizeBtn = new System.Windows.Forms.Button();
            this.CloseBtn = new System.Windows.Forms.Button();
            this.mainpanel = new System.Windows.Forms.Panel();
            this.playpanel = new System.Windows.Forms.Panel();
            this.SongPlayLbl = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.songlist = new System.Windows.Forms.ListBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.volumeBar = new System.Windows.Forms.TrackBar();
            this.button2 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.NextBtn = new System.Windows.Forms.Button();
            this.PlayBtn = new System.Windows.Forms.Button();
            this.PrevBtn = new System.Windows.Forms.Button();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.button1 = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.AddBtn = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.noResImg = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.mainpanel.SuspendLayout();
            this.playpanel.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.volumeBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.noResImg)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(117)))), ((int)(((byte)(214)))));
            this.label1.Name = "label1";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::SongPlayer_Project.Properties.Resources.logo_RAU_en;
            resources.ApplyResources(this.pictureBox1, "pictureBox1");
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.TabStop = false;
            // 
            // MinimizeBtn
            // 
            resources.ApplyResources(this.MinimizeBtn, "MinimizeBtn");
            this.MinimizeBtn.FlatAppearance.BorderSize = 0;
            this.MinimizeBtn.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.MinimizeBtn.Name = "MinimizeBtn";
            this.MinimizeBtn.UseVisualStyleBackColor = true;
            this.MinimizeBtn.Click += new System.EventHandler(this.MinimizeBtn_Click);
            // 
            // CloseBtn
            // 
            resources.ApplyResources(this.CloseBtn, "CloseBtn");
            this.CloseBtn.FlatAppearance.BorderSize = 0;
            this.CloseBtn.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.CloseBtn.Name = "CloseBtn";
            this.CloseBtn.UseVisualStyleBackColor = true;
            this.CloseBtn.Click += new System.EventHandler(this.CloseBtn_Click);
            // 
            // mainpanel
            // 
            this.mainpanel.BackColor = System.Drawing.Color.White;
            this.mainpanel.Controls.Add(this.playpanel);
            this.mainpanel.Controls.Add(this.AddBtn);
            this.mainpanel.Controls.Add(this.label2);
            this.mainpanel.Controls.Add(this.noResImg);
            resources.ApplyResources(this.mainpanel, "mainpanel");
            this.mainpanel.Name = "mainpanel";
            // 
            // playpanel
            // 
            resources.ApplyResources(this.playpanel, "playpanel");
            this.playpanel.BackColor = System.Drawing.Color.White;
            this.playpanel.Controls.Add(this.SongPlayLbl);
            this.playpanel.Controls.Add(this.label4);
            this.playpanel.Controls.Add(this.songlist);
            this.playpanel.Controls.Add(this.panel2);
            this.playpanel.Controls.Add(this.pictureBox2);
            this.playpanel.Controls.Add(this.button1);
            this.playpanel.Controls.Add(this.label3);
            this.playpanel.Name = "playpanel";
            // 
            // SongPlayLbl
            // 
            resources.ApplyResources(this.SongPlayLbl, "SongPlayLbl");
            this.SongPlayLbl.Name = "SongPlayLbl";
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // songlist
            // 
            resources.ApplyResources(this.songlist, "songlist");
            this.songlist.BackColor = System.Drawing.SystemColors.Menu;
            this.songlist.FormattingEnabled = true;
            this.songlist.Name = "songlist";
            this.songlist.SelectedIndexChanged += new System.EventHandler(this.songlist_SelectedIndexChanged);
            // 
            // panel2
            // 
            resources.ApplyResources(this.panel2, "panel2");
            this.panel2.BackColor = System.Drawing.Color.LightGray;
            this.panel2.Controls.Add(this.label5);
            this.panel2.Controls.Add(this.volumeBar);
            this.panel2.Controls.Add(this.button2);
            this.panel2.Controls.Add(this.button6);
            this.panel2.Controls.Add(this.NextBtn);
            this.panel2.Controls.Add(this.PlayBtn);
            this.panel2.Controls.Add(this.PrevBtn);
            this.panel2.Name = "panel2";
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.Name = "label5";
            // 
            // volumeBar
            // 
            this.volumeBar.LargeChange = 1;
            resources.ApplyResources(this.volumeBar, "volumeBar");
            this.volumeBar.Maximum = 200;
            this.volumeBar.Name = "volumeBar";
            this.volumeBar.SmallChange = 2;
            this.volumeBar.Value = 200;
            this.volumeBar.Scroll += new System.EventHandler(this.volumeBar_Scroll);
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.RosyBrown;
            this.button2.FlatAppearance.BorderSize = 0;
            resources.ApplyResources(this.button2, "button2");
            this.button2.ForeColor = System.Drawing.SystemColors.Control;
            this.button2.Name = "button2";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button6
            // 
            this.button6.BackColor = System.Drawing.Color.Green;
            this.button6.FlatAppearance.BorderSize = 0;
            resources.ApplyResources(this.button6, "button6");
            this.button6.ForeColor = System.Drawing.SystemColors.Control;
            this.button6.Name = "button6";
            this.button6.UseVisualStyleBackColor = false;
            this.button6.Click += new System.EventHandler(this.AddBtn_Click);
            // 
            // NextBtn
            // 
            this.NextBtn.BackColor = System.Drawing.Color.DimGray;
            this.NextBtn.FlatAppearance.BorderSize = 0;
            resources.ApplyResources(this.NextBtn, "NextBtn");
            this.NextBtn.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.NextBtn.Name = "NextBtn";
            this.NextBtn.UseVisualStyleBackColor = false;
            this.NextBtn.Click += new System.EventHandler(this.NextBtn_Click);
            // 
            // PlayBtn
            // 
            this.PlayBtn.BackColor = System.Drawing.Color.DimGray;
            this.PlayBtn.FlatAppearance.BorderSize = 0;
            resources.ApplyResources(this.PlayBtn, "PlayBtn");
            this.PlayBtn.ForeColor = System.Drawing.SystemColors.Desktop;
            this.PlayBtn.Name = "PlayBtn";
            this.PlayBtn.UseVisualStyleBackColor = false;
            this.PlayBtn.Click += new System.EventHandler(this.PlayBtn_Click);
            // 
            // PrevBtn
            // 
            this.PrevBtn.BackColor = System.Drawing.Color.DimGray;
            this.PrevBtn.FlatAppearance.BorderSize = 0;
            resources.ApplyResources(this.PrevBtn, "PrevBtn");
            this.PrevBtn.ForeColor = System.Drawing.SystemColors.Control;
            this.PrevBtn.Name = "PrevBtn";
            this.PrevBtn.UseVisualStyleBackColor = false;
            this.PrevBtn.Click += new System.EventHandler(this.PrevBtn_Click);
            // 
            // pictureBox2
            // 
            resources.ApplyResources(this.pictureBox2, "pictureBox2");
            this.pictureBox2.Image = global::SongPlayer_Project.Properties.Resources.music_notes_png_finding_your_song_girl_got_faith_27;
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.TabStop = false;
            // 
            // button1
            // 
            resources.ApplyResources(this.button1, "button1");
            this.button1.BackColor = System.Drawing.Color.Green;
            this.button1.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.button1.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.button1.Name = "button1";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AllowDrop = true;
            resources.ApplyResources(this.label3, "label3");
            this.label3.AutoEllipsis = true;
            this.label3.ForeColor = System.Drawing.SystemColors.AppWorkspace;
            this.label3.Name = "label3";
            this.label3.UseCompatibleTextRendering = true;
            // 
            // AddBtn
            // 
            resources.ApplyResources(this.AddBtn, "AddBtn");
            this.AddBtn.BackColor = System.Drawing.Color.Green;
            this.AddBtn.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.AddBtn.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.AddBtn.Name = "AddBtn";
            this.AddBtn.UseVisualStyleBackColor = true;
            this.AddBtn.Click += new System.EventHandler(this.AddBtn_Click);
            // 
            // label2
            // 
            this.label2.AllowDrop = true;
            resources.ApplyResources(this.label2, "label2");
            this.label2.AutoEllipsis = true;
            this.label2.ForeColor = System.Drawing.SystemColors.AppWorkspace;
            this.label2.Name = "label2";
            this.label2.UseCompatibleTextRendering = true;
            // 
            // noResImg
            // 
            this.noResImg.Image = global::SongPlayer_Project.Properties.Resources.no_results;
            resources.ApplyResources(this.noResImg, "noResImg");
            this.noResImg.Name = "noResImg";
            this.noResImg.TabStop = false;
            // 
            // Main
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.Controls.Add(this.CloseBtn);
            this.Controls.Add(this.MinimizeBtn);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.mainpanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Main";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.mainpanel.ResumeLayout(false);
            this.mainpanel.PerformLayout();
            this.playpanel.ResumeLayout(false);
            this.playpanel.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.volumeBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.noResImg)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Panel mainpanel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button MinimizeBtn;
        private System.Windows.Forms.Button CloseBtn;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.PictureBox noResImg;
        private System.Windows.Forms.Button AddBtn;
        private System.Windows.Forms.Panel playpanel;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ListBox songlist;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Button NextBtn;
        private System.Windows.Forms.Button PlayBtn;
        private System.Windows.Forms.Button PrevBtn;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label SongPlayLbl;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TrackBar volumeBar;
        private System.Windows.Forms.Label label5;
    }
}
