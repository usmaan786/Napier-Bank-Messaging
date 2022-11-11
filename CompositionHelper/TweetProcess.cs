using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contract;
using System.ComponentModel.Composition;
using System.Text.RegularExpressions;
using System.IO;

namespace CompositionHelper
{
    [Export(typeof(IBodyProcess))]
    [ExportMetadata("BodyMetaData", "Tweet")]
    public class TweetProcess : IBodyProcess
    {
        public string GetBody(string messageBody)
        {
            string pattern = "(#)";
            string[] hashtags = Regex.Split(messageBody,pattern);

            return hashtags[1]+hashtags[2];
        }

        public string[] GetHashtag(string messageBody)
        {
            
            string[] hashtags = messageBody.Split(' ','@');

            return hashtags;
        }
        public string[] GetMention(string messageBody)
        {
            int index = messageBody.IndexOf('@');

            string[] mentions = messageBody.Split(' ','#');

            return mentions;
        }
        public string GetSortCode(string messageBody)
        {
            int startIndex = messageBody.IndexOf(": ");

            string sortCode = messageBody.Substring(startIndex, 10);

            sortCode = sortCode.Remove(0, 1);

            return sortCode;
        }

        public string GetIncident(string messageBody)
        {
            int startIndex = messageBody.IndexOf("Nature of Incident: ");

            string incident = messageBody.Substring(startIndex, 29);

            incident = incident.Remove(0, 19);

            return incident;
        }

        public string GetTextspeak(ref string messageBody, string[] textspeakAbbrev)
        {


            return messageBody;
        }

        // Code to process tweet body into hashtags and mentions under here.
    }
}
