var table; // Declare the 'table' variable in a global scope
var defaultColumns = [6, 2, 3, 4, 9, 1, 22]; //Alias,Remit to,Supplier Name,Supplier Number,Legal Entity,Tax ID,Actions

function initializeDataTable() {

    table = $('#dataTable').DataTable({
        autoWidth: true,
        stateSave: true,
        responsive: true,
        dom: 'lBfrtip',
        buttons: [
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
                targets: '_all',
                visible: false
            },
            {
                targets: 22, // 22 column (0-based index)
                render: function (data, type, full, meta) {
                    var rowId = full[0]; // Change 0 to the appropriate column index
                    // Define the custom buttons in the Actions column
                    return '<button class="dt-button filterButton view-button" data-id="' + rowId + '"><i class="fa-solid fa-eye"></i></button>' +
                        '<button class="dt-button filterButton edit-button" data-id="' + rowId + '"><i class="fas fa-edit"></i></button>' +
                        '<button class="dt-button filterButton erase-button" data-id="' + rowId + '"><i class="fa-solid fa-eraser"></i></button>';
                }
            }            
        ],
        dom: '<"top"lBf>rt<"bottom"ip>'


    });
    table.columns(defaultColumns).visible(true);
    $('a.toggle-vis').on('click', function (e) {
        e.preventDefault();

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
        var redirectUrl = 'Delete/' + rowId;
        window.location.href = redirectUrl;
    });
}
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

// Call the function to initialize the DataTable when the document is ready
$(document).ready(function () {
    initializeDataTable();
});
