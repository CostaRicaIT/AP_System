// JavaScript for modal interactions
const openModalBtns = document.querySelectorAll(".openModalBtn");
const modals = document.querySelectorAll(".modal");

openModalBtns.forEach(function (button) {
    button.addEventListener("click", function () {
        var modalId = this.getAttribute("data-modal");
        var modal = document.getElementById(modalId);
        modal.style.display = "block";
       /* If the "ShowHistoricRemitModal" modal is opened, fill in the select*/
        if (modalId === "ShowHistoricRemitModal") {
            fillSelectHistoric();
        }
        if (modalId === "ShowHighlights") {
            fillSelectHighligtsHistoric();
        }
        /* Get the cancel button inside the modal */
        var cancelButton = modal.querySelector("#cancelAlias");

        /* Close the modal when the user clicks on the cancel button */
        cancelButton.addEventListener("click", function () {
            modal.style.display = "none";
        });
    });
});





function fillSelectHistoric() {
    $.ajax({
        
        url: '/Main/GetHistoryInfo',
        type: 'GET',
        success: function (data) {
            if (data.success) {
                const select = document.getElementById("form-SelectHistoricRemit");
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
            alert("An error occurred while loading Historic Remits.");
        }
    });
}

function fillSelectHighligtsHistoric() {
    $.ajax({

        url: '/Main/GetHighlightsInfo',
        type: 'GET',
        success: function (data) {
            if (data.success) {
                const select = document.getElementById("form-SelectHighlightsHistoric");
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
            alert("An error occurred while loading Historic Remits.");
        }
    });
}

modals.forEach(function (modal) {
    const closeModalBtn = modal.querySelector(".close");
    closeModalBtn.addEventListener("click", function () {
        modal.style.display = "none";
    });

    window.addEventListener("click", function (event) {
        if (event.target === modal) {
            modal.style.display = "none";
        }
    });
});

// Handle form submission for each modal
const forms = document.querySelectorAll("form");

forms.forEach(function (form) {
    form.addEventListener("submit", function (event) {
        event.preventDefault();
        // Add more logic here to handle the form submiSssion, e.g., send data to a server.
        form.reset(); // Reset the form
        const modalId = this.querySelector("button[type='button']").getAttribute("data-modal");
        const modal = document.getElementById(modalId);
        modal.style.display = "none"; // Close the associated modal after submission
    });
});
