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
        public string GetType(string messageHeader, string messageBody, ref string sender, ref string subject, ref string message)
        {

            char[] letter= messageHeader.ToCharArray();
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




                 return "This is a Tweet.";
            }
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
                

                
                return "This is a SMS Text Message.";
            }
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







                return "This is an Email.";
            }

            return "Didn't return value.";

        }
    }
}
