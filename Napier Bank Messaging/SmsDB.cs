using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Napier_Bank_Messaging
{
    class SmsDB
    {
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

        public SMS get(string val)
        {
            return db[val];
        }

    }
}
