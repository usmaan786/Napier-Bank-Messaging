using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;

namespace Napier_Bank_Messaging
{
    class EmailDB
    {
        private Dictionary<string, Email> db = new Dictionary<string, Email>();

        public void add(string key, Email val)
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

        public Email get(string val)
        {
            return db[val];
        }

        public void returnListTest(ref String emailList)
        {
            foreach (var item in db.Values)
            {
                emailList = emailList + "SIR - " + item.SIR + " " + item.SortCode + " " + item.Incident + " " + "\n";
            }
            return;
        }

        public void allocateURL(ref string emailID, string url)
        {
            foreach(var item in db.Values)
            {
                if(item.emailID == emailID)
                {
                    item.addURL(emailID, url);
                }
            }
            return;
        }
    }
}