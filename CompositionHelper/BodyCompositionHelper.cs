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
    //Composition Helper Class to execute methods defined for IBodyProcecss interface.
    public class BodyCompositionHelper
    {
        [Import(typeof(IBodyProcess))]
            public IBodyProcess BodyProcessPlugin { get; set; }


        //Assembling components
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

        //Calling GetHashtag with parameters from main window
        public string[] ExecuteHashtag(string messageBody)
        {
            string[] result = null;

                    result = BodyProcessPlugin.GetHashtag(messageBody);
                   
            return result;
        }

        //Calling GetMention with parameters from main window
        public string[] ExecuteMention(string messageBody)
        {

            string[] result = BodyProcessPlugin.GetMention(messageBody);
                   
          
            return result;
        }

        //Calling GetSortCode with parameters from main window

        public string ExecuteSIR(string messageBody)
        {
            string result = null;
           
                    result = BodyProcessPlugin.GetSortCode(messageBody);
          

            
            return result;
        }

        //Calling GetIncident with parameters from main window
        public string ExecuteIncident(string messageBody,string[] incidentData)
        {
            string result = null;
         
                    result = BodyProcessPlugin.GetIncident(messageBody, incidentData);
         

            
            return result;
        }

        //Calling GetTextspeak with parameters from main window
        public string ExecuteTextspeak(ref string messageBody, string[] textspeakAbbrev)
        {
            string result = null;
                    result = BodyProcessPlugin.GetTextspeak(ref messageBody, textspeakAbbrev);
              
              
            return result;
        }

        //Calling GetURL with parameters from main window
        public string[] ExecuteURL(string messageBody)
        {
            string[] result;

            result = BodyProcessPlugin.GetURL(messageBody);

            return result;
        }
    }
}

