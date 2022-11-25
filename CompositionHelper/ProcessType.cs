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
        //Definiton for GetType that will process a message according to the header's initial character
        public string GetType(string messageHeader, string messageBody, ref string sender, ref string subject, ref string message)
        {
            //Checking initial character of message header and processing according to that 
            char[] letter= messageHeader.ToCharArray();

            //If header is a tweet splits message and finds sender and message and stores data.
            if(letter.Length > 1 && letter[0].Equals('T'))
            {
                try
                {
                    string[] line = messageBody.Split(' ');
                    if(line[0].Contains("@") && line[0].Length<=15)
                    {
                        sender = line[0];
                        messageBody = messageBody.Remove(0, line[0].Length);

                        message = messageBody;
                    }

                }
                catch(Exception ex)
                {
                    return "Twitter handle invalid or exceeds 15 characters.";
                }

                 return "Tweet Processed.";
            }

            //If header is a SMS splits message and gets Sender phone number and message and stores data
            else if(letter.Length > 1 && letter[0].Equals('S'))
            {
                try
                {
                    string[] line = messageBody.Split(' ');

                    sender = line[0];

                    messageBody = messageBody.Remove(0, line[0].Length);

                    message = messageBody;

                }
                catch(Exception ex)
                {
                    return "No phone number";
                }
                

                
                return "SMS Text Message Processed.";
            }

            //If header is an Email message looks for Sender email address and stores.
            else if(letter.Length > 1 && letter[0].Equals('E'))
            {

                try
                {
                    string[] line = messageBody.Split(' ');
                    if (line[0].Contains("@"))
                    {
                        sender = line[0];
                    }

                }
                catch (Exception ex)
                {
                    return "No email address";
                }


                //Checks if message body is a Serious Incident Report and stores that information
                try
                {
                    string[] line = messageBody.Split(' ');
                    if (line[0].Contains("@"))
                    {
                     messageBody = messageBody.Remove(0, line[0].Length);

                        if (messageBody.Contains("SIR"))
                        {
                            int index = messageBody.IndexOf("SIR");

                            subject = messageBody.Substring(index, 12);

                            messageBody = messageBody.Remove(0, 12);

                            message = messageBody;

                        }
                        else
                        {

                            subject = messageBody.Substring(0, 20);

                            messageBody = messageBody.Remove(0, 20);
                            message = messageBody;


                        }
                    }
                }
                catch(Exception ex)
                {
                    return "No subject";
                }


                //Checks if message body contains any URls and quarantines them for redisplay.
                try
                  {
                        string[] value = message.Split(' ');

                        foreach( string line in value)
                        {
                            if(line.Contains("https") || line.Contains("www."))
                            {
                                int index = message.IndexOf(line);

                                message = message.Remove(index, line.Length);
                                message = message.Insert(index, "<URL Quarantined>");
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        return "Email Processed | No valid URL links found";
                    }
                

                return "Email Processed.";
            }

            return "Invalid Header.";

        }
    }
}
