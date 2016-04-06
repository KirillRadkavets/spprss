using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel.Syndication;
namespace Spprss
{
    [Serializable]
    public class UsersData
    {
        public string Name { get; set; }
        public int ShowCount { get; set; }
        public IList<string> Sites { get; set; }
        public IList<string> Include { get; set; }
        public IList<string> Exclude { get; set; }

        public List<Items> Cashe {get;set;}

        public UsersData(string name, int showCount, IList<string> sites, IList<string> include, IList<string> exclude)
        {
            this.Name = name;
            this.ShowCount = showCount;
            this.Sites = new List<string>(sites);
            this.Include = new List<string>(include);
            this.Exclude = new List<string>(exclude);
            
            this.Cashe = new List<Items>();
        }
    }
}

