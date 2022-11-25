using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Napier_Bank_Messaging
{
    //Data Singleton layer for a single Tweet database.
    public class tweetSingleton
    {
        private static tweetSingleton reference;

        private tweetSingleton() { }

        //pointing reference to a new instance of emailSingleton if its null
        public static tweetSingleton getInstance()
        {
            if (reference== null)
            
                reference = new tweetSingleton();

                return reference;
            
        }

        private TweetDB db = new TweetDB();

        //Adding Tweet object to DB
        public void addTweet(Tweet t)
        {
            db.add(t.TweetID, t);
        }

       
        public Tweet getTweet(string tweetID)
        {
            return db.get(tweetID);
        }

        //Calling allocateHashtag to add Hashtag to Tweet object
        public void addHashtag(ref string tweetID, string hashtag)
        {
            db.allocateHashtag(ref tweetID, hashtag);
            return;
        }

        //Calling allocateMention to add Mention to Tweet object
        public void addMention(ref string tweetID, string mention)
        {
            db.allocateMention(ref tweetID, mention);
            return;
        }

        //Calling hashtagList to return list of Hashtags.
        public void returnHashtagList(ref List<string>hashtags, ref List<int> count)
        {
            db.hashtagList(ref hashtags, ref count);
            return;
        }

        //Calling mentionList to return list of Mentions.
        public void returnMentionList(ref List<string>mentions)
        {
            db.mentionList(ref mentions);
            return;
        }

    }
}
