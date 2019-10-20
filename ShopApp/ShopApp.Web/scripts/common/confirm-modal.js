function openConfirmationModal(confirmCallback) {
    var body = "<p>Are you sure that u want to continue?</p>";

    var buttons = [
        {
            Name: "Confirm",
            onClick: confirmCallback
        }
    ];

    openModal("Confirm", body, buttons);
}