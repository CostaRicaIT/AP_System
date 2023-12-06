var table; // Declare the 'table' variable in a global scope
var defaultColumns = [1, 3, 4, 5, 9, 2, 23]; //Alias,Remit to,Supplier Name,Supplier Number,Legal Entity,Tax ID,Actions

function initializeDataTable() {
    table = $('#dataTable').DataTable({
        orderCellsTop: true,
        autoWidth: true,
        stateSave: false,
        responsive: true,
        scrollCollapse: true,
        scrollY: '60vh',
        scrollX: '80vh',
        dom: '<"top"lBf>rt<"bottom"ip>',
        order: [[0, 'desc']],
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
                targets: 23,
                render: function (data, type, full, meta) {
                    var rowId = full[0];
                    return '<button class="dt-button filterButton view-button" data-id="' + rowId + '"><i class="fa-solid fa-eye"></i></button>' +
                        '<button class="dt-button filterButton edit-button" data-id="' + rowId + '"><i class="fas fa-edit"></i></button>' +
                        '<button class="dt-button filterButton erase-button" data-id="' + rowId + '"><i class="fa-solid fa-eraser"></i></button>';
                }
            }
        ],
        language: {
            search:"",
            searchPlaceholder: "Search... "
        },
        //initComplete: function () {
        //    this.api().columns().every(function () {
        //        var column = this;
        //        var title = column.footer().textContent;

        //        // Create input element and add event listener
        //        var inputElement = $('<input type="text" placeholder="Search ' + title + '" />')
        //            .appendTo($(column.footer()).empty())
        //            .on('keyup change clear', function () {
        //                if (column.search() !== this.value) {
        //                    column.search(this.value).draw();
        //                }
        //            });

        //        // Hide search input for actions column (index 23)
        //        if (column.index() === 23) {
        //            inputElement.hide();
        //        }
        //    });

        //},



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
        var confirmDelete = confirm("Are you sure you want to delete this Template?");
        if (confirmDelete) {
            $.ajax({
                type: 'POST',
                url: '/Main/Delete/' + rowId,
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

    //window.onscroll = function() {
    //    scrollFunction();
    //    };

    //function scrollFunction() {
    //        var scrollToTopButton = document.getElementById("scrollToTop");

    //        // Show or hide the button based on scroll position
    //        if (document.body.scrollTop > document.body.scrollHeight / 2 || document.documentElement.scrollTop > document.documentElement.scrollHeight / 2) {
    //    scrollToTopButton.style.display = "block";
    //        } else {
    //    scrollToTopButton.style.display = "none";
    //        }
    //    }

    //function scrollToTop() {
    //    document.body.scrollTop = 0;
    //document.documentElement.scrollTop = 0;
    //    }