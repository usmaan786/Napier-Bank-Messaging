using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Napier_Bank_Messaging
{
    public class emailSingleton
    {
        private static emailSingleton reference;

        private emailSingleton() { }

        public static emailSingleton getInstance()
        {
            if (reference == null)

                reference = new emailSingleton();

            return reference;

        }

        private EmailDB db = new EmailDB();

        public void addEmail(Email e)
        {
            db.add(e.emailID, e);
        }

        public Email getEmail(string emailID)
        {
            return db.get(emailID);
        }
        public String getList
        {
            get
            {
                String emailList = "";
                db.returnListTest(ref emailList);
                return emailList;
            }
        }

        public void addURL(ref string emailID, string url)
        {
            db.allocateURL(ref emailID, url);
            return;
        }
    }
}
