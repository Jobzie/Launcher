namespace EFT_Launcher_12
{
    partial class MainWindow
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.startButton = new System.Windows.Forms.Button();
            this.profilesListBox = new System.Windows.Forms.ComboBox();
            this.gamePathTextBox = new System.Windows.Forms.TextBox();
            this.profileEditButton = new System.Windows.Forms.Button();
            this.backendUrlTextBox = new System.Windows.Forms.TextBox();
            this.serverPathTextBox = new System.Windows.Forms.TextBox();
            this.serverOutputRichBox = new System.Windows.Forms.RichTextBox();
            this.background_panel = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Candara", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(36, 81);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 18);
            this.label1.TabIndex = 3;
            this.label1.Text = "Game Location";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Candara", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(31, 44);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(105, 18);
            this.label2.TabIndex = 9;
            this.label2.Text = "Server Location";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Candara", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(53, 119);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(83, 18);
            this.label3.TabIndex = 11;
            this.label3.Text = "Backend Url";
            // 
            // startButton
            // 
            this.startButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.startButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.startButton.Font = new System.Drawing.Font("Trebuchet MS", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.startButton.ForeColor = System.Drawing.SystemColors.Control;
            this.startButton.Location = new System.Drawing.Point(412, 100);
            this.startButton.Name = "startButton";
            this.startButton.Size = new System.Drawing.Size(160, 49);
            this.startButton.TabIndex = 0;
            this.startButton.Text = "Start Game";
            this.startButton.UseVisualStyleBackColor = false;
            this.startButton.Click += new System.EventHandler(this.startButton_Click);
            // 
            // profilesListBox
            // 
            this.profilesListBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.profilesListBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.profilesListBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.profilesListBox.Font = new System.Drawing.Font("Candara", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.profilesListBox.ForeColor = System.Drawing.SystemColors.Window;
            this.profilesListBox.FormattingEnabled = true;
            this.profilesListBox.Items.AddRange(new object[] {
            "Select a Profile !"});
            this.profilesListBox.Location = new System.Drawing.Point(412, 14);
            this.profilesListBox.Name = "profilesListBox";
            this.profilesListBox.Size = new System.Drawing.Size(160, 26);
            this.profilesListBox.TabIndex = 1;
            this.profilesListBox.SelectedIndexChanged += new System.EventHandler(this.profilesListBox_SelectedIndexChanged);
            // 
            // gamePathTextBox
            // 
            this.gamePathTextBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.gamePathTextBox.Font = new System.Drawing.Font("Candara", 11.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gamePathTextBox.ForeColor = System.Drawing.Color.White;
            this.gamePathTextBox.Location = new System.Drawing.Point(139, 77);
            this.gamePathTextBox.Name = "gamePathTextBox";
            this.gamePathTextBox.Size = new System.Drawing.Size(199, 26);
            this.gamePathTextBox.TabIndex = 2;
            this.gamePathTextBox.TextChanged += new System.EventHandler(this.gamePathTextBox_TextChanged);
            // 
            // profileEditButton
            // 
            this.profileEditButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.profileEditButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.profileEditButton.Font = new System.Drawing.Font("Candara", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.profileEditButton.ForeColor = System.Drawing.SystemColors.Control;
            this.profileEditButton.Location = new System.Drawing.Point(412, 46);
            this.profileEditButton.Name = "profileEditButton";
            this.profileEditButton.Size = new System.Drawing.Size(160, 26);
            this.profileEditButton.TabIndex = 6;
            this.profileEditButton.Text = "Edit Profile";
            this.profileEditButton.UseVisualStyleBackColor = false;
            this.profileEditButton.Click += new System.EventHandler(this.profileEditButton_Click);
            // 
            // backendUrlTextBox
            // 
            this.backendUrlTextBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.backendUrlTextBox.Font = new System.Drawing.Font("Candara", 11.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.backendUrlTextBox.ForeColor = System.Drawing.Color.White;
            this.backendUrlTextBox.Location = new System.Drawing.Point(139, 115);
            this.backendUrlTextBox.Name = "backendUrlTextBox";
            this.backendUrlTextBox.Size = new System.Drawing.Size(199, 26);
            this.backendUrlTextBox.TabIndex = 10;
            this.backendUrlTextBox.Text = "Check if backendUrl matches!";
            this.backendUrlTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.backendUrlTextBox.TextChanged += new System.EventHandler(this.backendUrlTextBox_TextChanged);
            // 
            // serverPathTextBox
            // 
            this.serverPathTextBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.serverPathTextBox.Font = new System.Drawing.Font("Candara", 11.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.serverPathTextBox.ForeColor = System.Drawing.Color.White;
            this.serverPathTextBox.Location = new System.Drawing.Point(139, 40);
            this.serverPathTextBox.Name = "serverPathTextBox";
            this.serverPathTextBox.Size = new System.Drawing.Size(199, 26);
            this.serverPathTextBox.TabIndex = 8;
            this.serverPathTextBox.TextChanged += new System.EventHandler(this.serverPathTextBox_TextChanged);
            // 
            // serverOutputRichBox
            // 
            this.serverOutputRichBox.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.serverOutputRichBox.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.serverOutputRichBox.ForeColor = System.Drawing.Color.Lime;
            this.serverOutputRichBox.Location = new System.Drawing.Point(12, 162);
            this.serverOutputRichBox.Name = "serverOutputRichBox";
            this.serverOutputRichBox.Size = new System.Drawing.Size(585, 202);
            this.serverOutputRichBox.TabIndex = 7;
            this.serverOutputRichBox.Text = "";
            // 
            // background_panel
            // 
            this.background_panel.Location = new System.Drawing.Point(0, 0);
            this.background_panel.MaximumSize = new System.Drawing.Size(609, 377);
            this.background_panel.MinimumSize = new System.Drawing.Size(609, 162);
            this.background_panel.Name = "background_panel";
            this.background_panel.Size = new System.Drawing.Size(609, 162);
            this.background_panel.TabIndex = 12;
            this.background_panel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Title_MouseDown);
            this.background_panel.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Title_MouseMove);
            this.background_panel.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Title_MouseUp);
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.ClientSize = new System.Drawing.Size(609, 162);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.backendUrlTextBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.serverPathTextBox);
            this.Controls.Add(this.serverOutputRichBox);
            this.Controls.Add(this.profileEditButton);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.gamePathTextBox);
            this.Controls.Add(this.profilesListBox);
            this.Controls.Add(this.startButton);
            this.Controls.Add(this.background_panel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(609, 377);
            this.MinimumSize = new System.Drawing.Size(609, 162);
            this.Name = "MainWindow";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Button startButton;
		private System.Windows.Forms.ComboBox profilesListBox;
		private System.Windows.Forms.TextBox gamePathTextBox;
		private System.Windows.Forms.Button profileEditButton;
		private System.Windows.Forms.TextBox backendUrlTextBox;
		private System.Windows.Forms.TextBox serverPathTextBox;
		private System.Windows.Forms.RichTextBox serverOutputRichBox;
        private System.Windows.Forms.Panel background_panel;
    }
}