using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Contract
{
    public interface IBodyProcess
    {
       string GetBody(string messageBody);

        string[] GetHashtag(string messageBody);

        string[] GetMention(string messageBody);

        string GetSortCode(string messageBody);

        string GetIncident(string messageBody, string[] incidentData);

        string GetTextspeak(ref string messageBody,string[] textspeakAbbrev);

        string[] GetURL(string messageBody);

    }
}
