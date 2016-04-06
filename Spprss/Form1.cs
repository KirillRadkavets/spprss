using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.ServiceModel.Syndication;

using System.Xml;
using System.Diagnostics;

namespace Spprss
{
    public partial class Form1 : Form
    {
        Config cfg;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            cfg = new Config("config.xml", lvUrls, lvNews);
            cfg.BuildSelection(cbUsers);
            cfg.RefreshWebPanel(cbUsers.SelectedIndex);
        }
        private void refresh_button_Click(object sender, EventArgs e)
        {
          //  cfg.active_user = cbUsers.SelectedIndex;
          //  cfg.threadPool.QueueTask(cfg.UpdateCache);
        }

        private void lvNews_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                // получаем ListViewItem, который находится под курсором
                ListViewItem item = lvNews.GetItemAt(e.X, e.Y);

                // если курс не на пустом месте, то
                if (item != null)
                {
                    // получаем связаную с выбраным ListViewItem новость
                    SyndicationItem RSI = (SyndicationItem)item.Tag;

                    // выводим полный текст новости
                    // иначе RSS
                    wbDescription.DocumentText = RSI.Summary.Text;
                    tbDate.Text = RSI.PublishDate.ToString();
                    linkLabel1.Text = RSI.Id.ToString();
                }
            }
        }

        private void cbUsers_SelectedIndexChanged(object sender, EventArgs e)
        {
            lvUrls.Items.Clear();
            lvNews.Items.Clear();
            cfg.RefreshWebPanel(cbUsers.SelectedIndex);
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            cfg.threadPool.Dispose();
        }
        private void lvUrls_MouseDown(object sender, MouseEventArgs e)
        {

            if (e.Button == MouseButtons.Left)
            {
                // получаем ListViewItem, который находится под курсором
                ListViewItem item = lvUrls.GetItemAt(e.X, e.Y);

                // если курс не на пустом месте, то
                if (item != null)
                {
                    // Очищаем ListView перед добавлением новых данных
                    lvNews.Clear();
                    XmlReader FeedReader = XmlReader.Create(item.Text);
                    // Загружаем RSS/Atom
                    SyndicationFeed Channel = new SyndicationFeed();
                    try
                    {
                        Channel=SyndicationFeed.Load(FeedReader);
                    }
                    catch
                    {
                        MessageBox.Show("Проверьте интернет соединение");
                    }
                    if (Channel != null)
                    {
                        // обрабатываем каждую новость канала
                        foreach (SyndicationItem RSI in Channel.Items)
                        {
                            // создаем элемент для вывода в ListView
                            if(cfg.NeedInclude(cfg.active_user, RSI.Title.Text, RSI.Summary.Text) && cfg.NeedExclude(cfg.active_user, RSI.Title.Text, RSI.Summary.Text))
                            { 
                            ListViewItem LVI = new ListViewItem(RSI.Title.Text);
                            LVI.Name = RSI.Title.Text;
                            // связываем ListViewItem и новость
                            LVI.Tag = RSI;
                            // добавляем новость в ListView
                            lvNews.Items.Add(LVI);

                            }

                        }
                    }
                }
            }
        }

        private void btn_Settings_Click(object sender, EventArgs e)
        {
            Form settingsForm = new Form();
            settingsForm.Size = new System.Drawing.Size(350, 300);
            settingsForm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            settingsForm.MaximizeBox = false;
            settingsForm.MinimizeBox = false;
            FlowLayoutPanel settingsPanel = new FlowLayoutPanel();
            settingsPanel.FlowDirection = FlowDirection.TopDown;
            settingsPanel.WrapContents = false;
            settingsPanel.AutoScroll = true;
            settingsPanel.Dock = DockStyle.Fill;
            UsersData data = cfg.GetUser(cbUsers.SelectedIndex);
            Label nameLabel = new Label();
            nameLabel.Text = "Name:";
            TextBox nameBox = new TextBox();
            nameBox.Width = 250;
            nameBox.Text = data.Name;
            settingsPanel.Controls.Add(nameLabel);
            settingsPanel.Controls.Add(nameBox);
            Label countLabel = new Label();
            countLabel.Text = "Name:";
            TextBox countBox = new TextBox();
            countBox.Width = 250;
            countBox.Text = data.ShowCount.ToString();
            settingsPanel.Controls.Add(countLabel);
            settingsPanel.Controls.Add(countBox);
            Label filterInclude = new Label();
            filterInclude.Text = "Include:";
            FlowLayoutPanel includePanel = new FlowLayoutPanel();
            includePanel.FlowDirection = FlowDirection.TopDown;
            includePanel.Width = 300;
            includePanel.WrapContents = false;
            includePanel.AutoScroll = true;
            int iC = 0;
            foreach (string str in data.Include)
            {
                TextBox includeBox = new TextBox();
                includeBox.Name = string.Format("Include{0}", iC);
                includeBox.Width = 240;
                includeBox.Text = str;
                includePanel.Controls.Add(includeBox);
                iC++;
            }

            Button addInclude = new Button();
            addInclude.Text = "Add include";
            addInclude.Click += delegate
            {
                TextBox includeBox = new TextBox();
                includeBox.Name = string.Format("Include{0}", iC);
                includeBox.Width = 240;
                includeBox.Text = string.Empty;
                includePanel.Controls.Add(includeBox);
                iC++;
            };
            settingsPanel.Controls.Add(filterInclude);
            settingsPanel.Controls.Add(includePanel);
            settingsPanel.Controls.Add(addInclude);
            Label filterExclude = new Label();
            filterExclude.Text = "Exclude:";
            FlowLayoutPanel excludePanel = new FlowLayoutPanel();
            excludePanel.FlowDirection = FlowDirection.TopDown;
            excludePanel.Width = 300;
            excludePanel.WrapContents = false;
            excludePanel.AutoScroll = true;
            int iD = 0;
            foreach (string str in data.Exclude)
            {
                TextBox excludeBox = new TextBox();
                excludeBox.Name = string.Format("Exclude{0}", iD);
                excludeBox.Width = 240;
                excludeBox.Text = str;
                excludePanel.Controls.Add(excludeBox);
                iD++;
            }
            Button addexclude = new Button();
            addexclude.Text = "Add exclude";
            addexclude.Click += delegate
            {
                TextBox excludeBox = new TextBox();
                excludeBox.Name = string.Format("Exclude{0}", iD);
                excludeBox.Width = 240;
                excludeBox.Text = string.Empty;
                excludePanel.Controls.Add(excludeBox);
                iD++;
            };
            settingsPanel.Controls.Add(filterExclude);
            settingsPanel.Controls.Add(excludePanel);
            settingsPanel.Controls.Add(addexclude);
            Label sitesText = new Label();
            sitesText.Text = "Sites:";
            settingsPanel.Controls.Add(sitesText);
            FlowLayoutPanel sitesPanel = new FlowLayoutPanel();
            sitesPanel.FlowDirection = FlowDirection.TopDown;
            sitesPanel.Width = 300;
            sitesPanel.WrapContents = false;
            sitesPanel.AutoScroll = true;
            int i = 0;
            foreach (string str in data.Sites)
            {
                TextBox siteBox = new TextBox();
                siteBox.Name = string.Format("Site{0}", i);
                siteBox.Width = 240;
                siteBox.Text = str;
                sitesPanel.Controls.Add(siteBox);
                i++;
            }
            settingsPanel.Controls.Add(sitesPanel);
            Button addSite = new Button();
            addSite.Text = "Add site";
            addSite.Click += delegate
            {
                TextBox siteBox = new TextBox();
                siteBox.Name = string.Format("Site{0}", i);
                siteBox.Width = 240;
                siteBox.Text = string.Empty;
                sitesPanel.Controls.Add(siteBox);
                i++;
            };
            settingsPanel.Controls.Add(addSite);
            Button editButton = new Button();
            editButton.Text = "Edit";
            editButton.Click += delegate
            {
                data.Name = nameBox.Text;
                List<string> sites = new List<string>();
                List<string> include = new List<string>();
                List<string> exclude = new List<string>();
                int j = 0;
                while (sitesPanel.Controls[string.Format("Site{0}", j)] != null)
                {
                    string text = ((TextBox)(sitesPanel.Controls[string.Format("Site{0}", j)])).Text;
                    if (text != string.Empty)
                    {
                        sites.Add(text);
                    }
                    j++;
                }
                int jD = 0;
                while (excludePanel.Controls[string.Format("Exclude{0}", jD)] != null)
                {
                    string text = ((TextBox)(excludePanel.Controls[string.Format("Exclude{0}", jD)])).Text;
                    if (text != string.Empty)
                    {
                        exclude.Add(text);
                    }
                    jD++;
                }
 
                int jC = 0;
                while (includePanel.Controls[string.Format("Include{0}", jC)] != null)
                {
                    string text = ((TextBox)(includePanel.Controls[string.Format("Include{0}", jC)])).Text;
                    if (text != string.Empty && !exclude.Contains(text))
                    {
                        include.Add(text);
                    }
                    jC++;
                }
                data.Sites = sites;
                data.ShowCount = Convert.ToInt32(countBox.Text);
                data.Include = include;
                data.Exclude = exclude;
                cfg.SetUser(cbUsers.SelectedIndex, data);
                cfg.BuildSelection(cbUsers);
                cfg.RefreshWebPanel(cbUsers.SelectedIndex);
                cfg.UpdateUsers();
                settingsForm.Close();
                settingsForm.Dispose();
            };
            settingsPanel.Controls.Add(editButton);
            settingsForm.Controls.Add(settingsPanel);
            settingsForm.ShowDialog();
        }

        private void btn_Show_All_Click(object sender, EventArgs e)
        {
            lvNews.Items.Clear();
            if (lvUrls != null)
            {
                cfg.active_user = cbUsers.SelectedIndex;
                cfg.threadPool.QueueTask(cfg.ShowAll);
            }
            else
                MessageBox.Show("Empty");
                         
        }

        private void linkLabel1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Process.Start(new ProcessStartInfo(linkLabel1.Text));
        }

        
    }
}

