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
            db.add(t.tweetID, t);
        }

        public Tweet getTweet(string tweetID)
        {
            return db.get(tweetID);
        }
        public String getList
        {
            get
            {
                String tweetList = "";
                db.returnList(ref tweetList);
                return tweetList;
            }
        }

    }
}
