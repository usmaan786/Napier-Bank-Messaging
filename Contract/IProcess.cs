using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contract
{
    public interface IProcess
    {
        string GetType(string messageHeader,string messageBody, ref string sender, ref string subject, ref string message);
    }
}
