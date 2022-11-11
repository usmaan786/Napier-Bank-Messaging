using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Napier_Bank_Messaging
{
    public class SMS
    {
        private string id;
        private string sender;
        private string message;

        public string SmsID
        {
            get { return id; }
            set { id = value; }
        }

        public string Sender
        {
            get { return sender; }
            set { sender = value; }
        }

        public string Message
        {
            get { return message; }
            set { message = value; }
        }
    }
}
