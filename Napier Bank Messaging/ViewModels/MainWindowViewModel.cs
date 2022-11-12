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

namespace Napier_Bank_Messaging.ViewModels
{
    public class MainWindowViewModel : BaseViewModel
    {
        

        ProcessCompositionHelper helper;
        BodyCompositionHelper bodyHelper;
        public string MessageHeaderBlock { get; private set; }
        public string MessageBodyBlock { get; private set; }

        public string MessageHeaderText { get; set; }
        public string MessageBodyText { get; set; }
        public string ProcessButton { get; private set; }
        public string TestButton { get; private set; }
        public string ClearButton { get; private set; }
        public string ExpandTextButton { get; private set; }

        public ICommand ProcessButtonCommand { get; private set; }
        public ICommand TestButtonCommand { get; private set; }
        public ICommand ExpandTextCommand { get; private set; }

        public ICommand ClearButtonCommand { get; private set; }

        public string ExpandedText { get; private set; }
        public string OriginalText { get; private set; }

        public bool Formatted { get; private set; }
        public MainWindowViewModel()
        {
            MessageHeaderBlock = "Header";
            MessageBodyBlock = "Body";

            ProcessButton = "Process";
            ExpandTextButton = "Expand Text";
            ClearButton = "Clear";

            MessageHeaderText = string.Empty;
            MessageBodyText = string.Empty;

            ExpandedText = MessageBodyText;
            OriginalText = MessageBodyText;

            Formatted = false;

            ProcessButtonCommand = new RelayCommand(ProcessButtonClick);
            TestButtonCommand = new RelayCommand(TestButtonClick);
            ExpandTextCommand = new RelayCommand(ExpandButtonClick);
            ClearButtonCommand = new RelayCommand(ClearButtonClick);

        }

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

        private void ProcessButtonClick()
        {
            

            helper = new ProcessCompositionHelper();
            helper.AssembleProcessComponents();

            var result = "";

            var method = GetMethod(MessageHeaderText.ToString());

            bodyHelper = new BodyCompositionHelper();
            bodyHelper.AssembleBodyComponents();

            //var bodyProcess = bodyHelper.Execute(MessageBodyText.ToString(), method);

            string sender = null;
            string subject = null;
            string message = null;

            //need to fix if statement
            if (method == "Tweet")
            {
                string[] hashtagList = null;
                string[] mentionList = null;

                result = helper.Execute(MessageHeaderText.ToString(), MessageBodyText.ToString(), ref sender, ref subject, ref message);
                hashtagList = bodyHelper.ExecuteHashtag(MessageBodyText.ToString());
                mentionList = bodyHelper.ExecuteMention(MessageBodyText.ToString());

                ExpandText();

                tweetSave(sender, message, hashtagList, mentionList);

            }
            else if(method == "Email")
            {
                result = helper.Execute(MessageHeaderText.ToString(), MessageBodyText.ToString(),ref sender, ref subject, ref message);
                if(subject.Contains("SIR"))
                {
                    string[] incidentData = File.ReadAllLines(@"F:\Software Coursework 2022\incident.csv");
                    string sortCode = null;
                    string incident = null;
                    string[] urlList = null;

                    urlList = bodyHelper.ExecuteURL(MessageBodyText.ToString());
                    sortCode = bodyHelper.ExecuteSIR(MessageBodyText.ToString());
                    incident = bodyHelper.ExecuteIncident(MessageBodyText.ToString(),incidentData);

                    emailSave(sender,subject,message,sortCode, incident,urlList);
                    
                }
                else
                {
                    string[] urlList = null;

                    urlList = bodyHelper.ExecuteURL(MessageBodyText.ToString());

                    emailSave(sender,subject,message,"N/A", "N/A",urlList);
                }
            }
            else if(method == "SMS")
            {
                result = helper.Execute(MessageHeaderText.ToString(), MessageBodyText.ToString(), ref sender, ref subject, ref message);
                ExpandText();

                smsSave(sender, message);
            }
            

            MessageBox.Show(string.Format(result));
        }

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

        public void smsSave(string sender, string message)
        {
            string smsID = MessageHeaderText.ToString();

            smsSingleton ss = smsSingleton.getInstance();
            SMS newSMS = new SMS();
            //process in TweetProcess.cs and return values for newTweet object to store.
            newSMS.SmsID = smsID;
            newSMS.Sender = sender;
            newSMS.Message = message;

            ss.addSMS(newSMS);

            smsSerialization(newSMS);


        }

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
                //createStream.Dispose();

            }
            //using FileStream createStream = File.Create(fileName);

        }

        public void tweetSave(string sender, string message, string[] hashtagList, string[] mentionList)
        {
            string tweetID = MessageHeaderText.ToString();

            tweetSingleton ts = tweetSingleton.getInstance();
            Tweet newTweet = new Tweet();
            //process in TweetProcess.cs and return values for newTweet object to store.
            newTweet.TweetID = tweetID;
            newTweet.Sender = sender;
            newTweet.Message = message;

            ts.addTweet(newTweet);

            foreach (var hashtag in hashtagList)
            {
                ts.addHashtag(ref tweetID, hashtag);
            }
            foreach(var mention in mentionList)
            {
                ts.addMention(ref tweetID, mention);
                
            }

            tweetSerialization(newTweet);
           

        }
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
                //createStream.Dispose();

            }
            //using FileStream createStream = File.Create(fileName);

        }


        private void TestButtonClick()
        {
            //emailSerialization();
        }

        private void emailSave(string sender, string subject, string message, string sortCode, string incident, string[] urlList)
        {
            Email newEmail = new Email();

            string emailID = MessageHeaderText.ToString();

            /// Need to add validation for all inputs here later.
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
            

            emailSerialization(newEmail);
        }

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
                //createStream.Dispose();

            }
            //using FileStream createStream = File.Create(fileName);

        }


    }
}
