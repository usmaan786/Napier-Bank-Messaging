﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Napier_Bank_Messaging
{
    public class Tweet
    {
        private string id;
        private string sender;
        private string message;
        private string mention;
        private string hashtag;

        public string tweetID
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
        public string Mention
        {
            get { return mention; }
            set { mention = value; }
        }

        public string Hashtag
        {
            get { return hashtag; }
            set { hashtag = value; }
        }

    }
}