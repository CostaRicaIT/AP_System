

const selectAlias = document.getElementById("form-SelectAlias");// get a reference of the select 
const textareaInfoView = document.getElementById("historicRemitInfoView");// Add an event listener to the select
selectAlias.addEventListener("change", function () {
    // Get the selected value (ID)
    var selectedValue = selectAlias.value;
    $.ajax({
        url: '/Main/GetHistoricText', 
        type: 'GET',
        data: { historicId: selectedValue }, // Sends the selected ID to the controller
        success: function (data) {
            // Updates the content of the textarea with the historic
            textareaInfoView.value = data.historicText;
        },
        error: function () {
            // Handles any errors that may occur during the AJAX request
            alert("Error obtaining historical text.");
        }
    });
});

/*Listenner to get the info of the Alias*/

const selectAddAlias = document.getElementById("form-SelectInfoAlias");// get a reference of the select 
const InputInfo = document.getElementById("form-Alias");// Add an event listener to the select
selectAddAlias.addEventListener("change", function () {
    // Get the selected value (ID)
    var selectedValue = selectAddAlias.value;
    $.ajax({
        url: '/Main/GetAliasText',
        type: 'GET',
        data: { AliasId: selectedValue }, // Sends the selected ID to the controller
        success: function (data) {
            // Updates the content of the textarea with the historic
            InputInfo.value = data.AliasText;
        },
        error: function () {
            // Handles any errors that may occur during the AJAX request
            alert("Error al obtener el texto histórico.");
        }
    });
});
fillSelectInfoAlias();
recentDataHistoric();

function fillSelectInfoAlias() {
    $.ajax({

        url: '/Main/GetAliasInfo',
        type: 'GET',
        success: function (data) {
            if (data.success) {
                const select = document.getElementById("form-SelectInfoAlias");
                select.innerHTML = ""; // Clean the select

                // Fill the select
                data.selectItems.forEach(function (item) {
                    const option = document.createElement("option");
                    option.value = item.Value;
                    option.text = item.Text;
                    select.appendChild(option);
                });
            }
        },
        error: function () {
            alert("An error occurred while loading Alias info.");
        }
    });
}

function recentDataHistoric() {
    $.ajax({
        url: '/Main/GetHistoricRecentData',
        type: 'GET',
        success: function (data) {
            if (data.success) {
                const textarea = document.getElementById("form-HistRemitTo");
                textarea.value = data.mostRecentData;
            }
        },
        error: function () {
            alert("An error occurred while loading recent data.");
        }
    });
}

$(document).ready(function () {
    // Function to disable or enable the input field according to the status of the checkbox.
    function toggleInputState(checkbox, input) {
        if (checkbox.is(':checked')) {
            input.prop('disabled', true);
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
    $(".btn-submit").click(function () {

        // Get the current date
        var currentDate = new Date();

        
        var formattedDate = (currentDate.getMonth() + 1) + '/' + currentDate.getDate() + '/' + currentDate.getFullYear();

       
        var historic = $("#form-HistRemitTo").val();
        $.ajax({
            //Controller Name
            url: '/Main/CreateHistoric',
            type: 'POST',
            data: {
                //Send data to the server variables should match with DB
                HISTORIC_REMIT_DATE: formattedDate,
                HISTORIC_REMIT_INFO: historic
            },
            success: function (data) {
                if (data.success) {
                    /*Close the modal*/
                    const modalId = $(".submitHistoricRemit").attr("data-modal");
                    const modal = document.getElementById(modalId);
                    modal.style.display = "none";
                    location.reload();
                }
            },
            error: function () {
                alert("An error occurred while saving the record.");
            }
        }); 
 
       
    });
    
    $(".btn-submitAlias").click(function () {

        var Alias = $("#form-AddAlias").val();

        $.ajax({
            //Controller Name
            url: '/Main/CreateAlias',
            type: 'POST',
            data: {
                //Send data to the server variables should match with DB
                ALIAS_NAME: Alias
            },
            success: function (data) {
                if (data.success) {
                    /*Close the modal*/
                    const modalId = "AddAliasModal";
                    const modal = document.getElementById(modalId);
                    modal.style.display = "none";
                    location.reload();
                }
            },
            error: function () {
                alert("An error occurred while saving the record.");
            }
        });


    });    

    
   
    
});