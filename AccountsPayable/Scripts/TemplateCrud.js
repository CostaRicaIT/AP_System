//Global variables to store alias, emails, legal entity and approver
var aliasDataList = [];
var emailDataList = [];
var legalEntityDataList = [];
var approverDataList = [];
var organizationTypeDataList = [];

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
        update();
    });

    //Function to store more legal entity data to the dropdown
    $(document).ready(function () {

        // Function to sort alphabetic the legal entity in dropdown
        function sortLegalEntity() {

            let options = $("#legalEntityDropdown option");
            options.sort(function (a, b) {
                return a.text.localeCompare(b.text);
            });
            $("#legalEntityDropdown").empty().append(options);
        };

        // Button click event to add Legal Entity to dropdown
        $("#btn-addLegalEntity").on('click', function () {

            // Get the value from the input field
            let newLegalEntity = $("#form-AddLegalEntity").val();

            //Check if the value is not empty
            if (newLegalEntity.trim() != '') {

                //AJAX to made the request to the server
                $.ajax({
                    url: '/CRUD/AddLegalEntity',
                    type: 'POST',
                    data: { legalEntityName: newLegalEntity },
                    success: function (data) {

                        $('#legalEntityDropdown').append('<option value="' + data.id + '">' + data.name + '</option>');
                        $('#form-AddLegalEntity').val('');

                        alert("New legal entity added to the list section.");

                        // Call the function to sort
                        sortLegalEntity();

                    },

                    error: function (error) {
                        console.error('Error:', error);
                    }

                });
            }
        });

        // Call the function to sort
        sortLegalEntity();

    });
    //Function to store more organization type data to the dropdown
    $(document).ready(function () {

        // Function to sort alphabetic the organization type in dropdown
        function sortOrganizationType() {

            let options = $("#organizationTypeDropdown option");
            options.sort(function (a, b) {
                return a.text.localeCompare(b.text);
            });
            $("#organizationTypeDropdown").empty().append(options);
        };

        // Button click event to add Legal Entity to dropdown
        $("#btn-AddOrganizationType").on('click', function () {

            // Get the value from the input field
            let newOrganizationType = $("#form-AddOrganizationType").val();

            //Check if the value is not empty
            if (newOrganizationType.trim() != '') {

                //AJAX to made the request to the server
                $.ajax({
                    url: '/CRUD/AddOrganizationType',
                    type: 'POST',
                    data: { organizationTypeName: newOrganizationType },
                    success: function (data) {

                        $('#organizationTypeDropdown').append('<option value="' + data.id + '">' + data.name + '</option>');
                        $('#form-AddOrganizationType').val('');

                        alert("New Organization Type added to the list section.");

                        // Call the function to sort
                        sortOrganizationType();

                    },

                    error: function (error) {
                        console.error('Error:', error);
                    }

                });
            }
        });

        // Call the function to sort
        sortOrganizationType();

    });

    //Function to store more approver data to the dropdown
    $(document).ready(function () {

        // Function to sort alphabetic the approver in dropdown
        function sortApprover() {

            let options = $("#approverDropdown option");
            options.sort(function (a, b) {
                return a.text.localeCompare(b.text);
            });
            $("#approverDropdown").empty().append(options);
        };

        // Button click event to add approver to dropdown
        $("#btn-addAppover").on('click', function () {

            // Get the value from the input field
            let newApprover = $("#form-AddApprover").val();

            //Check if the value is not empty
            if (newApprover.trim() != '') {

                //AJAX to made the request to the server
                $.ajax({
                    url: '/CRUD/AddApprover',
                    type: 'POST',
                    data: { approverName: newApprover },
                    success: function (data) {

                        $('#approverDropdown').append('<option value="' + data.id + '">' + data.name + '</option>');
                        $('#form-AddApprover').val('');

                        alert("New approver added to the list section.");

                        // Call the function to sort
                        sortApprover();
                    },
                    error: function (error) {
                        console.error('Error:', error);
                    }
                });
            }
        });
        // Call the function to sort
        sortApprover();

    });

});
function create() {
    // get data from template .replace(/[<>]/g, '') is to remove <> that can cause issues to save
    var templateData = {
        TEMP_TAX_ID: tinymce.get("form-TaxID").getContent(),
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
        TEMP_DISTRIBUTION_SET: tinymce.get("form-DistroSet").getContent(),
        FK_TB_ORACLE_SOURCE_ID: $("#FK_TB_ORACLE_SOURCE_ID").val().replace(/[<>]/g, ''),
        TEMP_ORACLE_NOTES: tinymce.get("form-OracleN").getContent(),
        TEMP_ORACLE_INSTRUCTIONS: tinymce.get("form-OracleI").getContent(),
        FK_TB_APPROVER_ID: $("#approverDropdown").val().replace(/[<>]/g, ''),
        TEMP_APPROVER_COMMENTS: $("#form-ApproverComents").val().replace(/[<>]/g, ''),
        TEMP_INVOICE_FORMAT: $("#form-InvoiceF").val().replace(/[<>]/g, ''),
        TEMP_INVOICE_TYPE: $("#form-INFType").val().replace(/[<>]/g, ''),
        TEMP_FOLDER: tinymce.get("form-Folder").getContent(),
        TEMP_PAYMENT_METHOD: $("#form-PayMethod").val().replace(/[<>]/g, ''),
        TEMP_REMIT_TOACCOUNT: tinymce.get("form-RemitToAccount").getContent(),
        TEMP_BILLING_PERIOD: tinymce.get("form-BillPeriod").getContent(),
        TEMP_BILLING_PRERIOD_DATE: $("#form-Dates").val().replace(/[<>]/g, ''),
        TEMP_DISTRIBUTION_COMBINATION: tinymce.get("form-DistroCombination").getContent(),
        TEMP_ACCOUNTING_DATE: $("#form-AccoDate").val().replace(/[<>]/g, ''),
        TEMP_VSU: tinymce.get("form-VSU").getContent(),
        TEMP_W9_W8: tinymce.get('form-W9W8').getContent(),
        TEMP_INVOICE_NOTES: tinymce.get("form-InvoiceNotes").getContent(),
        TEMP_INVOICE_DESCRIPTION: $("#form-InvoiceDescrip").val().replace(/[<>]/g, ''),
        CONTACTS_CURRENT: $("#form-Currents").val().replace(/[<>]/g, ''),
        CONTACTS_PRIOR: $("#form-Prior").val().replace(/[<>]/g, ''),
        FK_TB_ORGANIZATION_TYPE_ID: $("#organizationTypeDropdown").val().replace(/[<>]/g, '')
    };

    var HistoricRemitToData = { //data to TB_HISTORIC REMIT
        HISTORIC_REMIT_INFO: tinymce.get("form-HistRemitTo").getContent()
    };

    var HighLightsData = { //data to TB_HIGHLIGHTS
        HIGHLIGHTS: tinymce.get("form-Higlights").getContent(),
        HIGHLIGHTS_COMMENTS: tinymce.get("form-HiglightsC").getContent(),
        HIGHLIGHTS_INSTRUCTIONS: tinymce.get("form-Instructions").getContent(),
        HIGHLIGHTS_EXCEPTIONS: tinymce.get("form-Exceptions").getContent(),
        HIGHLIGHTS_COMMON_ISSUES: tinymce.get("form-MostCI").getContent(),
        HIGHLIGHTS_SUPPLIER_AGENCY: tinymce.get("form-SupplierA").getContent(),
        HIGHLIGHTS_TEMPLATE_COMMENTS: tinymce.get("form-TemplateC").getContent(),


    };

    $.ajax({
        url: '/CRUD/Create/',
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
        TEMP_ID: $("#templateId").text(),
        TEMP_TAX_ID: tinymce.get("form-TaxID").getContent(),
        TEMP_REMIT_TO: $("#TEMP_REMIT_TO").val(),
        TEMP_SUPPLIER_NAME: $("#TEMP_SUPPLIER_NAME").val(),
        TEMP_VENDOR_ACCOUNT: $("#TEMP_VENDOR_ACCOUNT").val(),
        TEMP_SUPPLIER_NUMBER: $("#TEMP_SUPPLIER_NUMBER").val(),
        TEMP_SUPPLIER_SITE: $("#TEMP_SUPPLIER_SITE").val(),       
        FK_TB_LEGAL_ENTITY_ID: $("#FK_TB_LEGAL_ENTITY_ID").val(),
        TEMP_TAXPAYER_ID: $("#TEMP_TAXPAYER_ID").val(),
        FK_TB_ORACLE_TYPE_ID: $("#FK_TB_ORACLE_TYPE_ID").val(),
        TEMP_ORACLE_DESCRIPTION: tinymce.get("TEMP_ORACLE_DESCRIPTION").getContent(),
        FK_TB_ORACLE_PAY_TERMS_ID: $("#FK_TB_ORACLE_PAY_TERMS_ID").val(),
        TEMP_DISTRIBUTION_SET: tinymce.get("form-DistroSet").getContent(),
        FK_TB_ORACLE_SOURCE_ID: $("#FK_TB_ORACLE_SOURCE_ID").val(),
        TEMP_ORACLE_NOTES: tinymce.get("TEMP_ORACLE_NOTES").getContent(),
        TEMP_ORACLE_INSTRUCTIONS: tinymce.get("TEMP_ORACLE_INSTRUCTIONS").getContent(),
        FK_TB_APPROVER_ID: $("#FK_TB_APPROVER_ID").val(),
        TEMP_APPROVER_COMMENTS: $("#TEMP_APPROVER_COMMENTS").val(),
        TEMP_INVOICE_FORMAT: $("#TEMP_INVOICE_FORMAT").val(),
        TEMP_INVOICE_TYPE: $("#TEMP_INVOICE_TYPE").val(),
        TEMP_FOLDER: tinymce.get("form-Folder").getContent(),
        TEMP_PAYMENT_METHOD: $("#TEMP_PAYMENT_METHOD").val(),
        TEMP_REMIT_TOACCOUNT: tinymce.get("form-RemitToAccount").getContent(),
        TEMP_BILLING_PERIOD: tinymce.get("TEMP_BILLING_PERIOD").getContent(),
        TEMP_BILLING_PRERIOD_DATE: $("#form-Dates").val(),
        TEMP_DISTRIBUTION_COMBINATION: tinymce.get("form-DistroCombination").getContent(),
        TEMP_ACCOUNTING_DATE: $("#TEMP_ACCOUNTING_DATE").val(),
        TEMP_VSU: tinymce.get("TEMP_VSU").getContent(),
        TEMP_W9_W8: tinymce.get("TEMP_W9_W8").getContent(),
        TEMP_INVOICE_NOTES: tinymce.get("TEMP_INVOICE_NOTES").getContent(),
        TEMP_INVOICE_DESCRIPTION: $("#TEMP_INVOICE_DESCRIPTION").val(),
        CONTACTS_CURRENT: $("#form-Currents").val(),
        CONTACTS_PRIOR: $("#form-Prior").val(),
        FK_TB_ORGANIZATION_TYPE_ID: $("#FK_TB_ORGANIZATION_TYPE_ID").val(),
    };

    var HistoricRemitToData = { //data to TB_HISTORIC REMIT
        HISTORIC_REMIT_INFO: tinymce.get("TB_HISTORIC_REMIT1_HISTORIC_REMIT_INFO").getContent()
    };

    var HighLightsData = { //data to TB_HIGHLIGHTS
        HIGHLIGHTS: tinymce.get("TB_HIGHLIGHTS1_HIGHLIGHTS").getContent(),
        HIGHLIGHTS_COMMENTS: tinymce.get("TB_HIGHLIGHTS1_HIGHLIGHTS_COMMENTS").getContent(),
        HIGHLIGHTS_INSTRUCTIONS: tinymce.get("TB_HIGHLIGHTS1_HIGHLIGHTS_INSTRUCTIONS").getContent(),
        HIGHLIGHTS_EXCEPTIONS: tinymce.get("TB_HIGHLIGHTS1_HIGHLIGHTS_EXCEPTIONS").getContent(),
        HIGHLIGHTS_COMMON_ISSUES: tinymce.get("TB_HIGHLIGHTS1_HIGHLIGHTS_COMMON_ISSUES").getContent(),
        HIGHLIGHTS_SUPPLIER_AGENCY: tinymce.get("TB_HIGHLIGHTS1_HIGHLIGHTS_SUPPLIER_AGENCY").getContent(),
        HIGHLIGHTS_TEMPLATE_COMMENTS: tinymce.get("TB_HIGHLIGHTS1_HIGHLIGHTS_TEMPLATE_COMMENTS").getContent(),

    };

    $.ajax({
        url: '/CRUD/Edit',
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