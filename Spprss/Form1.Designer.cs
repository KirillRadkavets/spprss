namespace Spprss
{
    partial class Form1
    {
        /// <summary>
        /// Требуется переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Обязательный метод для поддержки конструктора - не изменяйте
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.wbDescription = new System.Windows.Forms.WebBrowser();
            this.lvNews = new System.Windows.Forms.ListView();
            this.cbUsers = new System.Windows.Forms.ComboBox();
            this.lvUrls = new System.Windows.Forms.ListView();
            this.btn_Settings = new System.Windows.Forms.Button();
            this.tbDate = new System.Windows.Forms.TextBox();
            this.btn_Show_All = new System.Windows.Forms.Button();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.SuspendLayout();
            // 
            // wbDescription
            // 
            this.wbDescription.Location = new System.Drawing.Point(437, 36);
            this.wbDescription.MinimumSize = new System.Drawing.Size(20, 20);
            this.wbDescription.Name = "wbDescription";
            this.wbDescription.Size = new System.Drawing.Size(293, 299);
            this.wbDescription.TabIndex = 2;
            // 
            // lvNews
            // 
            this.lvNews.Location = new System.Drawing.Point(13, 194);
            this.lvNews.Name = "lvNews";
            this.lvNews.Size = new System.Drawing.Size(418, 169);
            this.lvNews.TabIndex = 3;
            this.lvNews.UseCompatibleStateImageBehavior = false;
            this.lvNews.View = System.Windows.Forms.View.List;
            this.lvNews.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lvNews_MouseDown);
            // 
            // cbUsers
            // 
            this.cbUsers.FormattingEnabled = true;
            this.cbUsers.Location = new System.Drawing.Point(12, 12);
            this.cbUsers.Name = "cbUsers";
            this.cbUsers.Size = new System.Drawing.Size(142, 21);
            this.cbUsers.TabIndex = 4;
            this.cbUsers.SelectedIndexChanged += new System.EventHandler(this.cbUsers_SelectedIndexChanged);
            // 
            // lvUrls
            // 
            this.lvUrls.GridLines = true;
            this.lvUrls.Location = new System.Drawing.Point(13, 42);
            this.lvUrls.Name = "lvUrls";
            this.lvUrls.Size = new System.Drawing.Size(418, 148);
            this.lvUrls.TabIndex = 5;
            this.lvUrls.UseCompatibleStateImageBehavior = false;
            this.lvUrls.View = System.Windows.Forms.View.List;
            this.lvUrls.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lvUrls_MouseDown);
            // 
            // btn_Settings
            // 
            this.btn_Settings.Location = new System.Drawing.Point(160, 12);
            this.btn_Settings.Name = "btn_Settings";
            this.btn_Settings.Size = new System.Drawing.Size(75, 23);
            this.btn_Settings.TabIndex = 6;
            this.btn_Settings.Text = "Settings";
            this.btn_Settings.UseVisualStyleBackColor = true;
            this.btn_Settings.Click += new System.EventHandler(this.btn_Settings_Click);
            // 
            // tbDate
            // 
            this.tbDate.Location = new System.Drawing.Point(437, 12);
            this.tbDate.Name = "tbDate";
            this.tbDate.Size = new System.Drawing.Size(293, 20);
            this.tbDate.TabIndex = 7;
            // 
            // btn_Show_All
            // 
            this.btn_Show_All.Location = new System.Drawing.Point(254, 12);
            this.btn_Show_All.Name = "btn_Show_All";
            this.btn_Show_All.Size = new System.Drawing.Size(75, 23);
            this.btn_Show_All.TabIndex = 8;
            this.btn_Show_All.Text = "ShowAll";
            this.btn_Show_All.UseVisualStyleBackColor = true;
            this.btn_Show_All.Click += new System.EventHandler(this.btn_Show_All_Click);
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Location = new System.Drawing.Point(437, 350);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(55, 13);
            this.linkLabel1.TabIndex = 12;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "linkLabel1";
            this.linkLabel1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.linkLabel1_MouseDoubleClick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(742, 375);
            this.Controls.Add(this.linkLabel1);
            this.Controls.Add(this.btn_Show_All);
            this.Controls.Add(this.tbDate);
            this.Controls.Add(this.btn_Settings);
            this.Controls.Add(this.lvUrls);
            this.Controls.Add(this.cbUsers);
            this.Controls.Add(this.lvNews);
            this.Controls.Add(this.wbDescription);
            this.Name = "Form1";
            this.Text = "Form1";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.WebBrowser wbDescription;
        private System.Windows.Forms.ListView lvNews;
        private System.Windows.Forms.ComboBox cbUsers;
        private System.Windows.Forms.ListView lvUrls;
        private System.Windows.Forms.Button btn_Settings;
        private System.Windows.Forms.TextBox tbDate;
        private System.Windows.Forms.Button btn_Show_All;
        private System.Windows.Forms.LinkLabel linkLabel1;
    }
}

