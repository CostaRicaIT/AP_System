using AccountsPayable.Models;
using NUnit.Framework;
using System.Linq;
using System.Web.Mvc;


namespace AccountsPayable.Tests.Controllers
{
    public class ModelTest
    {
        [Test]
        public void AllowHTML_Attribute_TB_TEMPLATE()
        {
            // Arrange
            var modelType = typeof(TB_TEMPLATE);
            var propertiesWithTinyMCE = new[]
            {
            "TEMP_ORACLE_DESCRIPTION",
            "TEMP_ORACLE_NOTES",
            "TEMP_ORACLE_INSTRUCTIONS",
            "TEMP_BILLING_PERIOD",
            "TEMP_VSU",
            "TEMP_W9_W8",
            "TEMP_INVOICE_NOTES",
            "",
            "",
        };

            // Act & Assert
            foreach (var propertyName in propertiesWithTinyMCE)
            {
                var propertyInfo = modelType.GetProperty(propertyName);
                Assert.IsNotNull(propertyInfo, $"{propertyName} property not found on {modelType.Name} model.");

                var hasAllowHtmlAttribute = propertyInfo.GetCustomAttributes(typeof(AllowHtmlAttribute), false).Any();
                Assert.IsTrue(hasAllowHtmlAttribute, $"{propertyName} property on {modelType.Name} model does not have the [AllowHtml] attribute.");
            }
        }
    }
}
