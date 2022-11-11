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
        [Import(typeof(IBodyProcess))]
            public IBodyProcess BodyProcessPlugin { get; set; }

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
        public string Execute(string messageBody)
        {
            string result=null;
           
                    result = BodyProcessPlugin.GetBody(messageBody);
                
           
            return result;

        }

        public string[] ExecuteHashtag(string messageBody)
        {
            string[] result = null;

                    result = BodyProcessPlugin.GetHashtag(messageBody);
                   
            return result;
        }

        public string[] ExecuteMention(string messageBody)
        {

            string[] result = BodyProcessPlugin.GetMention(messageBody);
                   
          
            return result;
        }

        public string ExecuteSIR(string messageBody)
        {
            string result = null;
           
                    result = BodyProcessPlugin.GetSortCode(messageBody);
          

            
            return result;
        }

        public string ExecuteIncident(string messageBody)
        {
            string result = null;
         
                    result = BodyProcessPlugin.GetIncident(messageBody);
         

            
            return result;
        }

        public string ExecuteTextspeak(ref string messageBody, string[] textspeakAbbrev)
        {
            string result = null;
                    result = BodyProcessPlugin.GetTextspeak(ref messageBody, textspeakAbbrev);
              
              
            return result;
        }
    }
}

