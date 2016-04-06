using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.IO;
using System.Net;
using System.Xml;
using System.Windows.Forms;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Linq;

using System.ServiceModel.Syndication;
using System.Xml.Serialization;
namespace Spprss
{
    public class Config
    {
        private ListView lvUrls;
        private ListView lvNews;
        private List<UsersData> usersData = new List<UsersData>();
        public int active_site = 0;
        public Pool threadPool = new Pool(25);
        public int active_user = 0;
        private object IOLock = new object();
        private object DisplayLock = new object();

        public Config(string path, ListView lvUrls, ListView lvNews)
        {
            this.lvUrls = lvUrls;
            this.lvNews = lvNews;
            XmlDocument xmlFile = new XmlDocument();
            xmlFile.Load(path);
            List<SyndicationFeed> feeds = new List<SyndicationFeed>();
            if (!Directory.Exists("cache"))
            {
                Directory.CreateDirectory("cache");
            }
            XmlNodeList xmlNodes = xmlFile.SelectNodes("//users/user");
            if (xmlNodes != null)
            {
                foreach (XmlNode xmlNode in xmlNodes)
                {
                    int showCount = int.Parse(xmlNode.SelectSingleNode("filtering/threadCount/text()").Value);
                    XmlNodeList sitesXml = xmlNode.SelectNodes("sites/site/text()");
                    XmlNodeList includeXml = xmlNode.SelectNodes("filtering/include/item/text()");
                    XmlNodeList excludeXml = xmlNode.SelectNodes("filtering/exclude/item/text()");
                    List<string> siteList = (from XmlNode site in sitesXml select site.Value).ToList();
                    List<string> include = (from XmlNode item in includeXml select item.Value).ToList();
                    List<string> exclude = (from XmlNode item in excludeXml select item.Value).ToList();
                   
                        foreach(string exl in exclude)
                        {
                            if( include.Contains(exl))
                            {
                                include.Remove(exl);
                            }
                        }
                    usersData.Add(new UsersData(xmlNode.Attributes["name"].Value, showCount, siteList, include, exclude));
                }
            }
        }
        public void UpdateUsers()
        {
            XmlTextWriter filedatawriter = new XmlTextWriter("config.xml", Encoding.UTF8);
            filedatawriter.WriteStartDocument();
            filedatawriter.WriteStartElement("users");
            filedatawriter.WriteEndElement();
            filedatawriter.Close();
            XmlDocument xmlfile = new XmlDocument();
            xmlfile.Load("config.xml");
            foreach (UsersData user in usersData)
            {
                XmlNode userNode = xmlfile.CreateElement("user");

                XmlAttribute userAttribute = xmlfile.CreateAttribute("name");
                userAttribute.Value = user.Name;
                userNode.Attributes.Append(userAttribute);

                XmlNode filtering = xmlfile.CreateElement("filtering");
                XmlNode threadCount = xmlfile.CreateElement("threadCount");
                threadCount.InnerText = user.ShowCount.ToString();
                XmlNode include = xmlfile.CreateElement("include");
                XmlNode exclude = xmlfile.CreateElement("exclude");
                
                foreach (string includeStr in user.Include)
                {
                    XmlNode includeNode = xmlfile.CreateElement("item");
                    includeNode.InnerText = includeStr;
                    include.AppendChild(includeNode);
                }
                foreach (string excludeStr in user.Exclude)
                {
                    XmlNode excludeNode = xmlfile.CreateElement("item");
                    excludeNode.InnerText = excludeStr;
                    exclude.AppendChild(excludeNode);
                }

                filtering.AppendChild(threadCount);
                filtering.AppendChild(include);
                filtering.AppendChild(exclude);
                userNode.AppendChild(filtering);
                XmlNode sites = xmlfile.CreateElement("sites");
                foreach (string siteStr in user.Sites)
                {
                    XmlNode siteNode = xmlfile.CreateElement("site");
                    siteNode.InnerText = siteStr;
                    sites.AppendChild(siteNode);
                }

                userNode.AppendChild(sites);


                xmlfile.DocumentElement.AppendChild(userNode);
            }
            xmlfile.Save("config.xml");

        }
        public UsersData GetUser(int id)
        {
            return this.usersData[id];
        }
        public void SetUser(int id, UsersData data)
        {
            this.usersData[id] = data;
        }
        public void BuildSelection(ComboBox box)
        {
            box.Items.Clear();
            for (int i = 0; i < this.usersData.Count; i++)
            {
                box.Items.AddRange(new object[] { this.usersData[i].Name });
                box.SelectedIndex = 0;
            }
        }
        public bool NeedInclude(int user, string title, string desc)
        {
            if (usersData[user].Include.Count() > 0)
            {
                foreach (string include in usersData[user].Include)
                {
                    if (title.Contains(include) && desc.Contains(include))
                    {
                        return true;
                    }
                }
            }
            else
            {
                return true;
            }
            return false;
        }
        public bool NeedExclude(int user, string title, string desc)
        {
            if (usersData[user].Exclude.Count() > 0)
            {
                foreach (string exclude in usersData[user].Exclude)
                {
                    if (title.Contains(exclude) || desc.Contains(exclude))
                    {
                        return false;
                    }
                }
            }
            else
            {
                return true;
            }
            return true;
        }
        public void ShowAll()
        {
            SHowAll(active_user);
        }
        public void SHowAll(int user)
        {
            lock (DisplayLock)
            {
                    foreach (string item in usersData[user].Sites)
                    {
                        
                        XmlReader FeedReader = XmlReader.Create(item);
                        
                        SyndicationFeed Channel = SyndicationFeed.Load(FeedReader);

                        
                        if (Channel != null)
                        {
                            
                            foreach (SyndicationItem RSI in Channel.Items)
                            {
                                
                                if (NeedInclude(active_user, RSI.Title.Text, RSI.Summary.Text) && NeedExclude(active_user, RSI.Title.Text, RSI.Summary.Text))
                                {
                                    ListViewItem LVI = new ListViewItem(RSI.Title.Text);
                                    LVI.Name = RSI.Title.Text;
                                    
                                    LVI.Tag = RSI;
                                    
                                    lvNews.Invoke(new Action(delegate() { lvNews.Items.Add(LVI); }));
                                }

                            }
                        }
                    }
            }
        }
        public void RefreshWebPanel(int user)
        {
            lvUrls.Items.Clear();
            foreach (string site in usersData[user].Sites)
            {
                ListViewItem lvi = new ListViewItem(site);
                lvUrls.Items.Add(lvi);
            }
            if (usersData[user].Sites.Count == 0)
            {
                ListViewItem lvi = new ListViewItem(new string[]{"no sites"});
                lvUrls.Items.Add(lvi);
            }
        }
    }
}
