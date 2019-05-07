/// <reference path="FuncionesInspeccion.js" />

$(document).ready(function () {

    $("#dialogConfirm").dialog({
        autoOpen: false,
        modal: true
    });


    /****************************************************************/
    /*************************      FILTROS     *********************/
    /****************************************************************/

    //Filtro para index con tabla
    $(document).on('click', '#filtro', function (e) {
        e.preventDefault();
        var parametros = {};
        var antiForgeryToken = $('input[name="__RequestVerificationToken"]').val();
        $('[id^=buscador_]').each(function () {
            var campo = $(this).attr("name").replace("buscador.", "");
            var valor = $(this).val();
            parametros[campo] = valor;
        });
        ajaxFiltro(antiForgeryToken, parametros, 1);
    }); 

    //Filtro para index con mapas
    $(document).on('click', '#filtroMapa', function (e) {
        e.preventDefault();
        var parametros = {};
        var antiForgeryToken = $('input[name="__RequestVerificationToken"]').val();
        $('[id^=buscador_]').each(function () {
            var campo = $(this).attr("name").replace("buscador.", "");
            var valor = $(this).val();
            parametros[campo] = valor;
        });

        var tipoCol = 2;
        if (getControllerName() == "SeguimientoGPS")
        {
            tipoCol = 3;
        }
        ajaxFiltro(antiForgeryToken, parametros, tipoCol);
    });

    //Ordenacion para index con tabla
    var direccion = "1";
    $(document).on('click', '[id^=thFiltro]', function (e) {
        e.preventDefault();
        var id = $(this).attr("id");
        var propiedad = id.replace("thFiltro.", "");
        var separador = propiedad.split(".");
        var coleccion = separador[0];
        var nombreCampo = separador[1];

        var paramFiltro = {};
        var antiForgeryToken = $('input[name="__RequestVerificationToken"]').val();
        $('[id^=buscador_]').each(function () {
            var campo = $(this).attr("name").replace("buscador.", "");
            var valor = $(this).val();
            paramFiltro[campo] = valor;
        });

        var paramOrden = {};
        paramOrden["coleccion"] = coleccion;
        paramOrden["nombreCampo"] = nombreCampo;

        if (nombreCampo === $("#campoOculto").val() && direccion === "1") {
            direccion = "0";
        } else if (nombreCampo === $("#campoOculto").val() && direccion === "0") {
            direccion = "1";
        }

        ajaxOrden(antiForgeryToken, paramFiltro, paramOrden, direccion);
    });

    //Paginador post filtro/ordenacion
    $(document).on("click", ".yourClass a", function (e) {
        e.preventDefault();

        if ($(this).attr('href') === undefined) {
            // Element 'a' has no href
        } else {
            var url = $(this).attr("href");
            var pagina = url.substring(url.length, url.indexOf('=')).replace("=", '');

            var parametros = {};
            var controller = getControllerName();
            var antiForgeryToken = $('input[name="__RequestVerificationToken"]').val();
            $('[id^=buscador_]').each(function () {
                var campo = $(this).attr("name").replace("buscador.", "");
                var valor = $(this).val();
                parametros[campo] = valor;
            });

            if (pagina !== null) {
                parametros["pagina"] = pagina;
            }

            //Este caso es para la paginación post-filtro, se aplica tipoResult 1
            ajaxFiltro(antiForgeryToken, parametros, 1);
        }
    });

    //Despliega/contrae filtro
    $("#btnMuestraFiltro").click(function () {
        if (!$("#divFiltro").is(":visible")) {
            $("#divFiltro").show();
        } else {
            $("#divFiltro").hide();
        }
    });


    
    /*******************************************************************/
    /*************************      CALENDARIO     *********************/
    /*******************************************************************/

    //Incializacion locale ES    
    moment.locale('es', {
        months: ['Enero', 'Febrero', 'Marzo', 'Abril', 'Mayo', 'Junio', 'Julio', 'Agosto', 'Septiembre', 'Octubre', 'Noviembre', 'Diciembre'],
        monthsShort: ['Ene', 'Feb', 'Mar', 'Abr', 'May', 'Jun', 'Jul', 'Ago', 'Sep', 'Oct', 'Nov', 'Dic'],
        monthsParseExact: true,
        weekdays: ['Domingo', 'Lunes', 'Martes', 'Miércoles', 'Jueves', 'Viernes', 'Sábado'],
        weekdaysShort: ['Do', 'Lu', 'Ma', 'Mi', 'Ju', 'Vi', 'Sá'],
        weekdaysMin : 'Do_Lu_Ma_Mi_Ju_Vi_Sá'.split('_'),
        weekdaysParseExact: true,
        longDateFormat: {
            LT: 'HH:mm',
            LTS: 'HH:mm:ss',
            L: 'DD/MM/YYYY',
            LL: 'D MMMM YYYY',
            LLL: 'D MMMM YYYY HH:mm',
            LLLL: 'dddd D MMMM YYYY HH:mm'
        },
        week: { dow: 1 } //lunes primer dia semana
    });

    //Asignación parametros locale ES
    $('.clasePrueba').datetimepicker({
        locale: 'es',
        format: 'L LT'
    });

    //validacion
    $(function () {
        $.validator.addMethod('date',
        function (value, element) {
            if (this.optional(element)) {
                return true;
            }
            var ok = true;
            try {
                $.datepicker.parseDate('dd/mm/yy', value);
            }
            catch (err) {
                ok = false;
            }
            return ok;
        });
        $(".datefield").datepicker({ dateFormat: 'dd/mm/yy', changeYear: true });
    });
    
   

    /*******************************************************************/
    /*********************      PESTAÑAS INSPECCION     ****************/
    /*******************************************************************/

    //Cambio color th index
    $(document).on('mouseover', '.table th a', function (e) {
        $(this).parent().css("background-color", "#f59a2b");
    });
    $(document).on('mouseout', '.table th a', function (e) {
            $(this).parent().css("background-color", "#1296d3");
    });
    $("#pest_datosInspeccion").addClass("colorHoverActivoPest");

    //Pestañas inspeccion
    $('[id^=pest]').click(function () {
        var id = $(this).attr("id");
        var nomP = id.replace("pest_", "");

        $("#" + nomP).css("display", "inline");
        $(this).addClass("colorHoverActivoPest");

        $('[id^=pest]').each(function () {
            var nomPAlt = $(this).attr("id").replace("pest_", "");
            if (nomPAlt !== nomP) {
                $("#" + nomPAlt).css("display", "none");
                $(this).removeClass("colorHoverActivoPest");
            }
        });
    });



    /*******************************************************************/
    /*************      ELIMINAR/ENGADIR ELEMENTOS     *****************/
    /*******************************************************************/

    //Eliminar registro en index
    $(document).on('click', '[id^=delete]', function (e) {
        e.preventDefault(); // <------------------ stop default behaviour of button

        var nombreId = $(this).attr("id");
        var id = nombreId.replace("delete", "");
        var fila = $(this).parent().parent();

        var controller = getControllerName();
        var antiForgeryToken = $('input[name="__RequestVerificationToken"]').val();

        openDialog();
        $("#dialogConfirm").modal("show");

        $(".ui-dialog-titlebar").hide();

        $("#modal-eliminar-imagen").click(function (e) {
            $.ajax({
                url: "/" + controller + "/Delete",
                type: "POST",
                contentType: 'application/x-www-form-urlencoded; charset=utf-8',
                data: {
                    __RequestVerificationToken: antiForgeryToken,
                    id: id
                },
                success: function (data) {
                    fila.remove();
                    $("#dialogConfirm").dialog("close");
                    $("#dialogConfirm").modal("hide");
                },
                error: function (xhr, status, error) {
                    //console.log(xhr.responseText);
                    $('#title').text("Se ha produciddo un error y no se ha podido eliminar el registro.");
                    setTimeout(function () {
                        $("#dialogConfirm").dialog("close");
                        $('#dialogConfirm').modal('hide')
                    }, 3000);
                }
            });
        });
    });

    //Eliminar registro en content
    $(document).on('click', '[id^=removeContent]', function (e) {
        e.preventDefault(); // <------------------ stop default behaviour of button

        var antiForgeryToken = $('input[name="__RequestVerificationToken"]').val();

        var nombreId = $(this).attr("id");
        var elementos = nombreId.split("_");
        var id = elementos[2];
        var controlador = elementos[1];
        var fila = $(this).parent().parent();
        var controller = getBase() + "/" + controlador;


        $("#dialogAlert").dialog({
            autoOpen: false,
            modal: true
        });
        $("#dialogAlert").dialog("open");
        $("#dialogAlert").modal("show");

        $(".ui-dialog-titlebar").hide();
        $('#title').text("¿Está seguro que desea eliminar el registro seleccionado?");
        $("#modal-aceptar-alert").attr('style','visibility:visible; float:left;');

        $("#modal-aceptar-alert").click(function (e) {
            //console.log("/" + controller + "/Delete");
            $.ajax({
                //url: base + "/" + controller + "/Delete",
                //url: "/" + controller + "/Delete",
                url: controller + "/getElement",
                type: "POST",
                contentType: 'application/x-www-form-urlencoded; charset=utf-8',
                data: {
                    __RequestVerificationToken: antiForgeryToken,
                    id: id
                },
                success: function (data) {
                    fila.remove();
                    $("#dialogAlert").dialog("close");
                    $("#dialogAlert").modal("hide");
                },
                error: function (jqXHR, exception) {
                    //console.log(this.url);
                    $('#title').text("Se ha produciddo un error y no se ha podido eliminar el registro.");
                    setTimeout(function () {
                        $("#dialogAlert").dialog("close");
                        $('#dialogAlert').modal('hide')
                    }, 3000);
                }
            });
        });
    });

    //Engadir elemento en content
    $(document).on('click', '.btnEngadirLinhaContent', function (e) {
        e.preventDefault(); // <------------------ stop default behaviour of button

        var controlador = $(this).attr("id");
        var idTabla = $(this).parent().prev().attr("id");
        controller = getBase() + "/" + controlador;

        //console.log("BASE: " + getBase());
        //console.log("controller " + controller);

        $.ajax({
            url: controller + "/getElement",
            type: "GET",
            contentType: 'application/x-www-form-urlencoded; charset=utf-8',
            success: function (data) {
                //engadir linha para edición no content
                $("#" + idTabla + " tr:last").after(data);
                $('.clasePrueba').datetimepicker({
                    locale: 'es',
                    format: 'L LT'
                });
            },
            error: function () {
                openDialog();
                $("#dialogConfirm").modal("show");
                $(".ui-dialog-titlebar").hide();
                $('#title').text("Se ha produciddo un error y no se ha podido eliminar el registro.");
                setTimeout(function () {
                    $("#dialogConfirm").dialog("close");
                    $('#dialogConfirm').modal('hide')
                }, 3000);
            }
        });
        
    });

    //Eliminar elemento engadido
    $(document).on('click', '#deleteFilaContent_ajax', function (e) {
        var idTr = $(this).parent().parent().attr("id");
        $("#" + idTr).remove();
    });



    //Asignar valor select TipoUsuario a la propiedad tipo texto correspondiente
    $("#selectTipoUsuario").change(function () {
        var valor = $(this).find(":selected").text();
        var elemOculto = $(this).next();
        elemOculto.val(valor);
    });

    //Filtro inspecciones -> Asignar valor select ResultadoCharm a la propiedad oculta tipo texto ResultadoCharm
    $("#buscador_SelectResultadoCharm").change(function () {
        var textoSelected = $('option:selected', this).text();
        var hidden = $("#buscador_ResultadoCHARM");
        hidden.val(textoSelected);
    });

    //Filtro inspecciones -> Asignar valor select ResultadoQuino a la propiedad oculta tipo texto ResultadoQuino
    $("#buscador_SelectResultadoQuino").change(function () {
        var textoSelected = $('option:selected', this).text();
        var hidden = $("#buscador_RESULTADOQUINO");
        hidden.val(textoSelected);
    });

    //Exportar a formato CSV listado inspecciones vinculado a mapa    
    $(document).on('click', '#btnExportar', function () {
        var parametros = {};

        //analizar opción post sin ajax
        //$('[id^=buscador_]').each(function () {
        //    var campo = $(this).attr("name").replace("buscador.", "");
        //    var valor = $(this).val();
        //    parametros[campo] = valor;
        //});

        var pathname = window.location.pathname;
        pathname = pathname.replace("Index", "");
        var urlCompleta = window.location.protocol + "//" + window.location.host + pathname;
        var funcion = urlCompleta + 'tablaToCSV';
        var form = $('#limpiaFiltro :input').serialize();
        window.location = funcion + "?" + form;

    });

});



    var map;
    var mapOptions;

    function initialize() {
        mapOptions = {
            center: new google.maps.LatLng(43.2155828, -8.064984),
            zoom: 9,
            mapTypeId: google.maps.MapTypeId.HYBRID,
            scrollwheel: true,
            draggable: true,
            panControl: true,
            zoomControl: true,
            mapTypeControl: true,
            scaleControl: true,
            streetViewControl: true,
            overviewMapControl: true,
            rotateControl: true,
        };

        map = new google.maps.Map(document.getElementById("map"), mapOptions);
        var marker = new google.maps.Marker({ position: new google.maps.LatLng(43.2097741, -8.065404), map: map });
    }
    
    function ajaxFiltro(antiForgeryToken, buscador, tipoResult) {

        var controller = getControllerName();
        var nombreAction = getActionName();

        buscador["nombreAction"] = nombreAction;

        //console.log(JSON.stringify(buscador));
        //console.log("controller " + controller);
        //console.log("nombreAction " + nombreAction);
        //console.log("tipoResult " + tipoResult);      nombreAction

        $.ajax({
            url: "/" + controller + "/Filtro",
            type: "POST",
            contentType: "application/json; charset=utf-8",
            headers: {
                "__RequestVerificationToken": antiForgeryToken
            },
            data: JSON.stringify(buscador),
            success: function (data) {
                switch (tipoResult) {
                    case 1:
                        //tabla index (normal) o paginación post-filtro // pintaTabla en caso de que sea mapa gps
                        if (controller.indexOf("GPS") > -1) {
                            pintaTabla(controller, buscador, antiForgeryToken);
                        } else {
                            $('#indexReal').html(data);
                        }
                        break;
                    case 2:
                        //ruta y markers GPS (InspeccionesGPS, InspeccionesQuinoGPS, VisitasGanaderoGPS)
                        marcadoresGPS(data, true);
                        pintaTabla(controller, buscador, antiForgeryToken);
                        break;
                    case 3:
                        //solo markers GPS (Seguimiento GPS)
                        marcadoresGPS(data, false);
                        pintaTabla(controller, buscador, antiForgeryToken);
                        break;
                }
            },
            error: function (xhr, status, error) {
                console.log(xhr.responseText);
                console.log("Se ha producido un error, por favor, contacte con el administrador.");
            }
        });
    }

    function ajaxOrden(antiForgeryToken, buscador, paramOrden, direccion) {
        var controller = getControllerName();
        var nombreAction = getActionName();

        var param = {
            buscador,
            paramOrden,
            direccion,
            nombreAction
        };

        //console.log("ajaxOrden controller " + controller);
        //console.log("ajaxOrden nombreAction " + nombreAction);

        $.ajax({
            url: "/" + controller + "/Orden",
            type: "POST",
            contentType: "application/json; charset=utf-8",
            headers: {
                "__RequestVerificationToken": antiForgeryToken
            },
            data: JSON.stringify(param),
            success: function (data) {
                $('#indexReal').html(data);
                $("#campoOculto").val(paramOrden["nombreCampo"]);
            },
            error: function () {
                alert("Se ha producido un error, por favor, contacte con el administrador.");
            }
        });
    }

    function marcadoresGPS(data, pintarRuta) {
        map = new google.maps.Map(document.getElementById("map"), mapOptions);
        var contador = 0;
        var puntosRuta = [];

        var pinColor;
        var pinImage;
        var pinShadow = new google.maps.MarkerImage("http://chart.apis.google.com/chart?chst=d_map_pin_shadow",
            new google.maps.Size(40, 37),
            new google.maps.Point(0, 0),
            new google.maps.Point(12, 35));

        $(jQuery.parseJSON(data)).each(function () {
            var cx = this.cx;
            var cy = this.cy;
            var seriegan = this.seriegan;
            var fecha = this.fecha;

            var latLng = new google.maps.LatLng(cy, cx);
            puntosRuta.push(latLng);

            if (contador === 0) {
                map.setCenter(latLng);
                pinColor = "f9ff66";
            } else {
                pinColor = "FE7569";
            }

            pinImage = new google.maps.MarkerImage("http://chart.apis.google.com/chart?chst=d_map_pin_letter&chld=%E2%80%A2|" + pinColor,
                new google.maps.Size(21, 34),
                new google.maps.Point(0, 0),
                new google.maps.Point(10, 34));


            var marker = new google.maps.Marker({
                position: latLng,
                map: map,
                icon: pinImage,
                shadow: pinShadow
            });

            var infowindow = new google.maps.InfoWindow({
                content: seriegan + " " + fecha
            });

            marker.addListener('click', function () {
                infowindow.open(map, marker);
            });

            contador++;
        });

        if (pintarRuta) {
            var rutas = new google.maps.Polyline({
                path: puntosRuta,
                strokeColor: "#58ACFA",
                strokeOpacity: 1.0,
                strokeWeight: 2
            });

            rutas.setMap(map);
        }
    }
    
    function pintaTabla(controller, buscador, antiForgeryToken) {

        $.ajax({
            url: "/" + controller + "/PintaTabla",
            type: "POST",
            contentType: "application/json; charset=utf-8",
            headers: {
                "__RequestVerificationToken": antiForgeryToken
            },
            data: JSON.stringify(buscador),
            success: function (data) {
                $('#indexReal').html(data);
            },
            error: function (xhr, status, error) {
                console.log(xhr.responseText);
                console.log("Se ha producido un error, por favor, contacte con el administrador.");
            }
        });
    }

    function openDialog() {
        $("#dialogConfirm").dialog({
            autoOpen: false,
            modal: true
        });
        $("#dialogConfirm").dialog("open");
    }



