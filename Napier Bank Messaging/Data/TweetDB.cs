using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Napier_Bank_Messaging
{
    //Database for all Tweet object classes 

    class TweetDB
    {
        //Creating Tweet database

        private Dictionary<string, Tweet> db = new Dictionary<string, Tweet>();

        public void add(string key, Tweet val)
        {
            if (db.ContainsKey(key))
            {
                db[key] = val;
            }
            else
            {
                db.Add(key, val);
            }
        }

        //Getting specific Tweet object
        public Tweet get(string val)
        {
            return db[val];
        }

        //Allocating a Hashtag input to the specified Email object
        public void allocateHashtag(ref string tweetID, string hashtag)
        {
            foreach (var item in db.Values)
            {
                if (item.TweetID == tweetID)
                {
                    item.addHashtag(tweetID, hashtag);
                }
            }
            return;
        }

        //Allocating a Mention input to the specified Email object
        public void allocateMention(ref string tweetID, string mention)
        {
            foreach (var item in db.Values)
            {
                if (item.TweetID == tweetID)
                {
                    item.addMention(tweetID, mention);
                }
            }
            return;
        }

        //Adding contents of all Tweet objects with Hashtags to a list to display on call
        public void hashtagList(ref List<string> hashtags, ref List<int> count)
        {
         
            foreach(var item in db.Values)
            {
                string[] list = item.HashtagList.Split(' ');
                foreach(string line in list)
                {
                    if(!hashtags.Contains(line))
                    {
                        hashtags.Add(line);
                        count.Add(1);
                    }
                    else
                    {
                        int index = hashtags.IndexOf(line);

                        count[index]++;
                    }
                }
              
            }
            return;
        }

        //Adding contents of all Tweet objects with Mentions to a list to display on call
        public void mentionList(ref List<string> mentions)
        {
            foreach(var item in db.Values)
            {
                string[] list = item.MentionList.Split(' ');
                foreach(string line in list)
                {
                    mentions.Add(line);
                }

            }
            return;
        }


    }
}
