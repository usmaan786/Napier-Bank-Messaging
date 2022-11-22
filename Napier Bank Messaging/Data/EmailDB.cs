using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;

namespace Napier_Bank_Messaging
{
    //Database for all Email object classes 
    class EmailDB
    {
        //Creating Email database
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

        //Getting specific Email object
        public Email get(string val)
        {
            return db[val];
        }


        //Allocating a URL input to the specified Email object
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

        //Adding contents of all Email objects with Serious Incident Reports to a list to display on call
        public void SIRList(ref List<string> SIRs, ref List<string>sortCodes, ref List<string>incidents)
        {
            foreach(var item in db.Values)
            {
                if(item.Subject.Contains("SIR"))
                {
                    SIRs.Add(item.Subject);
                    sortCodes.Add(item.SortCode);
                    incidents.Add(item.Incident);
                }
            }



            return;
        }
    }
}