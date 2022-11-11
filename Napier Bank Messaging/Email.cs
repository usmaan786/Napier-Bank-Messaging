using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Napier_Bank_Messaging
{
    public class Email
    {
        private string id;
        private bool sir;
        private string sortCode;
        private string incident;

        private List<URL> urlLinks = new List<URL>();
        public string emailID
        {
            get { return id; }
            set { id = value; }
        }

        public bool SIR
        {
            get { return sir; }
            set { sir = value; }
        }

        public string SortCode
        {
            get { return sortCode; }
            set { sortCode = value; }
        }

        public string Incident
        {
            get { return incident; }
            set { incident = value; }
        }

        public void addURL(string email, string url)
        {
            urlLinks.Add(new URL(email, url));
        }

        public String urlList
        {
            get
            {
                String list = "";
                foreach (URL u in urlLinks)
                {
                    list = list + u.urlLink + "\n";
                }
                return list;
            }
        }

    }
}
