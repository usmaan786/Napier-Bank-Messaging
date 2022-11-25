using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Napier_Bank_Messaging
{
    //Database for all SMS object classes 
    class SmsDB
    {
        //Creating SMS database
        private Dictionary<string, SMS> db = new Dictionary<string, SMS>();

        public void add(string key, SMS val)
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

        //Getting specific SMS object
        public SMS get(string val)
        {
            return db[val];
        }

    }
}
