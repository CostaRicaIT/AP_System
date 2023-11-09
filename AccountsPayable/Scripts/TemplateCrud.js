$(document).ready(function () {
    // Function to disable or enable the input field according to the status of the checkbox.
    function toggleInputState(checkbox, input) {
        if (checkbox.is(':checked')) {
            input.prop('disabled', true);
            input.val(null)
        } else {
            input.prop('disabled', false);
        }
    }
    // Listen for the change in the checkboxes and adjust the status of the corresponding input field
    $('#BoxNoTaxId').change(function () {
        toggleInputState($(this), $('#form-TaxID'));
    });

    $('#BoxNoRemitInfo').change(function () {
        toggleInputState($(this), $('#form-RemitTo'));
    });

    $('#BoxSupName').change(function () {
        toggleInputState($(this), $('#form-SupName'));

    });
    $(".btn-cancel").click(function () {
        document.location.href = window.location.origin + '/Main/Index';
    });
    $("#NoAddAlias_Emailbtn").click(function (e) {
        e.preventDefault();
        saveNoAliasEmail();
    });
    $("#btn_AddAlias_Email").click(function (e) {
        e.preventDefault();
        SaveWithAliasEmail();
    });
    $(".btn-cancel").click(function () {
        document.location.href = window.location.origin + '/Main/Index';
    });
    $(".btn-update").click(function (e) {
        e.preventDefault();
        var TaxId = $("#form-TaxID").val();
        var RemitTo = $("#form-RemitTo").val();
        var SupplierName = $("#form-SupName").val();
        var SupplierNumber = $("#form-SupNumber").val();
        var OurVendor = $("#form-OurVendorA").val();
        var OracleSupplierSite = $("#form-SupSite").val();
        var OracleAddress = $("#form-Address").val();
        var OracleLegalEntity = $("#FK_TB_LEGAL_ENTITY_ID").val();
        var OracleTaxPayerID = $("#form-FirstParty").val();
        var OracleType = $("#FK_TB_ORACLE_TYPE_ID").val();
        var OracleDescrption = $("#form-Description").val();
        var OraclePayTerms = $("#FK_TB_ORACLE_PAY_TERMS_ID").val();
        var OracleAccountCoding = $("#form-AccountC").val();
        var OracleSource = $("#FK_TB_ORACLE_SOURCE_ID").val();
        var OracleNotes = $("#form-OracleN").val();
        var OracleInstructions = $("#form-OracleI").val();
        var Approver = $("#FK_TB_APPROVER_ID").val();
        var ApproverComments = $("#form-ApproverComents").val();
        var InvoiceFormat = $("#form-InvoiceF").val();
        var InvoiceType = $("#form-INFType").val();
        $.ajax({
            url: '/Main/Update',
            type: 'POST',
            data: {
                TEMP_TAX_ID: TaxId,
                TEMP_REMIT_TO: RemitTo,
                TEMP_SUPPLIER_NAME: SupplierName,
                TEMP_SUPPLIER_NUMBER: SupplierNumber,
                TEMP_VENDOR_ACCOUNT: OurVendor,
                TEMP_SUPPLIER_SITE: OracleSupplierSite,
                TEMP_ADDRESS: OracleAddress,
                FK_TB_LEGAL_ENTITY_ID: OracleLegalEntity,
                TEMP_TAXPAYER_ID: OracleTaxPayerID,
                FK_TB_ORACLE_TYPE_ID: OracleType,
                TEMP_ORACLE_DESCRIPTION: OracleDescrption,
                FK_TB_ORACLE_PAY_TERMS_ID: OraclePayTerms,
                TEMP_ACCOUNT_CODING: OracleAccountCoding,
                FK_TB_ORACLE_SOURCE_ID: OracleSource,
                TEMP_ORACLE_NOTES: OracleNotes,
                TEMP_ORACLE_INSTRUCTIONS: OracleInstructions,
                FK_TB_APPROVER_ID: Approver,
                TEMP_APPROVER_COMMENTS: ApproverComments,
                TEMP_INVOICE_FORMAT: InvoiceFormat,
                TEMP_INVOICE_TYPE: InvoiceType
            },
            success: function (data) {
                if (data.success) {
                    document.location.href = window.location.origin + '/Main/Index';
                }
            },
            error: function () {
                alert("An error occurred while saving the record.");
            }
        });
    });
});
function saveNoAliasEmail() {
    var templateData = {
        TEMP_TAX_ID: $("#form-TaxID").val(),
        TEMP_REMIT_TO: $("#form-RemitTo").val(),
        TEMP_SUPPLIER_NAME: $("#form-SupName").val(),
        TEMP_VENDOR_ACCOUNT: $("#form-OurVendorA").val(),
        TEMP_SUPPLIER_NUMBER: $("#form-SupNumber").val(),
        TEMP_SUPPLIER_SITE: $("#form-SupSite").val(),
        TEMP_ADDRESS: $("#form-Address").val(),
        FK_TB_LEGAL_ENTITY_ID: $("#FK_TB_LEGAL_ENTITY_ID").val(),
        TEMP_TAXPAYER_ID: $("#form-FirstParty").val(),
        FK_TB_ORACLE_TYPE_ID: $("#FK_TB_ORACLE_TYPE_ID").val(),
        TEMP_ORACLE_DESCRIPTION: $("#form-Description").val(),
        FK_TB_ORACLE_PAY_TERMS_ID: $("#FK_TB_ORACLE_PAY_TERMS_ID").val(),
        TEMP_ACCOUNT_CODING: $("#form-AccountC").val(),
        FK_TB_ORACLE_SOURCE_ID: $("#FK_TB_ORACLE_SOURCE_ID").val(),
        TEMP_ORACLE_NOTES: $("#form-OracleN").val(),
        TEMP_ORACLE_INSTRUCTIONS: $("#form-OracleI").val(),
        FK_TB_APPROVER_ID: $("#FK_TB_APPROVER_ID").val(),
        TEMP_APPROVER_COMMENTS: $("#form-ApproverComents").val(),
        TEMP_INVOICE_FORMAT: $("#form-InvoiceF").val(),
        TEMP_INVOICE_TYPE: $("#form-INFType").val()
    };

    var HistoricRemitToData = { //data to TB_HISTORIC REMIT
        HISTORIC_REMIT_INFO: $("#form-HistRemitTo").val()
    };

    var HighLightsData = { //data to TB_HIGHLIGHTS
        HIGHLIGHTS: $("#form-Higlights").val(),
        HIGHLIGHTS_COMMENTS: $("#form-HiglightsC").val(),
        HIGHLIGHTS_INSTRUCTIONS: $("#form-Instructions").val(),
        HIGHLIGHTS_EXCEPTIONS: $("#form-Exceptions").val(),
        HIGHLIGHTS_COMMON_ISSUES: $("#form-MostCI").val(),
        HIGHLIGHTS_SUPPLIER_AGENCY: $("#form-SupplierA").val(),
        HIGHLIGHTS_TEMPLATE_COMMENTS: $("#form-TemplateC").val(),

    };

    $.ajax({
        url: '/Main/CreateNoEmail_Alias',
        type: 'POST',
        data: {
            templateData, HistoricRemitToData, HighLightsData
        },
        success: function (data) {
            if (data.success) {
                document.location.href = window.location.origin + '/Main/Index';
            }
        },
        error: function () {
            alert("An error occurred while saving the record.");
        }
    });
}

function SaveWithAliasEmail() {
    var templateData = {
        TEMP_TAX_ID: $("#form-TaxID").val(),
        TEMP_REMIT_TO: $("#form-RemitTo").val(),
        TEMP_SUPPLIER_NAME: $("#form-SupName").val(),
        TEMP_VENDOR_ACCOUNT: $("#form-OurVendorA").val(),
        TEMP_SUPPLIER_NUMBER: $("#form-SupNumber").val(),
        TEMP_SUPPLIER_SITE: $("#form-SupSite").val(),
        TEMP_ADDRESS: $("#form-Address").val(),
        FK_TB_LEGAL_ENTITY_ID: $("#FK_TB_LEGAL_ENTITY_ID").val(),
        TEMP_TAXPAYER_ID: $("#form-FirstParty").val(),
        FK_TB_ORACLE_TYPE_ID: $("#FK_TB_ORACLE_TYPE_ID").val(),
        TEMP_ORACLE_DESCRIPTION: $("#form-Description").val(),
        FK_TB_ORACLE_PAY_TERMS_ID: $("#FK_TB_ORACLE_PAY_TERMS_ID").val(),
        TEMP_ACCOUNT_CODING: $("#form-AccountC").val(),
        FK_TB_ORACLE_SOURCE_ID: $("#FK_TB_ORACLE_SOURCE_ID").val(),
        TEMP_ORACLE_NOTES: $("#form-OracleN").val(),
        TEMP_ORACLE_INSTRUCTIONS: $("#form-OracleI").val(),
        FK_TB_APPROVER_ID: $("#FK_TB_APPROVER_ID").val(),
        TEMP_APPROVER_COMMENTS: $("#form-ApproverComents").val(),
        TEMP_INVOICE_FORMAT: $("#form-InvoiceF").val(),
        TEMP_INVOICE_TYPE: $("#form-INFType").val()
    };

    var HistoricRemitToData = { //data to TB_HISTORIC REMIT
        HISTORIC_REMIT_INFO: $("#form-HistRemitTo").val()
    };

    var HighLightsData = { //data to TB_HIGHLIGHTS
        HIGHLIGHTS: $("#form-Higlights").val(),
        HIGHLIGHTS_COMMENTS: $("#form-HiglightsC").val(),
        HIGHLIGHTS_INSTRUCTIONS: $("#form-Instructions").val(),
        HIGHLIGHTS_EXCEPTIONS: $("#form-Exceptions").val(),
        HIGHLIGHTS_COMMON_ISSUES: $("#form-MostCI").val(),
        HIGHLIGHTS_SUPPLIER_AGENCY: $("#form-SupplierA").val(),
        HIGHLIGHTS_TEMPLATE_COMMENTS: $("#form-TemplateC").val(),

    };
    var emailData = { //data to TB_EMAIL_BACKUP
        EMAIL_BACKUP: $("#form-BackUpEmails").val()
    }
    var aliasData = { //data for TB_ALIAS
        ALIAS_NAME: $("#form-AddAlias").val()
    }

    $.ajax({
        url: '/Main/CreateWithEmail_Alias',
        type: 'POST',
        data: {
            templateData, HistoricRemitToData, HighLightsData,aliasData,emailData
        },
        success: function (data) {
            if (data.success) {
                document.location.href = window.location.origin + '/Main/Index';
            }
        },
        error: function () {
            alert("An error occurred while saving the record.");
        }
    });



}