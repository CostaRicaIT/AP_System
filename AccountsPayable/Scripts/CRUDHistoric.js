const selectAlias = document.getElementById("form-SelectHistoricRemit");// get a reference of the select 
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

/*Listenner to get the info of the Highlights*/

const selectHighlightsHistoric = document.getElementById("form-SelectHighlightsHistoric");// get a reference of the select 
selectHighlightsHistoric.addEventListener("change", function () {
    // Get the selected value (ID)
    var selectedValue = selectHighlightsHistoric.value;
    $.ajax({
        url: '/Main/GetHighLightsText',
        type: 'GET',
        data: { HighlightsID: selectedValue }, // Sends the selected ID to the controller
        success: function (data) {
            // Updates the content of the textarea with the historic
            var highlight = data;
            document.getElementById("HighlightsHistoryView").value = highlight.HIGLIGTHS;
            document.getElementById("HighlightsCHistoryView").value = highlight.HIGLIGTHS_COMMENTS;
            document.getElementById("InstructionsHistoryView").value = highlight.HIGLIGTHS_INSTRUCTIONS;
            document.getElementById("ExceptionsHistoryView").value = highlight.HIGLIGTHS_EXCEPTIONS;
            document.getElementById("MostCIHistoryView").value = highlight.HIGLIGTHS_COMMON_ISSUES;
            document.getElementById("SupplierAHistoryView").value = highlight.HIGLIGTHS_SUPPLIER_AGENCY;
            document.getElementById("TemplateCHistoryView").value = highlight.HIGLIGTHS_TEMPLATE_COMMENTS;

        },
        error: function () {
            // Handles any errors that may occur during the AJAX request
            alert("Error al obtener el texto¿.");
        }
    });
});

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

function GetOracleLegalEntity() {
    // Use AJAX to fetch data and populate the dropdown
    $.ajax({
        url: '/Main/GetOracleLegalEntity', // Replace with the actual URL to fetch data
        method: 'GET',
        success: function (data) {
            // Assuming data is an array of items with 'Text' and 'Value' properties
            var dropdown = $('#form-OracleLegalE');

            // Iterate through the data and append options to the dropdown
            $.each(data, function (index, item) {
                dropdown.append($('<option>', {
                    value: item.Value,
                    text: item.Text
                }));
            });
        },
        error: function (error) {
            console.log("Error fetching data: " + error);
        }
    });
}
function GetOracleType() {
    // Use AJAX to fetch data and populate the dropdown
    $.ajax({
        url: '/Main/GetOracleType', // Replace with the actual URL to fetch data
        method: 'GET',
        success: function (data) {
            // Assuming data is an array of items with 'Text' and 'Value' properties
            var dropdown = $('#form-OracleType');

            // Iterate through the data and append options to the dropdown
            $.each(data, function (index, item) {
                dropdown.append($('<option>', {
                    value: item.Value,
                    text: item.Text
                }));
            });
        },
        error: function (error) {
            console.log("Error fetching data: " + error);
        }
    });
}
function GetOraclePayTerms() {
    // Use AJAX to fetch data and populate the dropdown
    $.ajax({
        url: '/Main/GetOraclePayTerms', // Replace with the actual URL to fetch data
        method: 'GET',
        success: function (data) {
            // Assuming data is an array of items with 'Text' and 'Value' properties
            var dropdown = $('#form-OraclePayTerms');

            // Iterate through the data and append options to the dropdown
            $.each(data, function (index, item) {
                dropdown.append($('<option>', {
                    value: item.Value,
                    text: item.Text
                }));
            });
        },
        error: function (error) {
            console.log("Error fetching data: " + error);
        }
    });
}
function GetOracleSource() {
    // Use AJAX to fetch data and populate the dropdown
    $.ajax({
        url: '/Main/GetOracleSource', // Replace with the actual URL to fetch data
        method: 'GET',
        success: function (data) {
            // Assuming data is an array of items with 'Text' and 'Value' properties
            var dropdown = $('#form-OracleSource');

            // Iterate through the data and append options to the dropdown
            $.each(data, function (index, item) {
                dropdown.append($('<option>', {
                    value: item.Value,
                    text: item.Text
                }));
            });
        },
        error: function (error) {
            console.log("Error fetching data: " + error);
        }
    });
}
function GetApprover() {
    // Use AJAX to fetch data and populate the dropdown
    $.ajax({
        url: '/Main/GetApprover', // Replace with the actual URL to fetch data
        method: 'GET',
        success: function (data) {
            // Assuming data is an array of items with 'Text' and 'Value' properties
            var dropdown = $('#form-SelectApprover');

            // Iterate through the data and append options to the dropdown
            $.each(data, function (index, item) {
                dropdown.append($('<option>', {
                    value: item.Value,
                    text: item.Text
                }));
            });
        },
        error: function (error) {
            console.log("Error fetching data: " + error);
        }
    });
}

function GetBackUpEmails() {
    // Use AJAX to fetch data and populate the dropdown
    $.ajax({
        url: '/Main/GetBackUpEmails', // Replace with the actual URL to fetch data
        method: 'GET',
        success: function (data) {
            // Assuming data is an array of items with 'Text' and 'Value' properties
            var dropdown = $('#form-SelectMails');

            // Iterate through the data and append options to the dropdown
            $.each(data, function (index, item) {
                dropdown.append($('<option>', {
                    value: item.Value,
                    text: item.Text
                }));
            });
        },
        error: function (error) {
            console.log("Error fetching data: " + error);
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

        // Get Hours, Minutes, Seconds



        var hours = currentDate.getHours().toString().padStart(2, '0');
        var minutes = currentDate.getMinutes().toString().padStart(2, '0');
        var seconds = currentDate.getSeconds().toString().padStart(2, '0');

        // Concats the parts of the hours
        formattedDate += ' ' + hours + ':' + minutes + ':' + seconds;
        var historic = $("#form-HistRemitTo").val();
        var confirmCreate = confirm("Are you sure you want to create this historic remit ?");

        if (confirmCreate) {
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
                        alert("Historic created successfully!");
                        location.reload();
                    }
                },
                error: function () {
                    alert("An error occurred while saving the record.");
                }
            });
        }

    });

    $(".btn-submitAlias").click(function () {

        var Alias = $("#form-AddAlias").val();
        var confirmCreate = confirm("Are you sure you want to create this Alias ?");
        if (confirmCreate) {
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
                        alert("Alias created successfully!");
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
        }

    });
    GetOracleLegalEntity();
    GetOracleType();
    GetOraclePayTerms();
    GetOracleSource();
    GetApprover();
    GetBackUpEmails();
});