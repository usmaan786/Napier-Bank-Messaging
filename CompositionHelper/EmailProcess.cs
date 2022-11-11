using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contract;
using System.ComponentModel.Composition;
using System.IO;

namespace CompositionHelper
{
    [Export(typeof(IBodyProcess))]
    [ExportMetadata("BodyMetaData", "Email")]
    public class EmailProcess : IBodyProcess
    {
        public string GetBody(string messageBody)
        {
            return "Email is from X .";
        }
      
        public string[] GetHashtag(string messageBody)
        {

            string[] hashtags = messageBody.Split(' ', '@');

            return hashtags;
        }
        public string[] GetMention(string messageBody)
        {
            int index = messageBody.IndexOf('@');

            string[] mentions = messageBody.Split(' ', '#');

            return mentions;
        }

        public string GetSortCode(string messageBody)
        {
            int startIndex = messageBody.IndexOf("Sort Code: ");

            string sortCode = messageBody.Substring(startIndex, 19);

            sortCode = sortCode.Remove(0, 10);

            return sortCode;
        }

        public string GetIncident(string messageBody)
        {
            int startIndex = messageBody.IndexOf("Nature of Incident: ");

            string incident = messageBody.Substring(startIndex, 25);

            incident = incident.Remove(0, 19);

            return incident;
        }

        public string GetTextspeak(ref string messageBody, string[] textspeakAbbrev)
        {


            return messageBody;
        }


    }
}
