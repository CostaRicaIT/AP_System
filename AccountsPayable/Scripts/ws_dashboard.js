var table; // Declare the 'table' variable in a global scope
var defaultColumns = [1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 48]; //

function showFilterModal() {
    // Open the modal
    $('#toggleColumnsModal').modal('show');

    // Initialize the checkboxes in the modal based on current visibility
    $('.toggle-column').each(function () {
        var columnIdx = $(this).data('column');
        var isVisible = table.column(columnIdx).visible();
        $(this).prop('checked', isVisible);
    });

    // Save button click event
    $('#saveColumnVisibility').on('click', function () {
        // Iterate through checkboxes and update column visibility
        $('.toggle-column').each(function () {
            var columnIdx = $(this).data('column');
            var isVisible = $(this).is(':checked');
            table.column(columnIdx).visible(isVisible);
            wrapScrollableContent();
        });

        // Close the modal
        $('#toggleColumnsModal').modal('hide');
    });
}

function resetFilters() {
    table.columns().visible(false);
    table.state.clear();
    $('#filtersModal').modal('hide');
    table.columns(defaultColumns).visible(true);
}

function wrapScrollableContent() {
    $('#dataTable tbody td.scrollable-content').each(function () {
        if (!$(this).children('div').length) { // Prevent double-wrapping
            var cellContent = $(this).html();
            $(this).html(`<div class="scrollable-content">${cellContent}</div>`);
        }
    });
}

function DrawTable() {
    var userRoleId = 0;
    // Initialize the DataTable
    table = $('#wsdataTable').DataTable({
        pageLength: 25,
        orderCellsTop: true, //Show table sorting at top
        autoWidth: true, //Set the widht of collumn depending on content
        responsive: true, //Set the table to be responsive
        scrollCollapse: true, //Add scroll to table, scrollY and Scroll X are max size on each axis that table can have
        scrollY: '60vh',
        scrollX: '80vh',
        processing: true,
        serverSide: true,
        filter: true,
        dom: '<"top"lBf>rt<"bottom"ip>', // set the order of table items to be drawn
        ajax: {
            "url": "/WorkSpace/GetWorkspaceData",
            "type": "POST",
            "datatype": "json",
            "dataSrc": function (json) {
                userRoleId = json.UserRoleId; // Extract UserRoleId from the response
                return json.data; // Return the data to populate the table
            },
            "error": function (xhr, error, thrown) {
                console.error("Error fetching data: ", error); // Log errors
            }
        },
        columns: [
            { data: 'WS_Id', name: 'WS_ID', className: "scrollable-content" },
            { data: 'TempTaxId', name: 'WS_TEMP_TAX_ID', className: "scrollable-content" },
            { data: 'Ws_duedate', name: 'WS_DUE_DATE', className: "scrollable-content" },
            { data: 'Ws_status', name: 'WS_STATUS', className: "scrollable-content" },
            { data: 'Ws_handled_by', name: 'WS_HANDLED_BY', className: "scrollable-content" },
            { data: 'Ws_created_date', name: 'WS_CREATED_DATE', className: "scrollable-content" },
            { data: 'TempSupplierName', name: 'WS_TEMP_SUPPLIER_NAME', className: "scrollable-content" },
            { data: 'Ws_invoice_number', name: 'WS_INVOICE_NUMBER', className: "scrollable-content" },
            { data: 'Ws_amount', name: 'WS_AMOUNT', className: "scrollable-content" },
            { data: 'Ws_invoice_date', name: 'WS_INVOICE_DATE', className: "scrollable-content" },
            { data: 'TempSupplierNumber', name: 'WS_TEMP_SUPPLIER_NUMBER', className: "scrollable-content" },
            { data: 'Id', name: 'TEMP_ID' },
            { data: 'Alias', name: 'WS_FK_TB_TEMPLATE_ALIAS_ID', className: "scrollable-content" },
            { data: 'Folder', name: 'WS_TEMP_FOLDER', className: "scrollable-content" },
            { data: 'Ws_reason', name: 'WS_REASON', className: "scrollable-content" },
            { data: 'Ws_source', name: 'WS_SOURCE', className: "scrollable-content" },
            { data: 'Ws_email_received', name: 'WS_EMAIL_RECEIVED', className: "scrollable-content" },
            { data: 'RemitTo', name: 'WS_TEMP_REMIT_TO', className: "scrollable-content" },
            { data: 'SupplierSite', name: 'WS_TEMP_SUPPLIER_SITE', className: "scrollable-content" },
            { data: 'HistoricRemitTo', name: 'WS_FK_TB_TEMPLATE_HISTORIC_REMIT_ID', className: "scrollable-content" },
            { data: 'VendorAccount', name: 'WS_TEMP_VENDOR_ACCOUNT', className: "scrollable-content", className: "scrollable-content" },
            { data: 'Source', name: 'WS_FK_TB_ORACLE_SOURCE_ID', className: "scrollable-content" },
            { data: 'InvoiceFormat', name: 'WS_TEMP_INVOICE_FORMAT', className: "scrollable-content" },
            { data: 'InvoiceType', name: 'WS_TEMP_INVOICE_TYPE', className: "scrollable-content" },
            { data: 'InvoiceNotes', name: 'WS_TEMP_INVOICE_NOTES', className: "scrollable-content" },
            { data: 'W9W8BENForm', name: 'WS_TEMP_W9_W8', className: "scrollable-content" },
            { data: 'VSUForm', name: 'WS_TEMP_VSU', className: "scrollable-content" },
            { data: 'PaymentMethod', name: 'WS_TEMP_PAYMENT_METHOD', className: "scrollable-content" },
            { data: 'RemitToAccount', name: 'WS_TEMP_REMIT_TOACCOUNT', className: "scrollable-content" },
            { data: 'PayTerms', name: 'WS_FK_TB_ORACLE_PAY_TERMS_ID', className: "scrollable-content" },
            { data: 'InvoiceDescription', name: 'WS_TEMP_INVOICE_DESCRIPTION', className: "scrollable-content" },
            { data: 'BillingPeriod', name: 'WS_TEMP_BILLING_PERIOD', className: "scrollable-content" },
            { data: 'Dates', name: 'WS_TEMP_BILLING_PERIOD_DATE', className: "scrollable-content" },
            { data: 'DistributionSet', name: 'WS_TEMP_DISTRIBUTION_SET', className: "scrollable-content" },
            { data: 'DistributionCombination', name: 'WS_TEMP_DISTRIBUTION_COMBINATION', className: "scrollable-content" },
            { data: 'AccountingDate', name: 'WS_TEMP_ACCOUNTING_DATE' },
            { data: 'LegalEntity', name: 'WS_FK_TB_LEGAL_ENTITY_ID', className: "scrollable-content" },
            { data: 'OrganizationType', name: 'WS_FK_TB_ORGANIZATION_TYPE_ID', className: "scrollable-content" },
            { data: 'TaxPayerID', name: 'WS_TEMP_TAXPAYER_ID', className: "scrollable-content" },
            { data: 'Type', name: 'WS_FK_TB_ORACLE_TYPE_ID', className: "scrollable-content" },
            { data: 'Description', name: 'WS_TEMP_ORACLE_DESCRIPTION', className: "scrollable-content" },
            { data: 'OracleNotes', name: 'WS_TEMP_ORACLE_NOTES', className: "scrollable-content" },
            { data: 'OracleInstructions', name: 'WS_TEMP_ORACLE_INSTRUCTIONS', className: "scrollable-content" },
            { data: 'ARKeyContactsCurrent', name: 'WS_CONTACTS_CURRENT', className: "scrollable-content" },
            { data: 'ARKeyContactsPrior', name: 'WS_CONTACTS_PRIOR', className: "scrollable-content" },
            { data: 'Approver', name: 'WS_FK_TB_APPROVER_ID', className: "scrollable-content" },
            { data: 'ApproverComments', name: 'WS_TEMP_APPROVER_COMMENTS', className: "scrollable-content" },
            { data: 'EmailBackup', name: 'WS_FK_TB_EMAIL_BACKUP_ID', className: "scrollable-content" },
            {
                data: null,
                render: function (data, type, row) {

                    var buttons = '';

                    // Check permissions for buttons
                    if (userRoleId === 1) { // Viewer / Read-Only User
                        buttons += `
                            <button class="dt-button filterButton view-button" data-id="${row.WS_Id}" aria-label="View">
                                <i class="fa-solid fa-eye"></i>
                            </button>
                            <button class="dt-button filterButton edit-button" data-id="${row.WS_Id}" aria-label="Edit">
                                <i class="fas fa-edit"></i>
                            </button>
                            <button class="dt-button filterButton erase-button" data-id="${row.WS_Id}" aria-label="Delete">
                                <i class="fa-solid fa-eraser"></i>
                            </button>
                        `;

                    } else if (userRoleId === 2) { // Operations Lead User
                        buttons += `
                            <button class="dt-button filterButton view-button" data-id="${row.WS_Id}" aria-label="View">
                                <i class="fa-solid fa-eye"></i>
                            </button>
                            <button class="dt-button filterButton edit-button" data-id="${row.WS_Id}" aria-label="Edit">
                                <i class="fas fa-edit"></i>
                            </button>
                        `;

                    } else if   (userRoleId === 3) { // Viewer / Read-Only User
                        buttons += `
                            <button class="dt-button filterButton view-button" data-id="${row.WS_Id}" aria-label="View">
                                <i class="fa-solid fa-eye"></i>
                            </button>
                        `;
                    } else if (userRoleId === 4) {// Workspace Specialist User
                        buttons += `
                            <button class="dt-button filterButton view-button" data-id="${row.WS_Id}" aria-label="View">
                                <i class="fa-solid fa-eye"></i>
                            </button>
                            <button class="dt-button filterButton edit-button" data-id="${row.WS_Id}" aria-label="Edit">
                                <i class="fas fa-edit"></i>
                            </button>
                            <button class="dt-button filterButton erase-button" data-id="${row.WS_Id}" aria-label="Delete">
                                <i class="fa-solid fa-eraser"></i>
                            </button>
                        `;
                    } else if (userRoleId === 5) {// Workspace Specialist User
                        buttons += `
                            <button class="dt-button filterButton view-button" data-id="${row.WS_Id}" aria-label="View">
                                <i class="fa-solid fa-eye"></i>
                            </button>
                            <button class="dt-button filterButton edit-button" data-id="${row.WS_Id}" aria-label="Edit">
                                <i class="fas fa-edit"></i>
                            </button>
                        `;

                    }



                    return `<div class="button-container">${buttons}</div>`;
                }
            }
        ],

        buttons: [ // Add filter button
            {
                text: '<i class="fas fa-filter"></i>',
                className: 'filterButton',
                action: function (e, dt, node, config) {
                    $('#filtersModal').modal('show');
                }
            }
        ],
        columnDefs: [
            {
                targets: '_all', // Hide all columns to show only default columns later
                visible: false
            },
        ],
        order: [
            [0, 'desc'], //Sort ID by descending order

        ], // Set the default sort to ID by descending
        drawCallback: function () {
            wrapScrollableContent(); // Apply the wrapping during every draw
        },
    });

    table.columns(defaultColumns).visible(true); // Show the default columns set on defaultColumns variable

    $('a.toggle-vis').on('click', function (e) {
        e.preventDefault();

        // Get the column and toggle its visibility
        var column = table.column($(this).attr('data-column'));
        column.visible(!column.visible());
    });

    $('#showColumnsButton').click(function () {
        $('#filtersModal').modal('hide');
        showFilterModal();
    });

    $('#resetColumnsButton').click(function () {
        resetFilters();
    });
    //Funtion to handle View, edit and delete buttons
    $('#wsdataTable').on('click', '.view-button', function () {
        // Extract the ID from the clicked button
        var rowId = $(this).data('id');

        // Construct the redirect URL based on the ID
        var redirectUrl = 'Details/' + rowId; // Modify the URL structure as needed

        // Redirect to the constructed URL
        window.location.href = redirectUrl;
    });

    // Click event handler for the "Edit" button
    $('#wsdataTable').on('click', '.edit-button', function () {
        var rowId = $(this).data('id');
        var redirectUrl = 'Edit/' + rowId;
        window.location.href = redirectUrl;
    });

    // Click event handler for the "Erase" button
    $('#wsdataTable').on('click', '.erase-button', function () {
        var rowId = $(this).data('id');
        var confirmDelete = confirm("Are you sure you want to delete this Template?");
        if (confirmDelete) {
            $.ajax({
                type: 'POST',
                url: '/WSCrud/Delete/' + rowId,
                headers: {
                    'X-HTTP-Method-Override': 'DELETE'
                },
                success: function (data) {
                    if (data.success) {
                        $('#row_' + rowId).remove();
                        document.location.reload();
                    }
                }
            });
        }
    })
};

// Call the function to initialize the DataTable when the document is ready
$(document).ready(function () {
    DrawTable();
});