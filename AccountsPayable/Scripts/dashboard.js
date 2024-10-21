var table; // Declare the 'table' variable in a global scope
var defaultColumns = [1, 3, 4, 5, 6, 24, 37]; //Alias,Remit to,Supplier Name,Supplier Number,Legal Entity,Tax ID,Actions

//function initializeDataTable() {
//    table = $('#dataTable').DataTable({
//        pageLength: 25,
//        orderCellsTop: true, //Show table sorting at top
//        autoWidth: true, //Set the widht of collumn depending on content
//        responsive: true, //Set the table to be responsive
//        scrollCollapse: true, //Add scroll to table, scrollY and Scroll X are max size on each axis that table can have
//        scrollY: '60vh',
//        scrollX: '80vh',
//        dom: '<"top"lBf>rt<"bottom"ip>', // set the order of table items to be drawn
//        order: [
//            [3, 'asc'], //Sort Tax ID by acsending order
//            [0, 'desc'], //Sort ID by descending order

//        ], // Set the default sort to ID by descending
//        buttons: [ // Add filter button
//            {
//                text: '<i class="fas fa-filter"></i>',
//                className: 'filterButton',
//                action: function (e, dt, node, config) {
//                    $('#filtersModal').modal('show');
//                }
//            }
//        ],
//        columnDefs: [
//            {
//                targets: '_all', //Hide all columns to show only default columns later
//                visible: false
//            },

//        ],
//        language: {
//            search: "", //Search bar label
//            searchPlaceholder: "Search... " // Search place holder
//        },

//    });
//    table.columns(defaultColumns).visible(true); //Show the default columns set on defaultColumns variable
//    $('a.toggle-vis').on('click', function (e) {// Function to show columns of table
//        e.preventDefault();

//        var column = table.column($(this).attr('data-column'));
//        column.visible(!column.visible());
//    });
//    $('#showColumnsButton').click(function () {
//        $('#filtersModal').modal('hide');
//        showFilterModal();
//    });
//    $('#resetColumnsButton').click(function () {
//        resetFilters();
//    });

//    //Funtion to handle View, edit and delete buttons
//    $('#dataTable').on('click', '.view-button', function () {
//        // Extract the ID from the clicked button
//        var rowId = $(this).data('id');

//        // Construct the redirect URL based on the ID
//        var redirectUrl = 'Details/' + rowId; // Modify the URL structure as needed

//        // Redirect to the constructed URL
//        window.location.href = redirectUrl;
//    });

//    // Click event handler for the "Edit" button
//    $('#dataTable').on('click', '.edit-button', function () {
//        var rowId = $(this).data('id');
//        var redirectUrl = 'Edit/' + rowId;
//        window.location.href = redirectUrl;
//    });

//    // Click event handler for the "Erase" button
//    $('#dataTable').on('click', '.erase-button', function () {
//        var rowId = $(this).data('id');
//        var confirmDelete = confirm("Are you sure you want to delete this Template?");
//        if (confirmDelete) {
//            $.ajax({
//                type: 'POST',
//                url: '/CRUD/Delete/' + rowId,
//                headers: {
//                    'X-HTTP-Method-Override': 'DELETE'
//                },
//                success: function (data) {
//                    if (data.success) {
//                        $('#row_' + rowId).remove();
//                        document.location.reload();
//                    }
//                }
//            });
//        }
//    });
//}
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
    table = $('#dataTable').DataTable({
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
            "url": "/Main/GetTemplateData",
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
            { data: 'Id', name: 'TEMP_ID' },
            { data: 'Alias', name: 'TB_ALIAS1.ALIAS_NAME', className:"scrollable-content" },
            { data: 'Folder', name: 'TEMP_FOLDER', className:"scrollable-content" },
            { data: 'TempTaxId', name: 'TEMP_TAX_ID', className:"scrollable-content" },
            { data: 'TempSupplierName', name: 'TEMP_SUPPLIER_NAME', className:"scrollable-content" },
            { data: 'TempSupplierNumber', name: 'TEMP_SUPPLIER_NUMBER', className:"scrollable-content" },
            { data: 'RemitTo', name: 'TEMP_REMIT_TO', className:"scrollable-content" },
            { data: 'SupplierSite', name: 'TEMP_SUPPLIER_SITE', className:"scrollable-content" },
            { data: 'HistoricRemitTo', name: 'TB_HISTORIC_REMIT1.HISTORIC_REMIT_INFO',className:"scrollable-content" },
            { data: 'VendorAccount', name: 'TEMP_VENDOR_ACCOUNT', className: "scrollable-content",className:"scrollable-content" },
            { data: 'Source', name: 'TB_ORACLE_SOURCE.ORACLE_SOURCE_DESCRIPTION',className:"scrollable-content" },
            { data: 'InvoiceFormat', name: 'TEMP_INVOICE_FORMAT',className:"scrollable-content" },
            { data: 'InvoiceType', name: 'TEMP_INVOICE_TYPE',className:"scrollable-content" },
            { data: 'InvoiceNotes', name: 'TEMP_INVOICE_NOTES',className:"scrollable-content" },
            { data: 'W9W8BENForm', name: 'TEMP_W9_W8',className:"scrollable-content" },
            { data: 'VSUForm', name: 'TEMP_VSU',className:"scrollable-content" },
            { data: 'PaymentMethod', name: 'TEMP_PAYMENT_METHOD',className:"scrollable-content" },
            { data: 'RemitToAccount', name: 'TEMP_REMIT_TOACCOUNT',className:"scrollable-content" },
            { data: 'PayTerms', name: 'TB_ORACLE_PAY_TERMS.PAY_TERMS_DESCRIPTION',className:"scrollable-content" },
            { data: 'InvoiceDescription', name: 'TEMP_INVOICE_DESCRIPTION',className:"scrollable-content" },
            { data: 'BillingPeriod', name: 'TEMP_BILLING_PERIOD',className:"scrollable-content" },
            { data: 'Dates', name: 'TEMP_BILLING_PRERIOD_DATE',className:"scrollable-content" },
            { data: 'DistributionSet', name: 'TEMP_DISTRIBUTION_SET',className:"scrollable-content" },
            { data: 'DistributionCombination', name: 'TEMP_DISTRIBUTION_COMBINATION',className:"scrollable-content" },
            { data: 'AccountingDate', name: 'TEMP_ACCOUNTING_DATE' },
            { data: 'LegalEntity', name: 'TB_ORACLE_LEGAL_ENTITIES.LEGAL_ENTITY_NAME',className:"scrollable-content" },
            { data: 'OrganizationType', name: 'TB_ORACLE_ORGANIZATION_TYPE.ORGANIZATION_TYPE_NAME',className:"scrollable-content" },
            { data: 'TaxPayerID', name: 'TEMP_TAXPAYER_ID',className:"scrollable-content" },
            { data: 'Type', name: 'TB_ORACLE_TYPE.ORACLE_TYPE_NAME',className:"scrollable-content" },
            { data: 'Description', name: 'TEMP_ORACLE_DESCRIPTION',className:"scrollable-content" },
            { data: 'OracleNotes', name: 'TEMP_ORACLE_NOTES',className:"scrollable-content" },
            { data: 'OracleInstructions', name: 'TEMP_ORACLE_INSTRUCTIONS',className:"scrollable-content" },
            { data: 'ARKeyContactsCurrent', name: 'CONTACTS_CURRENT',className:"scrollable-content" },
            { data: 'ARKeyContactsPrior', name: 'CONTACTS_PRIOR',className:"scrollable-content" },
            { data: 'Approver', name: 'TB_APPROVER.APPROVER_NAME',className:"scrollable-content" },
            { data: 'ApproverComments', name: 'TEMP_APPROVER_COMMENTS',className:"scrollable-content" },
            { data: 'EmailBackup', name: 'TB_EMAIL_BACKUP1.EMAIL_BACKUP',className:"scrollable-content" },
            {
                data: null,
                render: function (data, type, row) {

                    var buttons = '';

                    // Check permissions for buttons
                    if (userRoleId === 3) { // View permission
                        buttons += `
                            <button class="dt-button filterButton view-button" data-id="${row.Id}" aria-label="View">
                                <i class="fa-solid fa-eye"></i>
                            </button>
                        `;
                    } else if (userRoleId === 2) {
                        buttons += `
                            <button class="dt-button filterButton view-button" data-id="${row.Id}" aria-label="View">
                                <i class="fa-solid fa-eye"></i>
                            </button>
                            <button class="dt-button filterButton edit-button" data-id="${row.Id}" aria-label="Edit">
                                <i class="fas fa-edit"></i>
                            </button>
                            <button class="dt-button filterButton erase-button" data-id="${row.Id}" aria-label="Delete">
                                <i class="fa-solid fa-eraser"></i>
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
    $('#dataTable').on('click', '.view-button', function () {
        // Extract the ID from the clicked button
        var rowId = $(this).data('id');

        // Construct the redirect URL based on the ID
        var redirectUrl = 'Details/' + rowId; // Modify the URL structure as needed

        // Redirect to the constructed URL
        window.location.href = redirectUrl;
    });

    // Click event handler for the "Edit" button
    $('#dataTable').on('click', '.edit-button', function () {
        var rowId = $(this).data('id');
        var redirectUrl = 'Edit/' + rowId;
        window.location.href = redirectUrl;
    });

    // Click event handler for the "Erase" button
    $('#dataTable').on('click', '.erase-button', function () {
        var rowId = $(this).data('id');
        var confirmDelete = confirm("Are you sure you want to delete this Template?");
        if (confirmDelete) {
            $.ajax({
                type: 'POST',
                url: '/CRUD/Delete/' + rowId,
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
    //initializeDataTable();
    //// Set a timeout to hide the spinner after a certain period
    //setTimeout(function () {
    //    $('.spinner').addClass('hidden');
    //}, 1000); // Delay
});