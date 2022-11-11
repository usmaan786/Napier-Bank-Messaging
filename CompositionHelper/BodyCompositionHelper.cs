using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contract;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Reflection;

namespace CompositionHelper
{
    public class BodyCompositionHelper
    {
        [ImportMany]
        public System.Lazy<IBodyProcess, IDictionary<string, object>>[]
            BodyProcessPlugin { get; set; }

        public void AssembleBodyComponents()
        {
            try
            {
                var catalog = new AssemblyCatalog(Assembly.GetExecutingAssembly());

                var container = new CompositionContainer(catalog);

                container.ComposeParts(this);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public string Execute(string messageBody, string operationMethod)
        {
            string result=null;
            foreach (var BodyPlug in BodyProcessPlugin)
            {
                if ((string)BodyPlug.Metadata["BodyMetaData"] == operationMethod)
                {
                    result = BodyPlug.Value.GetBody(messageBody);
                    break;
                }

            }
            return result;

        }

        public string[] ExecuteHashtag(string messageBody, string operationMethod)
        {
            string[] result = null;
            foreach(var BodyPlug in BodyProcessPlugin)
            {
                if((string)BodyPlug.Metadata["BodyMetaData"]==operationMethod)
                {
                    result = BodyPlug.Value.GetHashtag(messageBody);
                    break;
                }
            
            }
            return result;
        }

        public string[] ExecuteMention(string messageBody, string operationMethod)
        {
            string[] result = null;
            foreach (var BodyPlug in BodyProcessPlugin)
            {
                if ((string)BodyPlug.Metadata["BodyMetaData"] == operationMethod)
                {
                    result = BodyPlug.Value.GetMention(messageBody);
                    break;
                }

            }
            return result;
        }

        public string ExecuteSIR(string messageBody, string operationMethod)
        {
            string result = null;
            foreach (var BodyPlug in BodyProcessPlugin)
            {
                if ((string)BodyPlug.Metadata["BodyMetaData"] == operationMethod)
                {
                    result = BodyPlug.Value.GetSortCode(messageBody);
                    break;
                }

            }
            return result;
        }

        public string ExecuteIncident(string messageBody, string operationMethod)
        {
            string result = null;
            foreach (var BodyPlug in BodyProcessPlugin)
            {
                if ((string)BodyPlug.Metadata["BodyMetaData"] == operationMethod)
                {
                    result = BodyPlug.Value.GetIncident(messageBody);
                    break;
                }

            }
            return result;
        }

        public string ExecuteTextspeak(ref string messageBody, string[] textspeakAbbrev, string operationMethod)
        {
            string result = null;
            foreach (var BodyPlug in BodyProcessPlugin)
            {
                if ((string)BodyPlug.Metadata["BodyMetaData"] == operationMethod)
                {
                    result = BodyPlug.Value.GetTextspeak(ref messageBody, textspeakAbbrev);
                    break;
                }

            }
            return result;
        }
    }
}

