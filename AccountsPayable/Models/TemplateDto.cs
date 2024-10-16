using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AccountsPayable.Models
{
    public class TemplateDto
    {
        public int Id { get; set; }                       // ID
        public string Alias { get; set; }                  // Alias
        public string Folder { get; set; }                 // Folder
        public string TempTaxId { get; set; }              // Tax ID
        public string TempSupplierName { get; set; }       // Supplier Name
        public string TempSupplierNumber { get; set; }     // Supplier Number
        public string RemitTo { get; set; }                // Remit To
        public string SupplierSite { get; set; }           // Supplier Site
        public string HistoricRemitTo { get; set; }        // Historic Remit to
        public string VendorAccount { get; set; }          // Vendor Account
        public string Source { get; set; }                 // Source
        public string InvoiceFormat { get; set; }          // Invoice Format
        public string InvoiceType { get; set; }            // Invoice Type
        public string InvoiceNotes { get; set; }           // Invoice Notes
        public string W9W8BENForm { get; set; }            // W9/W-8BEN Form
        public string VSUForm { get; set; }                // VSU Form
        public string PaymentMethod { get; set; }          // Payment Method
        public string RemitToAccount { get; set; }         // Remit-To Account
        public string PayTerms { get; set; }               // Pay Terms
        public string InvoiceDescription { get; set; }     // Invoice Description
        public string BillingPeriod { get; set; }          // Billing Period
        public string Dates { get; set; }                  // Dates
        public string DistributionSet { get; set; }        // Distribution Set
        public string DistributionCombination { get; set; } // Distribution Combination
        public string AccountingDate { get; set; }         // Accounting Date
        public string LegalEntity { get; set; }            // Legal Entity
        public string OrganizationType { get; set; }       // Organization Type
        public string TaxPayerID { get; set; }             // Tax Payer ID
        public string Type { get; set; }                   // Type
        public string Description { get; set; }             // Description
        public string OracleNotes { get; set; }            // Oracle Notes
        public string OracleInstructions { get; set; }      // Oracle Instructions
        public string ARKeyContactsCurrent { get; set; }   // AR Key Contacts/Currents
        public string ARKeyContactsPrior { get; set; }     // AR Key Contacts/Prior
        public string Approver { get; set; }               // Approver
        public string ApproverComments { get; set; }       // Approver Comments
        public string EmailBackup { get; set; }            // Email Backup
    }
}