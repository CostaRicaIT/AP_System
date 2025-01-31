//Show scroll to top button
window.onscroll = function () {
    scrollFunction();
};

function scrollFunction() {
    var scrollToTopButton = document.getElementById("scrollToTop");

    // Show or hide the button based on scroll position
    if (document.body.scrollTop > document.body.scrollHeight / 3 || document.documentElement.scrollTop > document.documentElement.scrollHeight / 3) {
        scrollToTopButton.style.display = "block";
    } else {
        scrollToTopButton.style.display = "none";
    }
}

function scrollToTop() {
    document.body.scrollTop = 0;
    document.documentElement.scrollTop = 0;
}
$(document).ready(function () {
    // Query to get text from emails according to ID in dropdown
    function getEmailBackupText(selectedEmail, templateId) {
        if (selectedEmail != 0) {
            $.ajax({
                url: '/Historic/GetEmailText',
                type: 'GET',
                data: { emailId: selectedEmail, id: templateId },
                success: function (data) {
                    // Check if the data is retrieved successfully
                    if (data.success) {
                        // Updates the content of the textarea with the historic email text
                        var emailBackup = data.historicEmailText;
                        $('#form-BackUpEmail').val(emailBackup);
                    } else {
                        alert("Failed to get email text." + data.message);
                    }
                },
                error: function () {
                    // Handles any errors that may occur during the AJAX request
                    alert("Error getting email text.");
                }
            });
        }
    }
    // Query to get text from Historic Remit to according to ID in dropdown
    function getHistoricRemitText(selectedHistoric, templateId) {
        $.ajax({
            url: '/Historic/GetHistoricRemitText',
            type: 'GET',
            data: { historicId: selectedHistoric, id: templateId },
            success: function (data) {
                // Check if the data is retrieved successfully
                if (data.success) {
                    // Updates the content of the textarea with the historic email text
                    var historicRemit = data.historicText;
                    //$('#historicRemitInfoView').val(historicRemit);
                    tinymce.get("historicRemitInfoView").setContent(historicRemit);
                } else {
                    alert("Failed to get historic Remit text. " + data.message);
                }
            },
            error: function () {
                // Handles any errors that may occur during the AJAX request
                alert("Error get historic Remit text.");
            }
        });
    }
    // Query to get text from Highlights history according to ID in dropdown
    function getHighLigthsText(selectedHighLight, templateId) {
        $.ajax({
            url: '/Historic/GetHighlights',
            type: 'GET',
            data: { highlightsId: selectedHighLight, id: templateId },
            success: function (data) {
                // Check if the data is retrieved successfully
                if (data.success) {
                    tinymce.get("HighlightsHistoryView").setContent(data.historicText.HIGHLIGHTS);
                    tinymce.get("HighlightsCHistoryView").setContent(data.historicText.HIGHLIGHTS_COMMENTS);
                    tinymce.get("InstructionsHistoryView").setContent(data.historicText.HIGHLIGHTS_INSTRUCTIONS);
                    tinymce.get("ExceptionsHistoryView").setContent(data.historicText.HIGHLIGHTS_EXCEPTIONS);
                    tinymce.get("MostCIHistoryView").setContent(data.historicText.HIGHLIGHTS_COMMON_ISSUES);
                    tinymce.get("SupplierAHistoryView").setContent(data.historicText.HIGHLIGHTS_SUPPLIER_AGENCY);
                    tinymce.get("TemplateCHistoryView").setContent(data.historicText.HIGHLIGHTS_COMMENTS);
                } else {
                    alert("Failed to get highlights text. " + data.message);
                }
            },
            error: function () {
                // Handles any errors that may occur during the AJAX request
                alert("Error get higlights.");
            }
        });
    }

    // Call the function on dropdown change and page load
    $('#FK_TB_EMAIL_BACKUP_ID').on('change', function (e) {
        var selectedEmail = $(this).val();
        var templateId = $('#templateId').text();
        if (selectedEmail !== null && templateId !== null) {
            getEmailBackupText(selectedEmail, templateId);
        }
    }).change(); // Trigger the change event on page load

    $('#FK_TB_HIGHLIGHTS').on('change', function (e) {
        var selectedHighLight = $(this).val();
        var templateId = $('#templateId').text();
        if (selectedHighLight !== null && templateId !== null) {
            getHighLigthsText(selectedHighLight, templateId);
        }
    }).change(); // Trigger the change event on page load

    $('#FK_TB_TEMPLATE_HISTORIC_REMIT_ID').on('change', function (e) {
        var selectedHistoric = $(this).val();
        var templateId = $('#templateId').text();
        if (selectedHistoric !== null && templateId !== null) {
            getHistoricRemitText(selectedHistoric, templateId);
        }
    }).change(); // Trigger the change event on page load

    //Cancel button
    $(".btn-cancel").click(function () {
        document.location.href = window.location.origin + '/Workspace/Index';
    });

    $("#btn-save").click(function (e) {
        create();
    });

});
function create() {
    // get data from template
    var templateData = {
        TEMP_ID: $("#templateId").text(),
        WS_WS_TEMPTAX_ID: $("#Item1_WS_TEMP_TAX_ID").val(),
        WS_TEMP_FOLDER: $("#Item1_WS_TEMP_FOLDER").val(),
        WS_TEMP_SUPPLIER_NAME: $("#Item1_WS_TEMP_SUPPLIER_NAME").val(),
        WS_TEMP_SUPPLIER_NUMBER: $("#Item1_WS_TEMP_SUPPLIER_NAME").val(),
        WS_TEMP_REMIT_TO: $("#Item1_WS_TEMP_REMIT_TO").val(),
        WS_TEMP_SUPPLIER_SITE: $("#Item1_WS_TEMP_SUPPLIER_SITE").val(),
        WS_TEMP_VENDOR_ACCOUNT: $("#Item1_WS_TEMP_VENDOR_ACCOUNT").val(),
        WS_FK_TB_ORACLE_SOURCE_ID: $("#FK_TB_ORACLE_SOURCE_ID").val(),
        WS_TEMP_INVOICE_FORMAT: $("#Item1_WS_TEMP_INVOICE_FORMAT").val(),
        WS_TEMP_INVOICE_TYPE: $("#Item1_WS_TEMP_INVOICE_TYPE").val(),
        WS_TEMP_INVOICE_NOTES: tinymce.get("Item1_WS_TEMP_INVOICE_NOTES").getContent(),
        WS_TEMP_W9_W8: tinymce.get("Item1_WS_TEMP_W9_W8").getContent(),
        WS_TEMP_VSU: tinymce.get("Item1_WS_TEMP_VSU").getContent(),
        WS_TEMP_PAYMENT_METHOD: $("#Item1_WS_TEMP_PAYMENT_METHOD").val(),
        WS_TEMP_REMIT_TOACCOUNT: $("#Item1_WS_TEMP_REMIT_TOACCOUNT").val(),
        WS_FK_TB_ORACLE_PAY_TERMS_ID: $("#FK_TB_ORACLE_PAY_TERMS_ID").val(),
        WS_TEMP_INVOICE_DESCRIPTION: $("#Item1_WS_TEMP_INVOICE_DESCRIPTION").val(),
        WS_TEMP_BILLING_PERIOD: tinymce.get("Item1_WS_TEMP_BILLING_PERIOD").getContent(),
        WS_TEMP_BILLING_PRERIOD_DATE: $("#Item1_WS_TEMP_BILLING_PERIOD").val(),
        WS_TEMP_DISTRIBUTION_SET: $("#Item1_WS_TEMP_DISTRIBUTION_SET").val(),
        WS_TEMP_DISTRIBUTION_COMBINATION: $("#Item1_WS_TEMP_DISTRIBUTION_COMBINATION").val(),
        WS_TEMP_ACCOUNTING_DATE: $("#Item1_WS_TEMP_ACCOUNTING_DATE").val(),
        WS_FK_TB_LEGAL_ENTITY_ID: $("#legalEntityDropdown").val(),
        WS_FK_TB_ORGANIZATION_TYPE_ID: $("#organizationTypeDropdown").val(),
        WS_TEMP_TAXPAYER_ID: $("#Item1_WS_TEMP_TAXPAYER_ID").val(),
        WS_FK_TB_ORACLE_TYPE_ID: $("#FK_TB_ORACLE_TYPE_ID").val(),
        WS_TEMP_ORACLE_DESCRIPTION: tinymce.get("Item1_WS_TEMP_ORACLE_DESCRIPTION").getContent(),
        WS_TEMP_ORACLE_NOTES: tinymce.get("Item1_WS_TEMP_ORACLE_NOTES").getContent(),
        WS_TEMP_ORACLE_INSTRUCTIONS: tinymce.get("Item1_WS_TEMP_ORACLE_INSTRUCTIONS").getContent(),
        WS_CONTACTS_CURRENT: $("#form-Currents").val(),
        WS_CONTACTS_PRIOR: $("#form-Prior").val(),
        WS_FK_TB_APPROVER_ID: $("#approverDropdown").val(),
        WS_TEMP_APPROVER_COMMENTS: $("#Item1_WS_TEMP_APPROVER_COMMENTS").val(),

    };

    // get data from workspace-specific fields
    var workspaceData = {
        WS_STATUS: $("#form-Status").val(),
        WS_REASON: $("#form-Reason").val(),
        WS_EMAIL_RECEIVED: $("#form-Email-Received").val(),
        WS_CREATED_DATE: $("#form-CreateDate").val(),
        WS_SOURCE: $("#form-Source").val(),
        WS_HANDLED_BY: $("#form-Handled-By").val(),
        WS_INVOICE_DATE: $("#form-Invoice-Date").val(),
        WS_DUE_DATE: $("#form-Due-Date").val(),
        WS_AMOUNT: $("#form-Amount").val(),
        WS_INVOICE_NUMBER: $("#form-Invoice-Number").val(),
    };

    var WorkspaceCommentsData = {
        WORKSPACE_INFO: tinymce.get("form-Comments").getContent()
    }

    var WorkspaceLastActionsData = {
        LAST_ACTIONS_INFO: tinymce.get("form-Last-Action").getContent()
    }


    $.ajax({
        url: '/WSCrud/Create/',
        type: 'POST',
        data: {
            templateData, workspaceData,WorkspaceCommentsData,WorkspaceLastActionsData,
        },
        success: function (data) {
            if (data.success) {
                document.location.href = window.location.origin + '/Workspace/Index';
            }
        },
        error: function () {
            alert("An error occurred while saving the record.");
        }
    });
}
