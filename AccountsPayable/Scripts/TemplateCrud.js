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
    $(".btn-submit").click(function (e) {
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
            url: '/Main/Create',
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