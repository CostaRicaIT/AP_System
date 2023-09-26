// JavaScript for modal interactions
const openModalBtns = document.querySelectorAll(".openModalBtn");
const modals = document.querySelectorAll(".modal");

openModalBtns.forEach(function (button) {
    button.addEventListener("click", function () {
        const modalId = this.getAttribute("data-modal");
        const modal = document.getElementById(modalId);
        modal.style.display = "block";
    });
});

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
