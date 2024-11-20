function create() {
    // get data from template
    var templateData = {
        TEMP_TAX_ID: $("#form-TaxID").val(),
        TEMP_REMIT_TO: $("#form-RemitTo").val().replace(/[<>]/g, ''),
        TEMP_SUPPLIER_NAME: $("#form-SupName").val().replace(/[<>]/g, ''),
        TEMP_VENDOR_ACCOUNT: $("#form-OurVendorA").val().replace(/[<>]/g, ''),
        TEMP_SUPPLIER_NUMBER: $("#form-SupNumber").val().replace(/[<>]/g, ''),
        TEMP_SUPPLIER_SITE: $("#form-SupSite").val().replace(/[<>]/g, ''),
        FK_TB_LEGAL_ENTITY_ID: $("#legalEntityDropdown").val().replace(/[<>]/g, ''),
        TEMP_TAXPAYER_ID: $("#form-FirstParty").val().replace(/[<>]/g, ''),
        FK_TB_ORACLE_TYPE_ID: $("#FK_TB_ORACLE_TYPE_ID").val().replace(/[<>]/g, ''),
        TEMP_ORACLE_DESCRIPTION: tinymce.get("form-Description").getContent(),
        FK_TB_ORACLE_PAY_TERMS_ID: $("#FK_TB_ORACLE_PAY_TERMS_ID").val().replace(/[<>]/g, ''),
        TEMP_DISTRIBUTION_SET: $("#form-DistroSet").val().replace(/[<>]/g, ''),
        FK_TB_ORACLE_SOURCE_ID: $("#FK_TB_ORACLE_SOURCE_ID").val().replace(/[<>]/g, ''),
        TEMP_ORACLE_NOTES: tinymce.get("form-OracleN").getContent(),
        TEMP_ORACLE_INSTRUCTIONS: tinymce.get("form-OracleI").getContent(),
        FK_TB_APPROVER_ID: $("#approverDropdown").val().replace(/[<>]/g, ''),
        TEMP_APPROVER_COMMENTS: $("#form-ApproverComents").val().replace(/[<>]/g, ''),
        TEMP_INVOICE_FORMAT: $("#form-InvoiceF").val().replace(/[<>]/g, ''),
        TEMP_INVOICE_TYPE: $("#form-INFType").val().replace(/[<>]/g, ''),
        TEMP_FOLDER: $("#form-Folder").val().replace(/[<>]/g, ''),
        TEMP_PAYMENT_METHOD: $("#form-PayMethod").val().replace(/[<>]/g, ''),
        TEMP_REMIT_TOACCOUNT: $("#form-RemitToAccount").val().replace(/[<>]/g, ''),
        TEMP_BILLING_PERIOD: tinymce.get("form-BillPeriod").getContent(),
        TEMP_BILLING_PRERIOD_DATE: $("#form-Dates").val().replace(/[<>]/g, ''),
        TEMP_DISTRIBUTION_COMBINATION: $("#form-DistroCombination").val().replace(/[<>]/g, ''),
        TEMP_ACCOUNTING_DATE: $("#form-AccoDate").val().replace(/[<>]/g, ''),
        TEMP_VSU: tinymce.get("form-VSU").getContent(),
        TEMP_W9_W8: tinymce.get('form-W9W8').getContent(),
        TEMP_INVOICE_NOTES: tinymce.get("form-InvoiceNotes").getContent(),
        TEMP_INVOICE_DESCRIPTION: $("#form-InvoiceDescrip").val().replace(/[<>]/g, ''),
        CONTACTS_CURRENT: $("#form-Currents").val().replace(/[<>]/g, ''),
        CONTACTS_PRIOR: $("#form-Prior").val().replace(/[<>]/g, ''),
        FK_TB_ORGANIZATION_TYPE_ID: $("#organizationTypeDropdown").val().replace(/[<>]/g, '')
    };

    // get data from workspace-specific fields
    var workspaceData = {
        FK_WS_INVOICE_CATEGORY_ID: $("#FK_WS_INVOICE_CATEGORY_ID").val().replace(/[<>]/g, ''),
        WS_STATUS: $("#form-Status").val().replace(/[<>]/g, ''),
        WS_REASON: $("#form-Reason").val().replace(/[<>]/g, ''),
        WS_EMAIL_RECEIVED: $("#form-EmailReceived").val().replace(/[<>]/g, ''),
        WS_CREATED_DATE: $("#form-CreateDate").val().replace(/[<>]/g, ''),
        WS_SOURCE: $("#form-Source").val().replace(/[<>]/g, ''),
        WS_HANDLED_BY: $("#form-HandledBy").val().replace(/[<>]/g, ''),
        WS_INVOICE_DATE: $("#form-InvoiceDate").val().replace(/[<>]/g, ''),
        WS_AMOUNT: $("#form-Amount").val().replace(/[<>]/g, ''),
        WS_INVOICE_NUMBER: $("#form-InvoiceNumber").val().replace(/[<>]/g, ''),
        WS_COMMENTS: tinymce.get("form-Comments").getContent(),
        WS_LAST_ACTION: tinymce.get("form-LastAction").getContent()
    };

    var HistoricRemitToData = {
        HISTORIC_REMIT_INFO: tinymce.get("form-HistRemitTo").getContent()
    };

    var HighLightsData = {
        HIGHLIGHTS: tinymce.get("form-Higlights").getContent(),
        HIGHLIGHTS_COMMENTS: tinymce.get("form-HiglightsC").getContent(),
        HIGHLIGHTS_INSTRUCTIONS: tinymce.get("form-Instructions").getContent(),
        HIGHLIGHTS_EXCEPTIONS: tinymce.get("form-Exceptions").getContent(),
        HIGHLIGHTS_COMMON_ISSUES: tinymce.get("form-MostCI").getContent(),
        HIGHLIGHTS_SUPPLIER_AGENCY: tinymce.get("form-SupplierA").getContent(),
        HIGHLIGHTS_TEMPLATE_COMMENTS: tinymce.get("form-TemplateC").getContent()
    };

    $.ajax({
        url: '/WSCrud/Create/',
        type: 'POST',
        data: {
            templateData, workspaceData, HistoricRemitToData, HighLightsData, aliasDataList, emailDataList
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
