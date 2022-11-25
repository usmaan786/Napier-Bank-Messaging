using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contract
{
    //Interface class used for processing header IDs to get the Type and Isolation of body to get Sender, Subject and Message
    public interface IProcess
    {
        string GetType(string messageHeader,string messageBody, ref string sender, ref string subject, ref string message);
    }
}
