BiblionetMainWindow.openDialog = function openDialog(dialogSelector, pageUrl, title, width, height) {

    console.log("BiblionetWindow");

    var $dialog = $(dialogSelector);

    // se il div non esiste, lo creo e lo aggiungo al body
    if ($dialog.length === 0) {
        $dialog = $('<div>', { id: dialogSelector.replace('#', '') });
        $('body').append($dialog);
    }

    // pulisco il contenuto
    $dialog.empty();

    // inserisco l'iframe
    $dialog.html('<iframe src="' + pageUrl + '" style="border:0;width:100%;height:100%;"></iframe>');

    // apro il dialog
    $dialog.dialog({
        modal: true,
        title: title,
        width: width,
        height: height,
        dialogClass: "dialog-open",
        buttons: {
            "Chiudi": function () {
                $(this).dialog("close");
            }
        },
        open: function () {
            // Aggiungo classi Bootstrap ai pulsanti quando il dialog si apre
            $(this).parent().find(".ui-dialog-buttonpane button").addClass("btn btn-dark");
        }
    });

    $dialog.parent().find(".ui-dialog-title").html(`
                  <div class="d-flex align-items-center gap-2">
                    <span class="material-symbols-outlined icon-circle d-flex aling-items-center" style="color:black;">book_5</span>
                    Aggiungi Libro
                  </div>
                `);
}



BiblionetMainWindow.ClosePopup = function () {
    const popup = document.getElementById("message-popup");
    popup.classList.remove("show");
    setTimeout(() => {
        popup.classList.add("hidden");
        document.getElementById("message-popup-title").innerText = "";
        document.getElementById("message-popup-text").innerText = "";
        document.getElementById("message-popup-icon").innerHTML = "";
    }, 300);
};


BiblionetMainWindow.showPopup = function (type, title, message) {
    const popup = document.getElementById("message-popup");
    const iconContainer = document.getElementById("message-popup-icon");

    // Imposta icona
    let iconHtml = "";
    if (type === "success") {
        iconHtml = '<img src="/Images/GIF/successo.gif"/>';
    } else if (type === "error") {
        iconHtml = '<img src="https://img.icons8.com/ios-filled/100/FF0000/cancel.png"/>';
    } else if (type === "info") {
        iconHtml = '<img src="https://img.icons8.com/ios-filled/100/0000FF/info.png"/>';
    }

    iconContainer.innerHTML = iconHtml;
    document.getElementById("message-popup-title").innerText = title;
    document.getElementById("message-popup-text").innerText = message;

    popup.classList.remove("hidden");
    setTimeout(() => popup.classList.add("show"), 10); // trigger animazione
};

BiblionetMainWindow.confirm_box = function (title, message, onConfirm) {
    const popup = document.getElementById("confirm-popup");

    document.getElementById("confirm-popup-title").innerText = title;
    document.getElementById("confirm-popup-text").innerText = message;

    popup.classList.remove("hidden");
    setTimeout(() => popup.classList.add("show"), 10); // trigger animazione

    document.getElementById("confirm-popup-cancel").onclick = function () {
        popup.classList.remove("show");
        setTimeout(() => popup.classList.add("hidden"), 300);
    };

    document.getElementById("confirm-popup-confirm").onclick = function () {
        popup.classList.remove("show");
        setTimeout(() => popup.classList.add("hidden"), 300);
        if (typeof onConfirm === "function") {
            onConfirm();
        }
    };
};


BiblionetMainWindow.show_success_message = function (message) {

    debugger;
    BiblionetMainWindow.showPopup("success", "Operazione completata", message);
};

BiblionetMainWindow.show_error_message = function (message) {
    BiblionetMainWindow.showPopup("error", "Errore", message);
};

BiblionetMainWindow.show_info_message = function (message) {
    BiblionetMainWindow.showPopup("info", "Informazione", message);
};

