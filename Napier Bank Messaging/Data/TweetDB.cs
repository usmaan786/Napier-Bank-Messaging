using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Napier_Bank_Messaging
{
    class TweetDB
    {
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

        public Tweet get(string val)
        {
            return db[val];
        }


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
