//Global variables to store alias, emails, legal entity and approver
var aliasDataList = [];
var emailDataList = [];
var legalEntityDataList = [];
var approverDataList = [];

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

    // Query to get text from emails according to ID in dropdown
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
    // Query to get text from Historic Remit to according to ID in dropdown

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
    // Query to get text from Highlights history according to ID in dropdown
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

    //Cancel button
    $(".btn-cancel").click(function () {
        document.location.href = window.location.origin + '/Main/Index';
    });

    //Trigger Save function on No alias and emails
    $("#NoAddAlias_Emailbtn").click(function (e) {
        e.preventDefault();
        create();
    });
    //Trigger Save function on templates with alias and emails
    $("#btn_AddAlias_Email").click(function (e) {
        e.preventDefault();
        create();
    });

    //Function to store multiple aliases
    $("#btn-addAlias").click(function (e) {
        var alias = $('#form-AddAlias').val().replace(/[<>]/g, '');
        if (alias != "") {
            aliasDataList.push({ ALIAS_NAME: alias });
            $('#FK_TB_TEMPLATE_ALIAS_ID').append($("<option></option>").text(alias));
            $('#form-AddAlias').val("");
        } else {
            alert("Alias cant be empty");
        }
    });

    //Function to store multiple legal entity
    $("#btn-addLegalEntity").click(function (e) {
        var legalEntity = $('#form-AddLegalEntity').val().replace(/[<>]/g, '');
        if (legalEntity != "") {
            legalEntityDataList.push({ LEGAL_ENTITY: legalEntity });
            $('#FK_TB_TEMPLATE_LEGALENTI_ID').append($("<option></option>").text(legalEntity));
            $('#form-AddLegalEntity').val("");

        } else {
            alert("Legal Entity cant be empty");
        };
    };

    //Function to store multiple emails
    $("#ButtonAddEmail").click(function (e) {
        var email = $('#form-BackUpEmail').val().replace(/[<>]/g, '');
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

//funtion to check that all requered fields are filled
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
        // The form is valid
        return true;
    } else {
        // If a field in the form thats required is not filled
        alert("Please fill all the required fields");
        return false;
    }
}

function create() {

    // get data from template .replace(/[<>]/g, '') is to remove <> that can cause issues to save
    var templateData = {
        TEMP_TAX_ID: $("#form-TaxID").val(),
        TEMP_REMIT_TO: $("#form-RemitTo").val().replace(/[<>]/g, ''),
        TEMP_SUPPLIER_NAME: $("#form-SupName").val().replace(/[<>]/g, ''),
        TEMP_VENDOR_ACCOUNT: $("#form-OurVendorA").val().replace(/[<>]/g, ''),
        TEMP_SUPPLIER_NUMBER: $("#form-SupNumber").val().replace(/[<>]/g, ''),
        TEMP_SUPPLIER_SITE: $("#form-SupSite").val().replace(/[<>]/g, ''),
        TEMP_ADDRESS: $("#form-Address").val().replace(/[<>]/g, ''),
        FK_TB_LEGAL_ENTITY_ID: $("#FK_TB_LEGAL_ENTITY_ID").val().replace(/[<>]/g, ''),
        TEMP_TAXPAYER_ID: $("#form-FirstParty").val().replace(/[<>]/g, ''),
        FK_TB_ORACLE_TYPE_ID: $("#FK_TB_ORACLE_TYPE_ID").val().replace(/[<>]/g, ''),
        TEMP_ORACLE_DESCRIPTION: $("#form-Description").val().replace(/[<>]/g, ''),
        FK_TB_ORACLE_PAY_TERMS_ID: $("#FK_TB_ORACLE_PAY_TERMS_ID").val().replace(/[<>]/g, ''),
        TEMP_ACCOUNT_CODING: $("#form-AccountC").val().replace(/[<>]/g, ''),
        FK_TB_ORACLE_SOURCE_ID: $("#FK_TB_ORACLE_SOURCE_ID").val().replace(/[<>]/g, ''),
        TEMP_ORACLE_NOTES: $("#form-OracleN").val().replace(/[<>]/g, ''),
        TEMP_ORACLE_INSTRUCTIONS: $("#form-OracleI").val().replace(/[<>]/g, ''),
        FK_TB_APPROVER_ID: $("#FK_TB_APPROVER_ID").val().replace(/[<>]/g, ''),
        TEMP_APPROVER_COMMENTS: $("#form-ApproverComents").val().replace(/[<>]/g, ''),
        TEMP_INVOICE_FORMAT: $("#form-InvoiceF").val().replace(/[<>]/g, ''),
        TEMP_INVOICE_TYPE: $("#form-INFType").val().replace(/[<>]/g, '')
    };

    var HistoricRemitToData = { //data to TB_HISTORIC REMIT
        HISTORIC_REMIT_INFO: $("#form-HistRemitTo").val().replace(/[<>]/g, '')
    };

    var HighLightsData = { //data to TB_HIGHLIGHTS
        HIGHLIGHTS: $("#form-Higlights").val().replace(/[<>]/g, ''),
        HIGHLIGHTS_COMMENTS: $("#form-HiglightsC").val().replace(/[<>]/g, ''),
        HIGHLIGHTS_INSTRUCTIONS: $("#form-Instructions").val().replace(/[<>]/g, ''),
        HIGHLIGHTS_EXCEPTIONS: $("#form-Exceptions").val().replace(/[<>]/g, ''),
        HIGHLIGHTS_COMMON_ISSUES: $("#form-MostCI").val().replace(/[<>]/g, ''),
        HIGHLIGHTS_SUPPLIER_AGENCY: $("#form-SupplierA").val().replace(/[<>]/g, ''),
        HIGHLIGHTS_TEMPLATE_COMMENTS: $("#form-TemplateC").val().replace(/[<>]/g, ''),

    };

    $.ajax({
        url: '/Main/Create',
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
    // get data from template .replace(/[<>]/g, '') is to remove <> that can cause issues to save
    var templateData = {
        TEMP_ID: $("#templateId").text().replace(/[<>]/g, ''),
        TEMP_TAX_ID: $("#TEMP_TAX_ID").val().replace(/[<>]/g, ''),
        TEMP_REMIT_TO: $("#TEMP_REMIT_TO").val().replace(/[<>]/g, ''),
        TEMP_SUPPLIER_NAME: $("#TEMP_SUPPLIER_NAME").val().replace(/[<>]/g, ''),
        TEMP_VENDOR_ACCOUNT: $("#TEMP_VENDOR_ACCOUNT").val().replace(/[<>]/g, ''),
        TEMP_SUPPLIER_NUMBER: $("#TEMP_SUPPLIER_NUMBER").val().replace(/[<>]/g, ''),
        TEMP_SUPPLIER_SITE: $("#TEMP_SUPPLIER_SITE").val().replace(/[<>]/g, ''),
        TEMP_ADDRESS: $("#TEMP_ADDRESS").val().replace(/[<>]/g, ''),
        FK_TB_LEGAL_ENTITY_ID: $("#FK_TB_LEGAL_ENTITY_ID").val().replace(/[<>]/g, ''),
        TEMP_TAXPAYER_ID: $("#TEMP_TAXPAYER_ID").val().replace(/[<>]/g, ''),
        FK_TB_ORACLE_TYPE_ID: $("#FK_TB_ORACLE_TYPE_ID").val().replace(/[<>]/g, ''),
        TEMP_ORACLE_DESCRIPTION: $("#TEMP_ORACLE_DESCRIPTION").val().replace(/[<>]/g, ''),
        FK_TB_ORACLE_PAY_TERMS_ID: $("#FK_TB_ORACLE_PAY_TERMS_ID").val().replace(/[<>]/g, ''),
        TEMP_ACCOUNT_CODING: $("#TEMP_ACCOUNT_CODING").val().replace(/[<>]/g, ''),
        FK_TB_ORACLE_SOURCE_ID: $("#FK_TB_ORACLE_SOURCE_ID").val().replace(/[<>]/g, ''),
        TEMP_ORACLE_NOTES: $("#TEMP_ORACLE_NOTES").val().replace(/[<>]/g, ''),
        TEMP_ORACLE_INSTRUCTIONS: $("#TEMP_ORACLE_INSTRUCTIONS").val().replace(/[<>]/g, ''),
        FK_TB_APPROVER_ID: $("#FK_TB_APPROVER_ID").val().replace(/[<>]/g, ''),
        TEMP_APPROVER_COMMENTS: $("#TEMP_APPROVER_COMMENTS").val().replace(/[<>]/g, ''),
        TEMP_INVOICE_FORMAT: $("#TEMP_INVOICE_FORMAT").val().replace(/[<>]/g, ''),
        TEMP_INVOICE_TYPE: $("#TEMP_INVOICE_TYPE").val().replace(/[<>]/g, '')
    };

    var HistoricRemitToData = { //data to TB_HISTORIC REMIT
        HISTORIC_REMIT_INFO: $("#TB_HISTORIC_REMIT1_HISTORIC_REMIT_INFO").val().replace(/[<>]/g, '')
    };

    var HighLightsData = { //data to TB_HIGHLIGHTS
        HIGHLIGHTS: $("#TB_HIGHLIGHTS1_HIGHLIGHTS").val().replace(/[<>]/g, ''),
        HIGHLIGHTS_COMMENTS: $("#TB_HIGHLIGHTS1_HIGHLIGHTS_COMMENTS").val().replace(/[<>]/g, ''),
        HIGHLIGHTS_INSTRUCTIONS: $("#TB_HIGHLIGHTS1_HIGHLIGHTS_INSTRUCTIONS").val().replace(/[<>]/g, ''),
        HIGHLIGHTS_EXCEPTIONS: $("#TB_HIGHLIGHTS1_HIGHLIGHTS_EXCEPTIONS").val().replace(/[<>]/g, ''),
        HIGHLIGHTS_COMMON_ISSUES: $("#TB_HIGHLIGHTS1_HIGHLIGHTS_COMMON_ISSUES").val().replace(/[<>]/g, ''),
        HIGHLIGHTS_SUPPLIER_AGENCY: $("#TB_HIGHLIGHTS1_HIGHLIGHTS_SUPPLIER_AGENCY").val().replace(/[<>]/g, ''),
        HIGHLIGHTS_TEMPLATE_COMMENTS: $("#TB_HIGHLIGHTS1_HIGHLIGHTS_TEMPLATE_COMMENTS").val().replace(/[<>]/g, ''),

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