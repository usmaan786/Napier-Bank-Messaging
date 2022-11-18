using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Napier_Bank_Messaging
{
    public class tweetSingleton
    {
        private static tweetSingleton reference;

        private tweetSingleton() { }

        public static tweetSingleton getInstance()
        {
            if (reference== null)
            
                reference = new tweetSingleton();

                return reference;
            
        }

        private TweetDB db = new TweetDB();

        public void addTweet(Tweet t)
        {
            db.add(t.TweetID, t);
        }

        public Tweet getTweet(string tweetID)
        {
            return db.get(tweetID);
        }

        public void addHashtag(ref string tweetID, string hashtag)
        {
            db.allocateHashtag(ref tweetID, hashtag);
            return;
        }

        public void addMention(ref string tweetID, string mention)
        {
            db.allocateMention(ref tweetID, mention);
            return;
        }

        public void returnHashtagList(ref List<string>hashtags, ref List<int> count)
        {
            db.hashtagList(ref hashtags, ref count);
            return;
        }

        public void returnMentionList(ref List<string>mentions)
        {
            db.mentionList(ref mentions);
            return;
        }

    }
}
