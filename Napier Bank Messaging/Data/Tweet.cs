using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Napier_Bank_Messaging
{
    //Class Object for Tweet - also creates multiple instances of class Hashtag and Mention to store multiple Hashtag and Mention objects within an Email object
    public class Tweet
    {
        private string id;
        private string sender;
        private string message;

        private List<Hashtag> hashtags = new List<Hashtag>();
        private List<Mention> mentions = new List<Mention>();

        public string TweetID
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

        public void addHashtag(string tweet, string hashtag)
        {
            hashtags.Add(new Hashtag(tweet, hashtag));
        }

        public void addMention(string tweet, string mention)
        {
            mentions.Add(new Mention(tweet, mention));  
        }

        public String HashtagList
        {
            get
            {
                String list = "";
                foreach (Hashtag h in hashtags)
                {
                    list = list + h.HashtagString + " ";
                }
                return list;
            }
        }

        public String MentionList
        {
            get
            {
                String list = "";
                foreach (Mention m in mentions)
                {
                    list = list + m.MentionString + " ";
                }
                return list;
            }
        }

    }
}
