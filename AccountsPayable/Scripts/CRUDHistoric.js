
// get a reference of the select and textarea
const selectAlias = document.getElementById("form-SelectAlias");
const textareaInfoView = document.getElementById("historicRemitInfoView");

// Add an event listener to the select
selectAlias.addEventListener("change", function () {
    // Get the selected value (ID)
    const selectedValue = selectAlias.value;
    $.ajax({
        url: '/Main/GetHistoricText', 
        type: 'GET',
        data: { historicId: selectedValue }, // Envía el ID seleccionado al controlador
        success: function (data) {
            // Actualiza el contenido del textarea con el texto histórico obtenido
            textareaInfoView.value = data.historicText;
        },
        error: function () {
            // Maneja cualquier error que pueda ocurrir durante la solicitud AJAX
            alert("Error al obtener el texto histórico.");
        }
    });
});

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
       
        var date = $("#historicRemitDate").val();
        var historic = $("#historicRemitInfoAdd").val();
        $.ajax({
            //Controller Name
            url: '/Main/CreateHistoric',
            type: 'POST',
            data: {
                //Send data to the server variables should match with DB
                HISTORIC_REMIT_DATE: date,
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
   
    
});