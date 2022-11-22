using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Napier_Bank_Messaging
{
    //Class object for URLs to be used within an Email object
    class URL
    {
        private string email;
        private string link;

        //Constructor to be used to create URL object
        public URL(string email, string url)
        {
            EmailID = email;
            urlLink = url;
        }
        public string EmailID
        {
            get { return email; }
            set { email = value; }
        }

        public string urlLink
        {
            get { return link ; }
            set { link = value; }
        }

    }
}
