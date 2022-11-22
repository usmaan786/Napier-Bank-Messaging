using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Napier_Bank_Messaging
{
    //Class Object for Emails - also creates multiple instances of class URL to store multiple URL objects within an Email object
    public class Email
    {

        private string id;
        private string sender;
        private string subject;
        private string message;

        private string sortCode;
        private string incident;

        private List<URL> urlLinks = new List<URL>();
        public string emailID
        {
            get { return id; }
            set { id = value; }
        }

        public string Sender
        {
            get { return sender; }
            set { sender = value; }
        }

        public string Subject
        {
            get { return subject; }
            set { subject = value; }
        }

        public string Message
        {
            get { return message; }
            set { message = value; }
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
                    list = list + u.urlLink + " ";
                }
                return list;
            }
        }

    }
}
