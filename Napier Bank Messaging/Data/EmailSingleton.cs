using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Napier_Bank_Messaging
{
    //Data Singleton layer for a single Email database.
    public class emailSingleton
    {
        private static emailSingleton reference;

        private emailSingleton() { }

        //pointing reference to a new instance of emailSingleton if its null
        public static emailSingleton getInstance()
        {
            if (reference == null)

                reference = new emailSingleton();

            return reference;

        }

        private EmailDB db = new EmailDB();


        //Adding Email object to DB
        public void addEmail(Email e)
        {
            db.add(e.emailID, e);
        }

        //Getting Email object from DB
        public Email getEmail(string emailID)
        {
            return db.get(emailID);
        }

        //Calling allocateURL to add url to email object
        public void addURL(ref string emailID, string url)
        {
            db.allocateURL(ref emailID, url);
            return;
        }

        //Calling SIRList to return list of Serious Incident Reports.
        public void returnSIR(ref List<string> SIRs, ref List<string> sortCodes, ref List<string> incidents)
        {
            db.SIRList(ref SIRs, ref sortCodes, ref incidents);
            return;
        }
    }
}
