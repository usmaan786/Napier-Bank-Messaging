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
    //Composition helper for methods defined for IProcess interface class.
    public class ProcessCompositionHelper
    {
        [Import(typeof(IProcess))]
        public IProcess ProcessPlugin { get; set; }


        //Assembling components
        public void AssembleProcessComponents()
        {
            try
            {
                var catalog = new AssemblyCatalog(Assembly.GetExecutingAssembly());

                var container = new CompositionContainer(catalog);

                container.ComposeParts(this);
            }
            catch(Exception ex)
            {
                throw ex;
            }

        }

        //Calling GetType method with parameters from main window
        public string Execute(string messageHeader, string messageBody, ref string sender, ref string subject, ref string message)
        {
            return ProcessPlugin.GetType(messageHeader, messageBody, ref sender, ref subject, ref message);
        }
    }
}
