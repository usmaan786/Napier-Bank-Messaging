using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Napier_Bank_Messaging
{
    public class smsSingleton
    {
        private static smsSingleton reference;

        private smsSingleton() { }

        public static smsSingleton getInstance()
        {
            if (reference == null)

                reference = new smsSingleton();

            return reference;

        }

        private SmsDB db = new SmsDB();

        public void addSMS(SMS s)
        {
            db.add(s.SmsID,s);
        }

        public SMS getSMS(string SmsID)
        {
            return db.get(SmsID);
        }

    }
}
