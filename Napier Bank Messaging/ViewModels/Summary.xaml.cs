﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Forms;


namespace Napier_Bank_Messaging
{
    /// <summary>
    /// Interaction logic for Summary.xaml
    /// </summary>
    public partial class Summary : Window
    {
        public Summary()
        { 
            InitializeComponent();

            tweetSingleton ts = tweetSingleton.getInstance();

            List<string> hashtags = new List<string>();
            List<int> count = new List<int>();

            ts.returnHashtagList(ref hashtags, ref count);

            string hashtagString = string.Empty;

            for(int i = 0; i < hashtags.Count; i++)
            {
                hashtagString = hashtagString + hashtags[i] + " - " + count[i] + "\n";
            }

            HashtagText.Text = hashtagString;

            List<string> mentions = new List<string>();

            ts.returnMentionList(ref mentions);

            string mentionsString = string.Empty;

            foreach(string mention in mentions)
            {
                mentionsString = mentionsString + mention + "\n";
            }

            MentionText.Text = mentionsString;


            List<string> SIR = new List<string>();
            List<string> sortCode = new List<string>();
            List<string> incident = new List<string>();

            emailSingleton es = emailSingleton.getInstance();

            es.returnSIR(ref SIR, ref sortCode, ref incident);

            string SIRstring = string.Empty;

            for(int i=0; i<SIR.Count; i++)
            {
                SIRstring = SIRstring + SIR[i] + " Sort Code: " + sortCode[i] + " Nature of Incident: " + incident[i] + "\n";

            }

            SIRText.Text = SIRstring;
        }

        private void EndBtn_Click(object sender, RoutedEventArgs e)
        {
            System.Environment.Exit(0);
        }
    }
}
