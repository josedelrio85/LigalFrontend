var todoCorrecto = 0;

$(document).ready(function () {

    //SECCION BEFORE-EDIT

    //prefijo formulario
    var preid = "inspeccion_";
    var prename = "inspeccion.";


    //var dev = true;
    var dev = false;
    
    var selectResultadoCharm = $("#map_IdResultadoCharm");
    var selectResultadoQuino = $("#map_IdResultadoQuino");
    var situacion = $("#" + preid + "Situacion");
    var resultado = $("#" + preid + "Resultado").val();

    $("#map_SITUA1").prop('checked', false);
    $("#map_SITUA2").prop('checked', false);
    $("#map_SITUA3").prop('checked', false);
    $("#map_SITUA4").prop('checked', false);

    //Marca los checks vinculados a Situación en función del valor de este campo
    switch (situacion.val()) {
        case "0":
            $("#map_SITUA1").prop('checked', true);
            break;
        case "1":
            $("#map_SITUA2").prop('checked', true);
            break;
        case "2":
            $("#map_SITUA3").prop('checked', true);
            break;
        case "3":
            $("#map_SITUA4").prop('checked', true);
            break;
        default:
            break;
    }


    //Funcionalidad botones vinculados a Situción:
    //Desmarca los otros botones y asigna el valor al campo situación oculto
    $("[id^=map_SITUA]").on("click", function () {
        var este = $(this).attr("name");
        $("[id^=map_SITUA]").each(function () {
            if (este !== $(this).attr("name")) {
                $(this).prop("checked", false);
            }
        });
        var valor = este.split("map_SITUA").pop();
        situacion.val(valor - 1);
    });


    //Setea los select charm y quino en función del valor del campo resultado
    //if (resultado.indexOf("+QUINO") < 0) {
    //    switch (resultado) {
    //        case "OTROS":
    //            selectResultadoCharm.val(1);
    //            selectResultadoQuino.val("");
    //            break;
    //        case "QUINOLONAS":
    //            selectResultadoCharm.val(1);
    //            break;
    //        default:
    //            selectResultadoQuino.val("");
    //            break;
    //    }
    //}

    if ((resultado.indexOf("+QUINO") < 0) && resultado !== "QUINOLONAS" && resultado !== "OTROS") {
        $("#selectResultadoQuino").hide();
        selectResultadoQuino.val("");
    }


    switch ($("#" + preid + "MAMITESN").val()) {
        case "0":
            $("#map_MAMITES").prop('checked', false);
            $("#map_MAMITEN").prop('checked', false);
            break;
        case "1":
            $("#map_MAMITES").prop('checked', true);
            $("#map_MAMITEN").prop('checked', false);
            break;
        case "2":
            $("#map_MAMITES").prop('checked', false);
            $("#map_MAMITEN").prop('checked', true);
            break;
    }

    switch ($("#" + preid + "OUTRASSN").val()) {
        case "0":
            $("#map_OUTRASS").prop('checked', false);
            $("#map_OUTRASN").prop('checked', false);
            break;
        case "1":
            $("#map_OUTRASS").prop('checked', true);
            $("#map_OUTRASN").prop('checked', false);
            break;
        case "2":
            $("#map_OUTRASS").prop('checked', false);
            $("#map_OUTRASN").prop('checked', true);
            break;
    }

    switch ($("#" + preid + "PARTOSN").val()) {
        case "0":
            $("#map_PARTOS").prop('checked', false);
            $("#map_PARTON").prop('checked', false);
            break;
        case "1":
            $("#map_PARTOS").prop('checked', true);
            $("#map_PARTON").prop('checked', false);
            break;
        case "2":
            $("#map_PARTOS").prop('checked', false);
            $("#map_PARTON").prop('checked', true);
            break;
    }

    switch ($("#" + preid + "ASOCIACIONSN").val()) {
        case "0":
            $("#map_ASOCIACIONS").prop('checked', false);
            $("#map_ASOCIACIONN").prop('checked', false);
            break;
        case "1":
            $("#map_ASOCIACIONS").prop('checked', true);
            $("#map_ASOCIACIONN").prop('checked', false);
            break;
        case "2":
            $("#map_ASOCIACIONS").prop('checked', false);
            $("#map_ASOCIACIONN").prop('checked', true);
            break;
    }

    switch ($("#" + preid + "ALGUNTRATA").val()) {
        case "0":
            $("#map_ALGUNTRATAS").prop('checked', false);
            $("#map_ALGUNTRATAN").prop('checked', false);
            break;
        case "1":
            $("#map_ALGUNTRATAS").prop('checked', true);
            $("#map_ALGUNTRATAN").prop('checked', false);
            break;
        case "2":
            $("#map_ALGUNTRATAS").prop('checked', false);
            $("#map_ALGUNTRATAN").prop('checked', true);
            break;
    }

    
    //APAÑO ResultadoCHARM y ResultadoQUINO
    selectResultadoCharm.change(function () {
        var textoSelected = $('option:selected', this).text();
        var hidden = $("#" + preid + "ResultadoCHARM");
        hidden.val(textoSelected);
    });

    selectResultadoQuino.change(function () {
        var textoSelected = $('option:selected', this).text();
        var hidden = $("#" + preid + "RESULTADOQUINO");
        hidden.val(textoSelected);
    });



    //OnChange
    $("#" + preid + "Destrucion").on("change", function () {
        if ($("#" + preid + "Destrucion").is(':checked')) {
            $("#map_SITUA1").prop('checked', true);
            $("#map_SITUA2").prop('checked', false);
            $("#map_SITUA3").prop('checked', false);
            $("#map_SITUA4").prop('checked', false);
            $("#" + preid + "NONDESTRUCION").prop('checked', false);
        }
    });

    $("#" + preid + "NONCAUSA").on("change", function () {
        if ($("#" + preid + "NONCAUSA").is(':checked')) {
            $("#" + preid + "MAMITE").prop('checked', false);
            $("#map_MAMITES").prop('checked', false);
            $("#map_MAMITEN").prop('checked', false);
            $("#" + preid + "OUTRAS").prop('checked', false);
            $("#map_OUTRASS").prop('checked', false);
            $("#map_OUTRASN").prop('checked', false);
            $("#" + preid + "PARTO").prop('checked', false);
            $("#map_PARTOS").prop('checked', false);
            $("#map_PARTON").prop('checked', false);
            $("#" + preid + "ASOCIACION").prop('checked', false);
            $("#map_ASOCIACIONS").prop('checked', false);
            $("#map_ASOCIACIONN").prop('checked', false);
            $("#" + preid + "EMPDOSIS").prop('checked', false);
            $("#" + preid + "TRATAINTRAM").prop('checked', false);
            $("#" + preid + "ORDEMUXI").prop('checked', false);
            $("#" + preid + "MUXIERRO").prop('checked', false);
            $("#" + preid + "INCORPORACION").prop('checked', false);
            $("#" + preid + "NOIDENTIF").prop('checked', false);
            $("#" + preid + "TRATANIERR").prop('checked', false);
            $("#" + preid + "ERRCOMUNI").prop('checked', false);
        }
    });

    $("#map_MAMITES").on("change", function () {
        if ($("#map_MAMITES").is(':checked')) {
            $("#map_MAMITEN").prop('checked', false);
            $("#" + preid + "MAMITESN").val(1);
        } else {
            $("#" + preid + "MAMITESN").val(0);
        }
    });
    $("#map_MAMITEN").on("change", function () {
        if ($("#map_MAMITEN").is(':checked')) {
            $("#map_MAMITES").prop('checked', false);
            $("#" + preid + "MAMITESN").val(2);
        } else {
            $("#" + preid + "MAMITESN").val(0);
        }
    });

    $("#map_OUTRASS").on("change", function () {
        if ($("#map_OUTRASS").is(':checked')) {
            $("#map_OUTRASN").prop('checked', false);
            $("#" + preid + "OUTRASSN").val(1);
        } else {
            $("#" + preid + "OUTRASSN").val(0);
        }
    });
    $("#map_OUTRASN").on("change", function () {
        if ($("#map_OUTRASN").is(':checked')) {
            $("#map_OUTRASS").prop('checked', false);
            $("#" + preid + "OUTRASSN").val(2);
        } else {
            $("#" + preid + "OUTRASSN").val(0);
        }
    });

    $("#map_PARTOS").on("change", function () {
        if ($("#map_PARTOS").is(':checked')) {
            $("#map_PARTON").prop('checked', false);
            $("#" + preid + "PARTOSN").val(1);
        } else {
            $("#" + preid + "PARTOSN").val(0);
        }
    });
    $("#map_PARTON").on("change", function () {
        if ($("#map_PARTON").is(':checked')) {
            $("#map_PARTOS").prop('checked', false);
            $("#" + preid + "PARTOSN").val(2);
        } else {
            $("#" + preid + "PARTOSN").val(0);
        }
    });

    $("#map_ASOCIACIONS").on("change", function () {
        if ($("#map_ASOCIACIONS").is(':checked')) {
            $("#map_ASOCIACIONN").prop('checked', false);
            $("#" + preid + "ASOCIACIONSN").val(1);
        } else {
            $("#" + preid + "ASOCIACIONSN").val(0);
        }
    });
    $("#map_ASOCIACIONN").on("change", function () {
        if ($("#map_ASOCIACIONN").is(':checked')) {
            $("#map_ASOCIACIONS").prop('checked', false);
            $("#" + preid + "ASOCIACIONSN").val(2);
        } else {
            $("#" + preid + "ASOCIACIONSN").val(0);
        }
    });

    $("#map_ALGUNTRATAS").on("change", function () {
        if ($("#map_ALGUNTRATAS").is(':checked')) {
            $("#map_ALGUNTRATAN").prop('checked', false);
            $("#" + preid + "ALGUNTRATA").val(1);
        } else {
            $("#" + preid + "ALGUNTRATA").val(0);
        }
    });
    $("#map_ALGUNTRATAN").on("change", function () {
        if ($("#map_ALGUNTRATAN").is(':checked')) {
            $("#map_ALGUNTRATAS").prop('checked', false);
            $("#" + preid + "ALGUNTRATA").val(2);
        } else {
            $("#" + preid + "ALGUNTRATA").val(0);
        }
    });

    $("#" + preid + "NONDESTRUCION").on("change", function () {
        if ($("#" + preid + "NONDESTRUCION").is(':checked')) {
            $("#" + preid + "Destrucion").prop('checked', false);
        }
    });


    selectResultadoCharm.on("change", function () {

        var optionSelectResultadoCharm = $('#map_IdResultadoCharm').find(":selected");
        var optionSelectResultadoQuino = $('#map_IdResultadoQuino').find(":selected");

        if (resultado !== "OTROS" && resultado !== "QUINOLONAS" && resultado.indexOf("+QUINO") < 0 && $("#" + preid + "ResultadoCHARM").val() === "NEGATIVO") {
            $("#map_SITUA1").prop('checked', false);
            $("#map_SITUA2").prop('checked', true);
            $("#map_SITUA3").prop('checked', false);
            $("#map_SITUA4").prop('checked', false);
            situacion.val(1);
        }

        if (resultado.indexOf("+QUINO") < 0) {

            if (optionSelectResultadoCharm.text() === "NEGATIVO" && optionSelectResultadoQuino.text() === " ") {//
                situacion.val(1)
                $("#map_SITUA2").prop('checked', true);
                $("#map_SITUA1").prop('checked', false);
                $("#map_SITUA3").prop('checked', false);
                $("#map_SITUA4").prop('checked', false);
            }

            if (optionSelectResultadoCharm.text() === " " && optionSelectResultadoQuino.text() === "NEGATIVO") {
                situacion.val(1)
                $("#map_SITUA2").prop('checked', true);
                $("#map_SITUA1").prop('checked', false);
                $("#map_SITUA3").prop('checked', false);
                $("#map_SITUA4").prop('checked', false);
            }

            if (optionSelectResultadoCharm.text() === "NEGATIVO" && optionSelectResultadoQuino.text() === "NEGATIVO") {
                situacion.val(1)
                $("#map_SITUA2").prop('checked', true);
                $("#map_SITUA1").prop('checked', false);
                $("#map_SITUA3").prop('checked', false);
                $("#map_SITUA4").prop('checked', false);
            }
        }
    });

    selectResultadoQuino.on("change", function () {
        
        var optionSelectResultadoCharm = $('#map_IdResultadoCharm').find(":selected");
        var optionSelectResultadoQuino = $('#map_IdResultadoQuino').find(":selected");

        if (resultado === "QUINOLONAS") {
            if (optionSelectResultadoQuino.text() === "NEGATIVO") {
                $("#map_SITUA1").prop('checked', false);
                $("#map_SITUA2").prop('checked', true);
                $("#map_SITUA3").prop('checked', false);
                $("#map_SITUA4").prop('checked', false);
                situacion.val(1);
            }

            if (optionSelectResultadoQuino.text() === "POSITIVO") {
                $("#map_SITUA1").prop('checked', false);
                $("#map_SITUA2").prop('checked', false);
                $("#map_SITUA3").prop('checked', true);
                $("#map_SITUA4").prop('checked', false);
                situacion.val(2);
            }
        }

        if (resultado.indexOf("+QUINO") >= 0) {
            if (optionSelectResultadoCharm.text() === "NEGATIVO" && optionSelectResultadoQuino.text() === " ") {
                $("#map_SITUA1").prop('checked', false);
                $("#map_SITUA2").prop('checked', true);
                $("#map_SITUA3").prop('checked', false);
                $("#map_SITUA4").prop('checked', false);
                situacion.val(1);
            }

            if (optionSelectResultadoCharm.text() === " " && optionSelectResultadoQuino.text() === "NEGATIVO") {
                $("#map_SITUA1").prop('checked', false);
                $("#map_SITUA2").prop('checked', true);
                $("#map_SITUA3").prop('checked', false);
                $("#map_SITUA4").prop('checked', false);
                situacion.val(1);
            }

            if (optionSelectResultadoCharm.text() === "NEGATIVO" && optionSelectResultadoQuino.text() === "NEGATIVO") {
                $("#map_SITUA1").prop('checked', false);
                $("#map_SITUA2").prop('checked', true);
                $("#map_SITUA3").prop('checked', false);
                $("#map_SITUA4").prop('checked', false);
                situacion.val(1);
            }
        }
    });


    //INSERT
    //$('#botonGardar').click(function (e) {
    $(document).on('click', '#botonGardar', function (e) {
        e.preventDefault();
        todoCorrecto = 0;

        var optionSelectResultadoCharm = $('#map_IdResultadoCharm').find(":selected");
        var optionSelectResultadoQuino = $('#map_IdResultadoQuino').find(":selected");


        if (situacion.val() === "") {
            abrirDialog("Hay que seleccionar un Tipo de Situación.");
        }

        /************************************/
        /*LOGICA RESULTADO QUINOLONAS*/
        /************************************/
        if (resultado === "QUINOLONAS") {

            if (optionSelectResultadoQuino.text() === "POSITIVO") {
                if ($("#" + preid + "IDMuestraRecogida").val() === "") {
                    abrirDialog("É necesario indicar Mostra Recollida.");
                }

                if (!$("#map_SITUA3").is(":checked")) {
                    abrirDialog("Non coincide o tipo de resultado ca situación.");
                }
            }

            if (optionSelectResultadoQuino.text() === "NEGATIVO") {
                if (situacion.val() !== "1") {
                    abrirDialog("Non coincide situación con resultado negativo.");
                }
            }

            if (optionSelectResultadoQuino.text() === "N/A NON TEN LEITE") {
                if ($("#" + preid + "IDMuestraRecogida").val() !== "") {
                    abrirDialog("Non pode haber mostra para o resultado negativo.");
                }
                if (situacion.val() === "0") {
                    abrirDialog("Non coincide o tipo de resultado ca situación.");
                }
            }

            if (optionSelectResultadoQuino.text() === " ") {
                if (situacion.val() === "1" || situacion.val() === "0") {
                    abrirDialog("Hai que seleccionar un tipo de Resultado Quinolona.");
                }
            }

            if (situacion.val() === "1") {
                if (optionSelectResultadoQuino.text() !== "NEGATIVO") {
                    abrirDialog("Non coincide a situación co tipo de resultado.");
                }
            }
        }

        /******************************************/
        /*LOGICA RESULTADO SI CONTIENE +QUINOLONAS*/
        /******************************************/
        if (resultado.indexOf("+QUINO") >= 0) {

            if ($("#" + preid + "IDMuestraRecogida").val() !== "") {
                if (situacion.val() !== "2") {
                    abrirDialog("Hai valor para mostra recollida, a situación debe ser en espera");
                }
            }

            if (optionSelectResultadoQuino.text() === "POSITIVO") {
                if ($("#" + preid + "IDMuestraRecogida").val() === "" || situacion !== "2") {
                    abrirDialog("Situación Positivo: a situación debe ser en espera e debe haber un valor para mostra recollida.")
                }
            } else {
                if (optionSelectResultadoQuino.text() === "NEGATIVO") {
                    if (optionSelectResultadoCharm.text() !== "NEGATIVO" && optionSelectResultadoCharm.text() !== "POSITIVO" && optionSelectResultadoCharm.text() !== "N/A NON TEN LEITE") {
                        if ($("#" + preid + "IDMuestraRecogida").val() === "") {
                            //SITUACION BLOQUEADO
                            if (situacion.val() !== "0") {
                                abrirDialog("A situación debe ser bloqueado.");
                            }
                        } else {
                            if (situacion.val() !== "2") {
                                abrirDialog("A situación debe ser en espera.");
                            }
                        }
                    }

                    if (optionSelectResultadoCharm.text() === "NEGATIVO") {
                        //LIBERADO Y NO HAY MUESTRA
                        if (situacion.val() !== "1") {
                            abrirDialog("A situación debe ser liberado.")
                        }
                        $("#" + preid + "IDMuestraRecogida").val("");
                    }
                }


                if (optionSelectResultadoCharm.text() === "N/A NON TEN LEITE") {
                    if (optionSelectResultadoQuino.text() !== "N/A NON TEN LEITE") {
                        abrirDialog("Combinación de resultados incompatible, revise a selección.")
                    }
                }

                if (optionSelectResultadoQuino.text() === "N/A NON TEN LEITE") {
                    if (optionSelectResultadoCharm.text() !== "N/A NON TEN LEITE") {
                        abrirDialog("Combinación de resultados incompatible, revise a selección.")
                    }
                }


                if (optionSelectResultadoCharm.text() === "N/A NON TEN LEITE" && optionSelectResultadoQuino.text() === "N/A NON TEN LEITE") {
                    //SITUACION BLOQUEADO SIN MUESTRA
                    if (situacion.val() !== 0) {
                        abrirDialog("A situación debe ser bloqueado.");
                    }
                    $("#" + preid + "IDMuestraRecogida").val("");
                }

                if (optionSelectResultadoCharm.text() === " " && optionSelectResultadoQuino.text() === " ") {
                    //SITUACION PEND. XUNTA Y PEDIR CONFIRMACION AL GRABAR
                    if (situacion.val() !== 3) {
                        abrirDialog("A situación debe ser pend. Xunta.");
                    } else {
                        //PEDIR CONFIRMACION AL GRABAR
                        //Si pulsa ACEPTAR vale 1 y si pulsa CANCELAR vale 2
                        // 	    vResult =ui.MsgBox ("MARCOUSE SITUACION PEND. XUNTA, ¿É CORRECTO?","Advertencia",1)
                        //         if vResult=2 then
                        //             Appdata.FailWithMessage -11888,"REVISE A SITUACIÓN MARCADA OU OS RESULTADOS ESCOLLIDOS."
                        //         end if
                    }
                }

                if (optionSelectResultadoCharm.text() === "" || optionSelectResultadoQuino.text() === "") {
                    abrirDialog("É necesario cumplimentar os selectores Resultado CHARM e Resultado Quinolonas.")
                }
            }
        }


        /************************************/
        //LOGICA RESULTADO CHARM
        //************************************/
        if (resultado !== "OTROS" && resultado !== "QUINOLOAS" && resultado.indexOf("+QUINO") < 0) {
            if ($("#" + preid + "IDMuestraRecogida").val() !== "" && optionSelectResultadoCharm.text() === "N/A NON TEN LEITE") {
                abrirDialog("Non pode haber mostra pra o resultado seleccionado.");
            }

            if (optionSelectResultadoCharm.text() === "NEGATIVO") {
                if (situacion.val() !== "1") {
                    abrirDialog("Ten que escoller situación liberado para o resultado negativo.");
                }
            }

            if (situacion.val() === "1") {
                if (optionSelectResultadoCharm.text() !== "NEGATIVO" && optionSelectResultadoCharm.text() !== "") {
                    abrirDialog("Non coincide o resultado coa situación escollida.");
                }
            }

            if (optionSelectResultadoCharm.text() === " " && situacion.val() <= "1") {
                abrirDialog("Ten que seleccionar un tipo de resultado CHARM.");
            }
        }


        /************************************/
        /*LOGICA RESULTADO OTROS*/
        /************************************/
        if (resultado === "OTROS") {
            if (situacion.val() < "2") {
                abrirDialog("A situación non se corresponde co tipo de resultado.");
            }
        }

        if ($("#" + preid + "FechaHoraVisita").val() === "") {
            abrirDialog("Ten que indicar unha data e hora de visita.");
        }

        if (diferenciaFecha("FechaHoraVisita", "FechaHoraVisita") > 4) {
            abrirDialog("Ten que indicar unha data de visita inferior a 5 días.");
        }
        
        if ($("#" + preid + "FechaSiguienteVisita").val() !== "") {
            if (diferenciaFecha("FechaHoraVisita", "FechaSiguienteVisita") < 0) {
                abrirDialog("Ten que indicar unha data de seguinte visita superior á actual");
            }
        }

        if (!$("#" + preid + "Destrucion").is(":selected")) {
            situacion.val(0);
            $("#map_SITUA1").prop('checked', true);
            $("#map_SITUA2").prop('checked', false);
            $("#map_SITUA3").prop('checked', false);
            $("#map_SITUA4").prop('checked', false);
        }


        if (situacion.val() !== "" && $("#" + preid + "TipoInspeccion").val() !== "" && $("#" + preid + "FechaHoraVisita").val() !== "") {
            if (dev) {
                $("#" + preid + "Actualizado").val(0);
            } else {
                $("#" + preid + "Actualizado").val(1);
            }
        }

        if (dev) {
            $("#" + preid + "Actualizado").val(0);
        } else {
            $("#" + preid + "Actualizado").val(1);
        }

        //console.log("todoCorrecto: " + todoCorrecto);
        //PARA ENVIARLO AL CONTROLLER!
        if (todoCorrecto == 0) {
            var controller = getControllerName();
            var nombreAction = getActionName();

            console.log("controller " + controller);
            console.log("nombreAction " + nombreAction);

            var antiForgeryToken = $('input[name="__RequestVerificationToken"]').val();

            var vm = $('[id ^= form]').serialize();

            $.ajax({
                url: "/" + controller + "/" + nombreAction,
                type: "POST",
                contentType: 'application/x-www-form-urlencoded; charset=UTF-8',
                headers: {
                    "__RequestVerificationToken": antiForgeryToken
                },
                data: vm,
                success: function (data) {
                    //console.log("succes!!!")
                    $(location).attr('href', data);
                },
                error: function (xhr, status, error) {
                    console.log(xhr.responseText);
                    console.log("Se ha producido un error, por favor, contacte con el administrador.");
                }
            });
        }

    });

    //Dialog Alert
    $("#dialogAlert").dialog({
        autoOpen: false,
        modal: true
    });

});


function abrirDialog(mensaje) {
    $("#dialogAlert").dialog("open");
    $("#dialogAlert").modal("show");
    $("#title").text(mensaje);
    todoCorrecto++;
}

function fechaStringToDate(d) {
    var parts = d.split("/");
    var dt = new Date(parseInt(parts[2], 10),
                        parseInt(parts[1], 10) - 1,
                        parseInt(parts[0], 10));
    return dt;
}

function diferenciaFecha(dInicio, dFin) {
    dFin = dFin || 0;

    var d1 = $("#inspeccion_" + dInicio).datepicker({ dateFormat: 'dd,MM,yyyy' }).val();
    var date1 = fechaStringToDate(d1);

    if (dFin === 0) {
        var diferencia = daysDifference(date1, Date.now());
    } else {
        var d2 = $("#inspeccion_" + dFin).datepicker({ dateFormat: 'dd,MM,yyyy' }).val();
        var date2 = fechaStringToDate(d2);
        var diferencia = daysDifference(date1, date2);
    }
    return diferencia;
}

function daysDifference(d0, d1) {
    var diff = new Date(+d1).setHours(12) - new Date(+d0).setHours(12);
    return Math.round(diff / 8.64e7);
}


