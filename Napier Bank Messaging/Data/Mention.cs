using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Napier_Bank_Messaging
{
    class Mention
    {
        private string tweet;
        private string mention;

        public Mention(string tweet, string mention)
        {
            TweetID = tweet;
            MentionString = mention;
        }
        public string TweetID
        {
            get { return tweet; }
            set { tweet = value; }
        }

        public string MentionString
        {
            get { return mention; }
            set { mention = value; }
        }

    }
}
