using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Napier_Bank_Messaging
{
    //Class object for Hashtags to be used within a Tweet object
    class Hashtag
    {
        private string tweet;
        private string hashtag;

        //Constructor to be used to create Hashtag object
        public Hashtag(string tweet, string hashtag)
        {
            TweetID = tweet;
            HashtagString = hashtag;
        }
        public string TweetID
        {
            get { return tweet; }
            set { tweet = value; }
        }

        public string HashtagString
        {
            get { return hashtag; }
            set { hashtag = value; }
        }

    }
}
