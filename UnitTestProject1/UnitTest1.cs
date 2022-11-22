using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using CompositionHelper;
using System.IO;

namespace UnitTestProject1
{
    //Unit Test for the Interface Methods.
    [TestClass]
    public class UnitTest1
    {

        ProcessCompositionHelper helper;
        BodyCompositionHelper bodyHelper;

        //Testing Handling of Tweets for isolating the Tweet Type, Sender Handler and Message.
        [TestMethod]
        public void TestTweetHandling()
        {
            helper = new ProcessCompositionHelper();
            helper.AssembleProcessComponents();

            string expectedHeader = "Tweet Processed.";
            string expectedSender = "@Usmaan";
            string expectedMessage = " this is a #Test #NapierBanking";

            string inputHeader = "T290680";
            string inputBody = "@Usmaan this is a #Test #NapierBanking";
            string sender = "";
            string subject = "";
            string message = "";

            var result = helper.Execute(inputHeader, inputBody, ref sender, ref subject, ref message);

            Assert.AreEqual(expectedHeader,result);
            Assert.AreEqual(expectedSender, sender);
            Assert.AreEqual(expectedMessage, message);
        }

        //Testing Handling of Tweet Message Body. Isolation of Hashtags and Mentions.
        [TestMethod]
        public void TestTweetBody()
        {
            bodyHelper = new BodyCompositionHelper();
            bodyHelper.AssembleBodyComponents();

            string messageBody = "@Usmaan this is a #Test #NapierBanking check this out @NBMofficial @ENUCS";

            string[] hashtagList = null;
            string[] mentionList = null;

            string[] expectedHashtag = { "#Test", "#NapierBanking" };
            string[] expectedMention = { "@NBMofficial", "@ENUCS" };

            hashtagList = bodyHelper.ExecuteHashtag(messageBody);
            mentionList = bodyHelper.ExecuteMention(messageBody);


            CollectionAssert.AreEqual(expectedHashtag, hashtagList);
            CollectionAssert.AreEqual(expectedMention, mentionList);

        }


        //Testing Handling of Emails for isolating the Email Type, Sender, Subject and Message.
        [TestMethod]

        public void TestEmailHandling()
        {
            helper = new ProcessCompositionHelper();
            helper.AssembleProcessComponents();

            string sender = "";
            string subject = "";
            string message = "";

            string inputHeader = "E4056295";
            string expectedHeader = "Email Processed.";

            string inputBody = "usmaan@napier.ac.uk NBM's first email!! NBM is officially in a working state ! check out the website https://nbmofficial.com";
            string expectedMessage = " NBM is officially in a working state ! check out the website <URL Quarantined>";


            string expectedSender = "usmaan@napier.ac.uk";
            string expectedSubject = " NBM's first email!!";

            var result = helper.Execute(inputHeader, inputBody, ref sender, ref subject, ref message);

            Assert.AreEqual(expectedSender, sender);
            Assert.AreEqual(expectedSubject, subject);
            Assert.AreEqual(expectedMessage, message);
            Assert.AreEqual(expectedHeader, result); 
        
        }
        
        //Testing Handling for Email Type - Serious Incident Report. Isolating Sort Code and Nature of Incident.
        [TestMethod]
        public void TestEmailSIR()
        {
            bodyHelper = new BodyCompositionHelper();
            bodyHelper.AssembleBodyComponents();

            string[] incidentData = File.ReadAllLines(@"F:\Software Coursework 2022\incident.csv");

            string inputMessage = "usmaan@napier.ac.uk SIR 25/12/2022 Sort Code: 40-40-40 Nature of Incident: Raid";

            var sortCode = bodyHelper.ExecuteSIR(inputMessage);
            var incident = bodyHelper.ExecuteIncident(inputMessage,incidentData);

            string expectedSortCode = " 40-40-40";
            string expectedIncident = "Raid";

            Assert.AreEqual(expectedSortCode, sortCode);
            Assert.AreEqual(expectedIncident, incident);
        }


        //Testing Handling of SMS Text Messages for isolating the Sender (Phone number), Message and Outputting a modified body for textspeak abbreviations.
        [TestMethod]
        public void TestSMS()
        {
            helper = new ProcessCompositionHelper();
            helper.AssembleProcessComponents();

            bodyHelper = new BodyCompositionHelper();
            bodyHelper.AssembleBodyComponents();

            string[] textspeak = File.ReadAllLines(@"F:\Software Coursework 2022\textwords (1).csv");

            string sender = "";
            string subject = "";
            string message = "";

            string inputHeader = "S190295";
            string inputMessage = "+44756932 You guys need to check this out ASAP NBM App is offically working!";

            string expectedHeader = "SMS Text Message Processed.";
            string expectedSender = "+44756932";
            string expectedMessage = " You guys need to check this out ASAP NBM App is offically working!";

            var result = helper.Execute(inputHeader, inputMessage, ref sender, ref subject, ref message);
            var expandedText = bodyHelper.ExecuteTextspeak(ref inputMessage, textspeak);

            string expectedExpanded = "+44756932 You guys need to check this out ASAP<As soon as possible> NBM App is offically working!";
            string expectedText = "+44756932 You guys need to check this out ASAP<> NBM App is offically working!";

            Assert.AreEqual(expectedHeader, result);
            Assert.AreEqual(expectedSender, sender);
            Assert.AreEqual(expectedMessage, message);
            Assert.AreEqual(expectedExpanded, expandedText);
            Assert.AreEqual(expectedText, inputMessage);
        }


    }
}
