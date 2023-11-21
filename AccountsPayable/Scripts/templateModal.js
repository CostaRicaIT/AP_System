// JavaScript for modal interactions
const openModalBtns = document.querySelectorAll(".openModalBtn");
const modals = document.querySelectorAll(".modal");

openModalBtns.forEach(function (button) {
    button.addEventListener("click", function () {
        var modalId = this.getAttribute("data-modal");
        var modal = document.getElementById(modalId);
        var form = document.getElementById('form');

        if (modalId == 'ShowSaveModal') {
            if (checkFormValidity()) {
                modal.style.display = "block";
            }
        } else {
            modal.style.display = "block";
        }



        /* Get the cancel button inside the modal */
        var cancelButton = modal.querySelector("#cancelButton");

        /* Close the modal when the user clicks on the cancel button */
        cancelButton.addEventListener("click", function () {
            modal.style.display = "none";
        });
    });
});
modals.forEach(function (modal) {
    const closeModalBtn = modal.querySelector(".close");
    closeModalBtn.addEventListener("click", function () {
        modal.style.display = "none";
    });
});
