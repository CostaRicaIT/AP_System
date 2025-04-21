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
            "TEMP_FOLDER",
            "TEMP_TAX_ID",
            "TEMP_ORACLE_DESCRIPTION",
            "TEMP_ORACLE_NOTES",
            "TEMP_ORACLE_INSTRUCTIONS",
            "TEMP_BILLING_PERIOD",
            "TEMP_VSU",
            "TEMP_W9_W8",
            "TEMP_INVOICE_NOTES",
            "TEMP_REMIT_TOACCOUNT",
            "TEMP_DISTRIBUTION_SET",
            "TEMP_DISTRIBUTION_COMBINATION",
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

        [Test]
        public void AllowHTML_Attribute_TB_HIGHLIGHTS()
        {
            // Arrange
            var modelType = typeof(TB_HIGHLIGHTS);
            var propertiesWithTinyMCE = new[]
            {
            "HIGHLIGHTS",
            "HIGHLIGHTS_COMMENTS",
            "HIGHLIGHTS_INSTRUCTIONS",
            "HIGHLIGHTS_EXCEPTIONS",
            "HIGHLIGHTS_COMMON_ISSUES",
            "HIGHLIGHTS_SUPPLIER_AGENCY",
            "HIGHLIGHTS_TEMPLATE_COMMENTS",
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

        [Test]
        public void AllowHTML_Attribute_TB_HISTORIC_REMIT()
        {
            // Arrange
            var modelType = typeof(TB_HISTORIC_REMIT);
            var propertiesWithTinyMCE = new[]
            {
            "HISTORIC_REMIT_INFO",
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


        [Test]
        public void AllowHTML_Attribute_TB_WORKSPACE()
        {
            // Arrange
            var modelType = typeof(TB_WORKSPACE);
            var propertiesWithTinyMCE = new[]
            {
            "WS_TEMP_FOLDER",
            "WS_TEMP_TAX_ID",
            "WS_TEMP_ORACLE_DESCRIPTION",
            "WS_TEMP_ORACLE_NOTES",
            "WS_TEMP_ORACLE_INSTRUCTIONS",
            "WS_TEMP_BILLING_PERIOD",
            "WS_TEMP_VSU",
            "WS_TEMP_W9_W8",
            "WS_TEMP_INVOICE_NOTES",
            "WS_TEMP_REMIT_TOACCOUNT",
            "WS_TEMP_DISTRIBUTION_SET",
            "WS_TEMP_DISTRIBUTION_COMBINATION",
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

        [Test]
        public void AllowHTML_Attribute_WS_COMMENTS()
        {
            // Arrange
            var modelType = typeof(WS_COMMENTS);
            var propertiesWithTinyMCE = new[]
            {
            "WORKSPACE_INFO",
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

        [Test]
        public void AllowHTML_Attribute_WS_LAST_ACTIONS()
        {
            // Arrange
            var modelType = typeof(WS_LAST_ACTIONS);
            var propertiesWithTinyMCE = new[]
            {
            "LAST_ACTIONS_INFO",
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
