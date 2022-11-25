using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Napier_Bank_Messaging
{
    //Data Singleton layer for a single SMS database.
    public class smsSingleton
    {
        private static smsSingleton reference;

        private smsSingleton() { }

        //pointing reference to a new instance of smsSingleton if its null
        public static smsSingleton getInstance()
        {
            if (reference == null)

                reference = new smsSingleton();

            return reference;

        }

        private SmsDB db = new SmsDB();

        //Adding SMS object to DB
        public void addSMS(SMS s)
        {
            db.add(s.SmsID,s);
        }

        //Getting SMS object from DB
        public SMS getSMS(string SmsID)
        {
            return db.get(SmsID);
        }

    }
}
