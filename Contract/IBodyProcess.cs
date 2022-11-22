using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Contract
{
    //Interface class used for processing body of messages to get a list of Hashtags, Mentions as well as getting Sort Code and Nature of Incident for SIRs, URLs for all emails and Sanitizing textspeak abbreviation
    public interface IBodyProcess
    {
        string[] GetHashtag(string messageBody);

        string[] GetMention(string messageBody);

        string GetSortCode(string messageBody);

        string GetIncident(string messageBody, string[] incidentData);

        string GetTextspeak(ref string messageBody,string[] textspeakAbbrev);

        string[] GetURL(string messageBody);

    }
}
