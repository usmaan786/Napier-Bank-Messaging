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

    //Methods defined for interface IBodyProcess
    public class BodyProcess : IBodyProcess
    {


        //GetHashtag tokenizes body of message by the spaces and obtains hashtags with indexes containing # at the start
        public string[] GetHashtag(string messageBody)
        {
            List<string> list = new List<string>();
            string[] line = messageBody.Split(' ');

            foreach(string value in line)
            {
                if(value.StartsWith("#"))
                {
                    list.Add(value);
                }
            }

            string[] hashtags = list.ToArray();

            return hashtags;
        }

        //GetMention tokenzies body of message by a space character and obtains mentions with indexes containing @ at the start to a list
        public string[] GetMention(string messageBody)
        {
            List<string> list = new List<string>();

            string[] line = messageBody.Split(' ');
            messageBody = messageBody.Remove(0, line[0].Length);

            foreach(string value in line)
            {
                if(value.StartsWith("@") && messageBody.Contains(value))
                {
                    list.Add(value);
                }
            }

            string[] mentions = list.ToArray();

          
            return mentions;
        }


        //GetSortCode searches for the index of the text "Sort Code: " and substrings the line to obtain sort code.
        public string GetSortCode(string messageBody)
        {
            int startIndex = messageBody.IndexOf("Sort Code: ");

            string sortCode = messageBody.Substring(startIndex, 19);

            sortCode = sortCode.Remove(0, 10);

            return sortCode;
        }


        //GetIncident searches for the index of the text "Nature of Incident: " and substrings the line according to any of the incidents within the incidentData array
        public string GetIncident(string messageBody, string[] incidentData)
        {
            string incident=null;
            bool incidentFound = false;

            try
            {
                foreach (string line in incidentData)
                {
                    if (messageBody.Contains("Nature of Incident") && messageBody.Contains(line) && incidentFound == false)
                    {
                        int index = messageBody.IndexOf(line);

                        incident = line;
                        incidentFound = true;

                    }
                }
            }
            catch (Exception ex)
            {
                return "Invalid Nature of Incident - See instructions for help.";
            }

            return incident;
        }


        //GetTextspeak searches through array of textspeak abbreviations to search for and adds "<>" in the body for every abbreviation found as well as an expanded version for later use
        public string GetTextspeak(ref string messageBody, string[] textspeakAbbrev)
        {
            string expandedText = messageBody;

            foreach (string line in textspeakAbbrev)
            {
                string[] value = line.Split(',');

                if (messageBody.Contains(value[0]))
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

        //GetURL tokenizes body of the message by space characters and looks for indexes containing the start of a URL currently hardcoded for https and www and adds to a list
        public string[] GetURL(string messageBody)
        {
            string[] value = messageBody.Split(' ');
            List<string> list = new List<string>();

                foreach (string line in value)
                {
                    if (line.Contains("https") || line.Contains("www."))
                    {
                        list.Add(line);      

                    }
                }

            string[] urlList = list.ToArray();


            return urlList;
        }


    }
}
