using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spprss
{
    [Serializable()]
    public class Items
    {
        public string Site{ get; set; }
       
        public string Link { get; set; }
      
        public string Description { get; set; }
        
        public string PubDate { get; set; }
       
        public string Header { get; set; }
    }
}
