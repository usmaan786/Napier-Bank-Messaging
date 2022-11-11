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
    [ExportMetadata("BodyMetaData", "SMS")]
    public class SmsProcess : IBodyProcess
    {
        public string GetBody(string messageBody)
        {
            return "SMS message from number X.";
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
            string expandedText = messageBody;

            foreach(string line in textspeakAbbrev)
            {
                string[] value = line.Split(',');

                if(messageBody.Contains(value[0]))
                {
                    int index = messageBody.IndexOf(value[0]);

                    index = index + value[0].Length;

                    messageBody = messageBody.Insert(index, "<>");

                    index = expandedText.IndexOf(value[0]);
                    index = index + value[0].Length;

                    expandedText = expandedText.Insert(index, "<" + value[1] + ">");


                }
            }

            return expandedText;
        }

    }
}
