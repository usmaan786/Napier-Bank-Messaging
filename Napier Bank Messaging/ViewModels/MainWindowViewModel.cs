using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Napier_Bank_Messaging.Commands;
using System.Windows;
using System.Windows.Input;
using CompositionHelper;
using System.Diagnostics;
using System.IO;
using System.Text.Json.Serialization;
using System.Text.Json;
using Napier_Bank_Messaging.Business;

//Author - Usmaan Chohan
//Matriculation - 40485296
//Solution - This solution serves as a WPF program that takes in a Header Input and Message Input to sanitize and redisplay Tweets, Emails (both a standard and Serious Incident Report) and SMS Text Messages.
//The solution also saves the details of all of the above message types to a JSON file "Messages.JSON" as well as provides functionality to expand textspeak abbreviations and provides a summary of all the details in messages


namespace Napier_Bank_Messaging.ViewModels
{
    public class MainWindowViewModel : BaseViewModel
    {
        //Setting Variables for Display and Main Functionalities

        //settings variables for interface composition helper functions
        ProcessCompositionHelper helper;
        BodyCompositionHelper bodyHelper;

        //Setting variables for text to display on WPF application
        public string MessageHeaderBlock { get; private set; }
        public string MessageBodyBlock { get; private set; }

        public string MessageHeaderText { get; set; }
        public string MessageBodyText { get; set; }
        public string ProcessButton { get; private set; }
        public string TestButton { get; private set; }
        public string ClearButton { get; private set; }
        public string ExpandTextButton { get; private set; }
        public string ExpandedText { get; private set; }
        public string OriginalText { get; private set; }
        public string EndButton { get; private set; }


        //Setting variables for button commands to call
        public ICommand ProcessButtonCommand { get; private set; }
        public ICommand ExpandTextCommand { get; private set; }

        public ICommand ClearButtonCommand { get; private set; }

        public ICommand EndButtonCommand { get; private set; }


        public bool Formatted { get; private set; }
        public MainWindowViewModel()
        {
            //Setting string contents for all on screen displayed variables
            MessageHeaderBlock = "Header";
            MessageBodyBlock = "Body";

            ProcessButton = "Process";
            ExpandTextButton = "Expand Text";
            ClearButton = "Clear";
            EndButton = "End Session";

            MessageHeaderText = string.Empty;
            MessageBodyText = string.Empty;

            ExpandedText = MessageBodyText;
            OriginalText = MessageBodyText;

            Formatted = false;

            //Execution of commands on button clicks
            ProcessButtonCommand = new RelayCommand(ProcessButtonClick);
            ExpandTextCommand = new RelayCommand(ExpandButtonClick);
            ClearButtonCommand = new RelayCommand(ClearButtonClick);
            EndButtonCommand = new RelayCommand(EndButtonClick);

        }

        //End button that displays the Summary window on click
        private void EndButtonClick()
        {
            Summary summaryWin = new Summary();
            summaryWin.ShowDialog();
        }

        //Clear button that resets all values on click to input a new header/message
        private void ClearButtonClick()
        {
            MessageBodyText = string.Empty;
            MessageHeaderText = string.Empty;

            ExpandedText = MessageBodyText;
            OriginalText = MessageBodyText;

            Formatted = false;

            OnChanged(nameof(MessageBodyText));
            OnChanged(nameof(MessageHeaderText));
        }

        //Process button provides main functionality to the program
        private void ProcessButtonClick()
        {
            //Assembling all components required
            helper = new ProcessCompositionHelper();
            helper.AssembleProcessComponents();

            var result = "";

            //Getting the type of message according to Message header
            var method = GetMethod(MessageHeaderText.ToString());

            bodyHelper = new BodyCompositionHelper();
            bodyHelper.AssembleBodyComponents();


            string sender = null;
            string subject = null;
            string message = null;

            //If tweet call according helper Execute functions accordingly to find Sender, Message and all Hashtags and Mentions
            if (method == "Tweet")
            {
                string[] hashtagList = null;
                string[] mentionList = null;

                result = helper.Execute(MessageHeaderText.ToString(), MessageBodyText.ToString(), ref sender, ref subject, ref message);
                hashtagList = bodyHelper.ExecuteHashtag(MessageBodyText.ToString());
                mentionList = bodyHelper.ExecuteMention(MessageBodyText.ToString());

                //Calling ExpandText to find any textspeak abbreviations
                ExpandText();
                 
                //Calling tweetSave to save all contents of current tweet
                tweetSave(sender, message, hashtagList, mentionList);

            }

            //If Email call according helper Execute functions accordingly to find Sender,Subject, Message and all URLs
            else if (method == "Email")
            {
                result = helper.Execute(MessageHeaderText.ToString(), MessageBodyText.ToString(),ref sender, ref subject, ref message);

                //If the email is a SIR process accordingly and find Sort Code and Nature of Incident
                if(subject.Contains("SIR"))
                {
                    string[] incidentData = File.ReadAllLines(@"F:\Software Coursework 2022\incident.csv");
                    string sortCode = null;
                    string incident = null;
                    string[] urlList = null;

                    urlList = bodyHelper.ExecuteURL(MessageBodyText.ToString());
                    sortCode = bodyHelper.ExecuteSIR(MessageBodyText.ToString());
                    incident = bodyHelper.ExecuteIncident(MessageBodyText.ToString(),incidentData);

                    //Redisplaying sanitized email
                    MessageBodyText = sender + subject + message;
                    OnChanged(nameof(MessageBodyText));

                    //Calling emailSave to save all contents of current email
                    emailSave(sender,subject,message,sortCode, incident,urlList);
                    
                }
                //else if not a SIR just find the Sender, Subject and Message and Save that.
                else
                {
                    string[] urlList = null;

                    urlList = bodyHelper.ExecuteURL(MessageBodyText.ToString());

                    MessageBodyText = sender + subject + message;
                    OnChanged(nameof(MessageBodyText));

                    emailSave(sender,subject,message,"N/A", "N/A",urlList);
                }
            }

            //If SMS call according helper Execute functions accordingly to find Sender and Message
            else if(method == "SMS")
            {
                result = helper.Execute(MessageHeaderText.ToString(), MessageBodyText.ToString(), ref sender, ref subject, ref message);

                //Calling ExpandText to find any textspeak abbreviations
                ExpandText();

                //Calling smsSave to save all contents of current SMS
                smsSave(sender, message);
            }
            

            MessageBox.Show(string.Format(result));
        }

        //ExpandText inputs a CSV file of textspeak abbreviations and calls helper functions to find those abbreviations and sanitize message for redisplay
        public void ExpandText()
        {
            string[] textspeak = File.ReadAllLines(@"F:\Software Coursework 2022\textwords (1).csv");
            if (Formatted == false)
            {
                string expandedText = ExpandedText;
                string originalText = MessageBodyText;

                bodyHelper = new BodyCompositionHelper();
                bodyHelper.AssembleBodyComponents();

                expandedText = bodyHelper.ExecuteTextspeak(ref originalText, textspeak);

                MessageBodyText = originalText;
                ExpandedText = expandedText;
                OriginalText = originalText;
                OnChanged(nameof(MessageBodyText));

                Formatted = true;

                return;
            }
        }

        //ExpandButtonClick provides functionality if ExpandText finds any textspeak abbreviations and on click displays either a message with unexpanded abbreviations or an expanded one
        private void ExpandButtonClick()
        {

            if (MessageBodyText.Equals(OriginalText) && Formatted==true)
            {
                MessageBodyText = ExpandedText;
                OnChanged(nameof(MessageBodyText));

            }
            else if(MessageBodyText.Equals(ExpandedText) && Formatted==true)
            {
                MessageBodyText = OriginalText;
                OnChanged(nameof(MessageBodyText));
            }

        }

        //GetMethod finds the message type
        public string GetMethod(string method)
        {
            char[] letter = method.ToCharArray();
            if (letter.Length > 1 && letter[0].Equals('T'))
            {
                return "Tweet";
            }
            else if (letter.Length > 1 && letter[0].Equals('S'))
            {
                return "SMS";
            }
            else if (letter.Length > 1 && letter[0].Equals('E'))
            {
                return "Email";
            }
            return "Invalid";
        }

        //Gets instance of smsSingleton and adds a SMS object to that DB
        public void smsSave(string sender, string message)
        {
            string smsID = MessageHeaderText.ToString();

            smsSingleton ss = smsSingleton.getInstance();
            SMS newSMS = new SMS();
            newSMS.SmsID = smsID;
            newSMS.Sender = sender;
            newSMS.Message = message;

            ss.addSMS(newSMS);

            //Serializing current object now
            smsSerialization(newSMS);


        }

        //Serializes current SMS object in the program and appends it to Messages.JSON (currently located in debug folder). If file does not exist then it creates it
        public static async void smsSerialization(SMS newSMS)
        {

            string fileName = "Messages.json";
            string jsonString = JsonSerializer.Serialize(newSMS);

            if (!File.Exists(fileName))
            {
                using FileStream createStream = File.Create(fileName);
                await JsonSerializer.SerializeAsync(createStream, newSMS);
                createStream.Dispose();
            }
            else
            {
                using StreamWriter appendStream = File.AppendText(fileName);
                appendStream.WriteLine("\\n");
                appendStream.WriteLine(jsonString);
                appendStream.Dispose();

            }

        }


        //Gets instance of emailSingleton and adds a Email object to that DB along with list of URLs if exists
        private void emailSave(string sender, string subject, string message, string sortCode, string incident, string[] urlList)
        {
            Email newEmail = new Email();

            string emailID = MessageHeaderText.ToString();
            newEmail.emailID = emailID;
            newEmail.Sender = sender;
            newEmail.Subject = subject;
            newEmail.Message = message;
            newEmail.SortCode = sortCode;
            newEmail.Incident = incident;

            emailSingleton es = emailSingleton.getInstance();
            es.addEmail(newEmail);
            foreach(string value in urlList)
            {
                es.addURL(ref emailID, value);
            }

            //Serializing current object now
            emailSerialization(newEmail);
        }

        //Serializes current Email object in the program and appends it to Messages.JSON (currently located in debug folder). If file does not exist then it creates it
        public static async void emailSerialization(Email newEmail)
        {

            string fileName = "Messages.json";
            string jsonString = JsonSerializer.Serialize(newEmail);

            if (!File.Exists(fileName))
            {
                using FileStream createStream = File.Create(fileName);
                await JsonSerializer.SerializeAsync(createStream, newEmail);
                createStream.Dispose();
            }
            else
            {
                using StreamWriter appendStream = File.AppendText(fileName);
                appendStream.WriteLine("\\n");
                appendStream.WriteLine(jsonString);
                appendStream.Dispose();
            }

        }

        //Gets instance of tweetSingleton and adds a Tweet object to that DB along with list of Hashtags and Mentions if exists
        public void tweetSave(string sender, string message, string[] hashtagList, string[] mentionList)
        {
            string tweetID = MessageHeaderText.ToString();

            tweetSingleton ts = tweetSingleton.getInstance();
            Tweet newTweet = new Tweet();
            newTweet.TweetID = tweetID;
            newTweet.Sender = sender;
            newTweet.Message = message;

            ts.addTweet(newTweet);

            foreach (var hashtag in hashtagList)
            {
                ts.addHashtag(ref tweetID, hashtag);
            }
            foreach (var mention in mentionList)
            {
                ts.addMention(ref tweetID, mention);

            }

            //Serializing current object now
            tweetSerialization(newTweet);


        }

        //Serializes current Tweet object in the program and appends it to Messages.JSON (currently located in debug folder). If file does not exist then it creates it
        public static async void tweetSerialization(Tweet newTweet)
        {

            string fileName = "Messages.json";
            string jsonString = JsonSerializer.Serialize(newTweet);

            if (!File.Exists(fileName))
            {
                using FileStream createStream = File.Create(fileName);
                await JsonSerializer.SerializeAsync(createStream, newTweet);
                createStream.Dispose();
            }
            else
            {
                using StreamWriter appendStream = File.AppendText(fileName);
                appendStream.WriteLine("\\n");
                appendStream.WriteLine(jsonString);
                appendStream.Dispose();
            }


        }


    }
}
