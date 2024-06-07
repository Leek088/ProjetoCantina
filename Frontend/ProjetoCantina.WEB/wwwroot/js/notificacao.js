"use strict";
const xhttp = new XMLHttpRequest();
var connection = new signalR.HubConnectionBuilder().withUrl("/notificacaoHub").build();
document.getElementById("buttonCadastrar").disabled = true;
connection.on("ReceiveMessage", function (result) {
    $("#messagesList").html(result);
});
function LimparNotificacoes() {
    $("#messagesList").html("");
}
connection.start().then(function () {
    document.getElementById("buttonCadastrar").disabled = false;
}).catch(function (err) {
    return console.error(err.toString());
});
xhttp.onreadystatechange = function () {
    if (xhttp.readyState === XMLHttpRequest.DONE) {
        if (xhttp.status == 201) {
            LimparForm();
        }
        document.getElementById("buttonCadastrar").disabled = false;
    }
};
function LimparForm() {
    $('form').each(function () {
        this.reset();
    });
}
document.getElementById("buttonCadastrar").addEventListener("click", function (event) {
    event.preventDefault();
    LimparNotificacoes();
    $("#messagesList").html("Aguarde...");
    document.getElementById("buttonCadastrar").disabled = true;
    var urlPost = $('form').attr('action');
    xhttp.open('POST', urlPost, true);
    var formData = new FormData($('form')[0]);
    xhttp.send(formData);
});