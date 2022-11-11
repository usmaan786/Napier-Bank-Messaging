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
            TestButton = "Test";
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

            var result = helper.Execute(MessageHeaderText.ToString());

            var method = GetMethod(MessageHeaderText.ToString());

            bodyHelper = new BodyCompositionHelper();
            bodyHelper.AssembleBodyComponents();

            //var bodyProcess = bodyHelper.Execute(MessageBodyText.ToString(), method);
            string[] hashtagList=null;
            string[] mentionList = null;
            string sortCode = null;
            string incident = null;
            //need to fix if statement
            if(method == "Tweet")
            {
                hashtagList = bodyHelper.ExecuteHashtag(MessageBodyText.ToString(), method);
                mentionList = bodyHelper.ExecuteMention(MessageBodyText.ToString(), method);

                tweetSave(hashtagList,mentionList);

                
            }
            else if(method == "Email")
            {
                if(MessageBodyText.StartsWith("SIR"))
                {
                    sortCode = bodyHelper.ExecuteSIR(MessageBodyText.ToString(), method);
                    incident = bodyHelper.ExecuteIncident(MessageBodyText.ToString(), method);
                    emailSave(sortCode,incident);
                }
                
            }

            MessageBox.Show(string.Format(result + incident));
        }

        private void ExpandButtonClick()
        {
            string[] textspeak = File.ReadAllLines(@"F:\Software Coursework 2022\textwords (1).csv");
            if (Formatted== false)
            {
                string expandedText = ExpandedText;
                string originalText = MessageBodyText;

                bodyHelper = new BodyCompositionHelper();
                bodyHelper.AssembleBodyComponents();

                expandedText = bodyHelper.ExecuteTextspeak(ref originalText, textspeak, "SMS");

                MessageBodyText = originalText;
                ExpandedText = expandedText;
                OriginalText = originalText;
                OnChanged(nameof(MessageBodyText));

                Formatted = true;

             return;
            }



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

        public void tweetSave(string[] hashtagList, string[] mentionList)
        {
            Tweet newTweet = new Tweet();
            //process in TweetProcess.cs and return values for newTweet object to store.
            newTweet.tweetID = MessageHeaderText.ToString();
            foreach(var hashtag in hashtagList)
            {
                if (hashtag.StartsWith("#"))
                {
                    newTweet.Hashtag = hashtag;
                }
            }
            foreach(var mention in mentionList)
            {
                if (mention.StartsWith("@"))
                {
                    newTweet.Mention = mention;
                }
                
            }
           
            tweetSingleton ts = tweetSingleton.getInstance();
            ts.addTweet(newTweet);
        }

        private void TestButtonClick()
        {
            //emailSerialization();
        }

        private void emailSave(string sortCode, string incident)
        {
            Email newEmail = new Email();

            string url = "https://test.com";
            string emailID = MessageHeaderText.ToString();

            /// Need to add validation for all inputs here later.
            newEmail.emailID = MessageHeaderText.ToString();
            newEmail.SIR = true;
            newEmail.SortCode = sortCode;
            newEmail.Incident = incident;

            emailSingleton es = emailSingleton.getInstance();
            es.addEmail(newEmail);
            es.addURL(ref emailID, url);

        }

        /*public void emailSerialization()
        {
            Email newEmail = new Email();

            emailSingleton es = emailSingleton.getInstance();
            newEmail = es.getEmail("E123");

            string fileName = "Email.json";
            using FileStream createStream = File.Create(fileName);
            string jsonString = JsonSerializer.Serialize<Email>(newEmail);
            //File.WriteAllText(fileName, jsonString);

            MessageBox.Show(jsonString);

        }*/


    }
}
