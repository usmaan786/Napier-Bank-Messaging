using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Napier_Bank_Messaging
{
    class URL
    {
        private string email;
        private string link;

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
