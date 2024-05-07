$(document).ready(function () {
    // Show form when "Add new" button is clicked
    $(".btn-create").click(function () {
        $("#form-container").show();
    });
    // Hide form when "Cancel" button is clicked
    $("#form-cancel").click(function () {
        $("#form-container").hide();
    });
    // Submit form using AJAX when "Save" button is clicked
    $(".btn-submit").click(function (e) {
        e.preventDefault();
        var id = $("#form-id").val();
        var fullname = $("#form-name").val();
        var username = $("#form-username").val();
        var password = $("#form-password").val();
        var role = $("#form-role").val();
        $.ajax({
            url: '/User/Create',
            type: 'POST',
            data: {
                //Send data to the server variables should match with DB
                LOG_IN_ID: id,
                LOG_IN_FULL_NAME: fullname,
                LOG_IN_USER_NAME: username,
                LOG_IN_PASSWORD: password,
                FK_TB_LOGIN_ROLES_ID: role,
            },
            success: function (data) {
                if (data.success) {
                    location.reload();
                }
            },
            error: function () {
                alert("An error occurred while saving the record.");
            }
        });
    });


    //Populate form using the table data
    $(".btn-edit").click(function () {
        var usrId = $(this).data("log-id");
        $("#hidden-Login-id").val(usrId);
        var row = $(this).closest('tr');
        var fullname = row.find('td:nth-child(1)').text();
        var username = row.find('td:nth-child(2)').text();
        var password = row.find('td:nth-child(4)').text();


        $('#form-name').val(fullname);
        $('#form-username').val(username);
        $("#form-submit").hide();
        $("#form-update").show();
        $('#form-container').show();
    });

    //Update data

    $(".btn-update").click(function () {
        var usrId = $("#hidden-Login-id").val();
        var name = $("#form-name").val();
        var username = $("#form-username").val();
        var password = $("#form-password").val();
        var role = $("#form-role").val();
        //Calls Ajax and send petition to the server or controller
        $.ajax({
            type: 'POST',
            url: '/User/Edit',
            data: { LOG_IN_ID: usrId, LOG_IN_FULL_NAME: name, LOG_IN_USER_NAME: username, LOG_IN_PASSWORD: password, FK_TB_LOGIN_ROLES_ID: role },
            success: function (data) {
                if (data.success) {
                    location.reload();
                }
            },
            error: function () {
                alert('Error updating information, please check and try again');
            }
        });
    });;


    $('.btn-delete').click(function () {
        var id = $(this).attr('data-log-id');

        var confirmDelete = confirm("Are you sure you want to delete this item?");
        if (confirmDelete) {
            $.ajax({
                type: "POST",
                url: '/User/Delete/' + id,
                headers: {
                    // Due to security reasons a Delete call is not possible from modern browsers this header convert the POST request to DELETE to be able to comunicate throught the server
                    'X-HTTP-Method-Override': 'DELETE'
                },
                success: function (data) {
                    if (data.success) {
                        $('#row_' + id).remove();
                    }
                }
            });
        }
    });
});