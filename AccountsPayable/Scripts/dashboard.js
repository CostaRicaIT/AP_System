var table; // Declare the 'table' variable in a global scope
var defaultColumns = [1, 3, 4, 5, 6, 24, 37]; //Alias,Remit to,Supplier Name,Supplier Number,Legal Entity,Tax ID,Actions

function initializeDataTable() {
    table = $('#dataTable').DataTable({
        pageLength: 25,
        orderCellsTop: true, //Show table sorting at top
        autoWidth: true, //Set the widht of collumn depending on content
        responsive: true, //Set the table to be responsive
        scrollCollapse: true, //Add scroll to table, scrollY and Scroll X are max size on each axis that table can have
        scrollY: '60vh',
        scrollX: '80vh',
        dom: '<"top"lBf>rt<"bottom"ip>', // set the order of table items to be drawn
        order: [
            [3, 'asc'], //Sort Tax ID by acsending order
            [0, 'desc'], //Sort ID by descending order
            
        ], // Set the default sort to ID by descending
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
                targets: '_all', //Hide all columns to show only default columns later
                visible: false
            },

        ],
        language: {
            search: "", //Search bar label
            searchPlaceholder: "Search... " // Search place holder
        },

    });
    table.columns(defaultColumns).visible(true); //Show the default columns set on defaultColumns variable
    $('a.toggle-vis').on('click', function (e) {// Function to show columns of table
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