$(document).ready(function () {

    $("#map_idUsuario").change(function () {
        var idUsuario = parseInt($(this).val());
        var controller = getControllerName();
        var idMatricula = parseInt($("#map_idMatricula").val())

        //setea matriculas asociadas al usuario seleccionado en su correspondiente select
        $.ajax({
            url: "/" + controller + "/getSelectVehiculosUsuario",
            type: "POST",
            contentType: 'application/x-www-form-urlencoded; charset=utf-8',
            dataType: 'json',
            data: { idUsuario: idUsuario },
            success: function (data) {
                var options = $("#map_idMatricula");
                options.empty();
                $.each(data, function (key, value) {
                    options.append(new Option(value["mat"], value["id"]));
                });

                //llamar a getIDUV con el valor actual de Usuario y el último (o primero, ya veremos) valor de matrícula
                //obtiene dinámicamente el valor de IDUV en función del usuario y la matrícula seleccionada
                var matriculaAhora = parseInt($("#map_idMatricula").val());
                $.ajax({
                    url: "/" + controller + "/getIDUV",
                    type: "POST",
                    contentType: 'application/x-www-form-urlencoded; charset=utf-8',
                    dataType: 'json',
                    data: {
                        idUsuario: idUsuario,
                        idMatricula: matriculaAhora
                    },
                    success: function (data) {
                        if (data !== null) {
                            $("#diario_IDUV").val(data["IDUV"]);
                            $("#diario_KMINICIALES").val(data["KMFINALES"]);
                        } else {
                            $("#diario_IDUV").val("");
                            $("#diario_KMINICIALES").val("");
                        }
                        
                    },
                    error: function (xhr, status, error) {
                        console.log(xhr.responseText);
                        console.log("Se ha producido un error, por favor, contacte con el administrador.");
                    }
                });
            },
            error: function (xhr, status, error) {
                console.log(xhr.responseText);
                console.log("Se ha producido un error, por favor, contacte con el administrador.");
            }
        });
    });

    //obtiene dinámicamente el valor de IDUV y los km finales del último resultado en función del usuario y la matrícula seleccionada
    $("#map_idMatricula").change(function () {
        var idUsuario = parseInt($("#map_idUsuario").val())
        var controller = getControllerName();
        var idMatricula = parseInt($(this).val());

        $.ajax({
            url: "/" + controller + "/getIDUV",
            type: "POST",
            contentType: 'application/x-www-form-urlencoded; charset=utf-8',
            dataType: 'json',
            data: {
                idUsuario: idUsuario,
                idMatricula: idMatricula
            },
            success: function (data) {
                if (data !== null) {
                    $("#diario_IDUV").val(data["IDUV"]);
                    $("#diario_KMINICIALES").val(data["KMFINALES"]);
                } else {
                    $("#diario_IDUV").val("");
                    $("#diario_KMINICIALES").val("");
                }
            },
            error: function (xhr, status, error) {
                console.log(xhr.responseText);
                console.log("Se ha producido un error, por favor, contacte con el administrador.");
            }
        });
    });

});
