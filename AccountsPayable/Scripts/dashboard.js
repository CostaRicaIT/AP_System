var table; // Declare the 'table' variable in a global scope

function initializeDataTable() {
    table = $('#dataTable').DataTable({
        autoWidth: true,
        stateSave: true,
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
                targets: 6, // Sixt column (0-based index)
                render: function (data, type, full, meta) {
                    // Define the custom buttons in the Actions column
                    return '<button class="dt-button filterButton"><i class="fa-solid fa-eye"></i></button>' +
                        '<button class="dt-button filterButton"><i class="fas fa-edit"></i></button>' +
                        '<button class="dt-button filterButton"><i class="fa-solid fa-eraser"></button>';
                }
            }
        ],
        dom: '<"top"lBf>rt<"bottom"ip>'


    });

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
    table.columns().visible(true);
    table.state.clear();
    $('#filtersModal').modal('hide');

}

// Call the function to initialize the DataTable when the document is ready
$(document).ready(function () {
    initializeDataTable();
});
