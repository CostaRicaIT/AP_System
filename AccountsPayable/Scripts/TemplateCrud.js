var aliasDataList = [];
var emailDataList = [];
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
        toggleInputState($(this), $('#TEMP_TAX_ID'));
    });

    $('#BoxNoRemitInfo').change(function () {
        toggleInputState($(this), $('#form-RemitTo'));
        toggleInputState($(this), $('#TEMP_REMIT_TO'));
    });

    $('#BoxSupName').change(function () {
        toggleInputState($(this), $('#form-SupName'));
        toggleInputState($(this), $('#TEMP_SUPPLIER_NAME'));

    });
    function getEmailBackupText(selectedEmail, templateId) {
        if (selectedEmail != 0) {
            $.ajax({
                url: '/Main/GetEmailText',
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

    function getHistoricRemitText(selectedHistoric, templateId) {
        $.ajax({
            url: '/Main/GetHistoricRemitText',
            type: 'GET',
            data: { historicId: selectedHistoric, id: templateId },
            success: function (data) {
                // Check if the data is retrieved successfully
                if (data.success) {
                    // Updates the content of the textarea with the historic email text
                    var historicRemit = data.historicText;
                    $('#historicRemitInfoView').val(historicRemit);
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

    function getHighLigthsText(selectedHighLight, templateId) {
        $.ajax({
            url: '/Main/GetHighlights',
            type: 'GET',
            data: { highlightsId: selectedHighLight, id: templateId },
            success: function (data) {
                // Check if the data is retrieved successfully
                if (data.success) {

                    // Updates the content of the textarea with the highlights text
                    $('#HighlightsHistoryView').val(data.historicText.HIGHLIGHTS);
                    $('#HighlightsCHistoryView').val(data.historicText.HIGHLIGHTS_COMMENTS);
                    $('#InstructionsHistoryView').val(data.historicText.HIGHLIGHTS_INSTRUCTIONS);
                    $('#ExceptionsHistoryView').val(data.historicText.HIGHLIGHTS_EXCEPTIONS);
                    $('#MostCIHistoryView').val(data.historicText.HIGHLIGHTS_COMMON_ISSUES);
                    $('#SupplierAHistoryView').val(data.historicText.HIGHLIGHTS_SUPPLIER_AGENCY);
                    $('#TemplateCHistoryView').val(data.historicText.HIGHLIGHTS_COMMENTS);
                } else {
                    alert("Failed to get highlights text. " + data.message);
                }
            },
            error: function () {
                // Handles any errors that may occur during the AJAX request
                alert("Error get historic Remit text.");
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
    $(".btn-cancel").click(function () {
        document.location.href = window.location.origin + '/Main/Index';
    });

    $('#FK_TB_TEMPLATE_HISTORIC_REMIT_ID').on('change', function (e) {
        var selectedHistoric = $(this).val();
        var templateId = $('#templateId').text();
        if (selectedHistoric !== null && templateId !== null) {
            getHistoricRemitText(selectedHistoric, templateId);
        }
    }).change(); // Trigger the change event on page load
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
    $("#btn-addAlias").click(function (e) {
        var alias = $('#form-AddAlias').val();
        if (alias != "") {
            aliasDataList.push({ ALIAS_NAME: alias });
            $('#FK_TB_TEMPLATE_ALIAS_ID').append($("<option></option>").text(alias));
            $('#form-AddAlias').val("");
        } else {
            alert("Alias cant be empty");
        }
    });
    $("#ButtonAddEmail").click(function (e) {
        var email = $('#form-BackUpEmail').val();
        var date = new Date();
        var formattedDate = date.toLocaleString('en-US', { month: '2-digit', day: '2-digit', year: 'numeric', hour: '2-digit', minute: '2-digit', hour12: true });
        if (email != "") {
            emailDataList.push({ EMAIL_BACKUP: email });
            $('#FK_TB_EMAIL_BACKUP_ID').append($("<option></option>").text(formattedDate).attr('value', '0'));
        } else {
            alert("Backup email cant be empty");
        }
    });
    $(".btn-cancel").click(function () {
        document.location.href = window.location.origin + '/Main/Index';
    });
    $("#btn-update").click(function (e) {
        if (checkFormValidity()) {
            update();
        }
    });
});
function checkFormValidity() {
    var form = $("#form")[0];

    // Reset previous error messages and remove focus
    $(".validation-message").remove();
    $(":input").removeClass("invalid-field");

    var firstInvalidField = null;

    // Check each input field for validity
    $(form).find(":input").each(function () {
        // Check if the field is required and not empty
        if (this.required && !$(this).val()) {
            // Display a message in a label associated with the field
            $(this).closest('div').append('<label class="validation-message text-danger">' + 'This field is required</label>');

            // Add a class to highlight the invalid field
            $(this).addClass("invalid-field");

            // Set focus to the first invalid field
            if (!firstInvalidField) {
                firstInvalidField = this;
            }
        }
    });

    if (firstInvalidField) {
        firstInvalidField.focus();
    }

    if (form.checkValidity()) {
        // The form is valid, you can proceed with your logic
        return true;
    } else {
        // The form is invalid, you can display an overall error message or handle it accordingly
        alert("Please fill all the required fields");
        return false;
    }
}

function saveNoAliasEmail() {
    var templateData = {
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

    $.ajax({
        url: '/Main/CreateWithEmail_Alias',
        type: 'POST',
        data: {
            templateData, HistoricRemitToData, HighLightsData, aliasDataList, emailDataList
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


function update() {
    var templateData = {
        TEMP_ID: $("#templateId").text(),
        TEMP_TAX_ID: $("#TEMP_TAX_ID").val(),
        TEMP_REMIT_TO: $("#TEMP_REMIT_TO").val(),
        TEMP_SUPPLIER_NAME: $("#TEMP_SUPPLIER_NAME").val(),
        TEMP_VENDOR_ACCOUNT: $("#TEMP_VENDOR_ACCOUNT").val(),
        TEMP_SUPPLIER_NUMBER: $("#TEMP_SUPPLIER_NUMBER").val(),
        TEMP_SUPPLIER_SITE: $("#TEMP_SUPPLIER_SITE").val(),
        TEMP_ADDRESS: $("#TEMP_ADDRESS").val(),
        FK_TB_LEGAL_ENTITY_ID: $("#FK_TB_LEGAL_ENTITY_ID").val(),
        TEMP_TAXPAYER_ID: $("#TEMP_TAXPAYER_ID").val(),
        FK_TB_ORACLE_TYPE_ID: $("#FK_TB_ORACLE_TYPE_ID").val(),
        TEMP_ORACLE_DESCRIPTION: $("#TEMP_ORACLE_DESCRIPTION").val(),
        FK_TB_ORACLE_PAY_TERMS_ID: $("#FK_TB_ORACLE_PAY_TERMS_ID").val(),
        TEMP_ACCOUNT_CODING: $("#TEMP_ACCOUNT_CODING").val(),
        FK_TB_ORACLE_SOURCE_ID: $("#FK_TB_ORACLE_SOURCE_ID").val(),
        TEMP_ORACLE_NOTES: $("#TEMP_ORACLE_NOTES").val(),
        TEMP_ORACLE_INSTRUCTIONS: $("#TEMP_ORACLE_INSTRUCTIONS").val(),
        FK_TB_APPROVER_ID: $("#FK_TB_APPROVER_ID").val(),
        TEMP_APPROVER_COMMENTS: $("#TEMP_APPROVER_COMMENTS").val(),
        TEMP_INVOICE_FORMAT: $("#TEMP_INVOICE_FORMAT").val(),
        TEMP_INVOICE_TYPE: $("#TEMP_INVOICE_TYPE").val()
    };

    var HistoricRemitToData = { //data to TB_HISTORIC REMIT
        HISTORIC_REMIT_INFO: $("#TB_HISTORIC_REMIT1_HISTORIC_REMIT_INFO").val()
    };

    var HighLightsData = { //data to TB_HIGHLIGHTS
        HIGHLIGHTS: $("#TB_HIGHLIGHTS1_HIGHLIGHTS").val(),
        HIGHLIGHTS_COMMENTS: $("#TB_HIGHLIGHTS1_HIGHLIGHTS_COMMENTS").val(),
        HIGHLIGHTS_INSTRUCTIONS: $("#TB_HIGHLIGHTS1_HIGHLIGHTS_INSTRUCTIONS").val(),
        HIGHLIGHTS_EXCEPTIONS: $("#TB_HIGHLIGHTS1_HIGHLIGHTS_EXCEPTIONS").val(),
        HIGHLIGHTS_COMMON_ISSUES: $("#TB_HIGHLIGHTS1_HIGHLIGHTS_COMMON_ISSUES").val(),
        HIGHLIGHTS_SUPPLIER_AGENCY: $("#TB_HIGHLIGHTS1_HIGHLIGHTS_SUPPLIER_AGENCY").val(),
        HIGHLIGHTS_TEMPLATE_COMMENTS: $("#TB_HIGHLIGHTS1_HIGHLIGHTS_TEMPLATE_COMMENTS").val(),

    };

    $.ajax({
        url: '/Main/Edit',
        type: 'POST',
        data: {
            templateData, HistoricRemitToData, HighLightsData, aliasDataList, emailDataList
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