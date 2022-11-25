using Xunit;
using CompositionHelper;
using Napier_Bank_Messaging.ViewModels;

namespace Unit_Testing
{
    public class UnitTest1
    {
        ProcessCompositionHelper helper;
        BodyCompositionHelper bodyHelper;

        [Fact]
        public void Test1()
        {
            helper = new ProcessCompositionHelper();
            helper.AssembleProcessComponents();

            bodyHelper = new BodyCompositionHelper();
            bodyHelper.AssembleBodyComponents();

            string expectedHeader = "Tweet Processed.";

            string inputHeader = "T123";
            string inputBody = "@Usmaan this a #Test #NapierBanking";
            string sender = "";
            string subject = "";
            string message = "";

            var result = helper.Execute(inputHeader,inputBody,ref sender,ref subject, ref message);

            Assert.Equal(expectedHeader,result);
        }
    }
}