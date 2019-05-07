var dev = true;
//var dev = false;

function getControllerName() {
    var url = window.location.pathname.split("/");
    var salida = "";

    for (var i = 1; i < url.length - 1; i++) {
        salida += url[i] + "/";
    }
    salida = salida.substr(0, salida.length - 1);
    //console.log("getControllerName " + salida);
    return salida;    
}

function getActionName() {
    var url = window.location.pathname.split("/");
    return url[url.length - 1];
}

function getBase() {
    var url = window.location.pathname.split("/");
    salida = "/" + url[1];
    //console.log("getBase " + salida);
    if (dev) {
        salida = "";
    }
    return salida;
}


function resetForm(form) {
    formu = $('#' + form);
    formu.find('input:text, input:password, input:file, select, textarea').val('');
    formu.find('input:radio, input:checkbox')
           .removeAttr('checked').removeAttr('selected');
}

function resetFiltro(filtro) {
    formu = $('#' + filtro);
    formu.find('input:text, input:password, input:file, select, textarea').val('');
    formu.find('input:radio, input:checkbox')
           .removeAttr('checked').removeAttr('selected');
}

function redirect(input) {
    if (input === null) {
        input = "RedirectTo";
    }
    var url = $("#"+input).val();
    location.href = url;
}

function redirect() {
    var url = $("#RedirectTo").val();
    location.href = url;
}

function imprimir(cualquiera) {
    $('#area_imprimible').printArea();
}