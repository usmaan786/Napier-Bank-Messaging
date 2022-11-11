using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.Composition;
using Contract;

namespace CompositionHelper
{
    [Export(typeof(IProcess))]
    public class ProcessType : IProcess
    {
        public string GetType(string messageHeader)
        {

            char[] letter= messageHeader.ToCharArray();
            if(letter.Length > 1 && letter[0].Equals('T'))
            {
                 return "This is a Tweet.";
            }
            else if(letter.Length > 1 && letter[0].Equals('S'))
            {
                return "This is a SMS Text Message.";
            }
            else if(letter.Length > 1 && letter[0].Equals('E'))
            {
                return "This is an Email.";
            }

            return "Didn't return value.";

        }
    }
}
