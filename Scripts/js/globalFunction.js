function openDialog(dialogSelector, pageUrl, title, width, height) {

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
