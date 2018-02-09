$(document).ready(function () {

    survey.creaPregunta();
    $("#txtLongitud").inputmask({ "mask": "99999" });
    $("#txtEntradaNumerica").inputmask({ "mask": "99999" });
});


survey = {

    creaPregunta: function () {

        $("#btnPDF").click(function () {
            $("input[name='GridHtml']").val($("#id=Grid").html());

            bootbox.alert('Ocurrio un error favor de intentar nuevamente, si el problema persiste favor de notificar al administrador del sistema');

        });

        $('#btnVistaPreliminar').click(function (e) {

            //var data = {
            //    numericalOrder: nFilas,
            //    question: txtPregunta,
            //    idQuestionType: $("#selectTipoPregunta").val(),
            //    textLength: txtLongitud,
            //    opcionesRespuesta: respuestas
            //};
            $.ajax({
                type: 'POST',
                //data: data,
                url: './Question/VistaPreliminar'
            }).done(function (request) {
                if (request.Status === 'Ok') {

                    bootbox.alert('Se genero Correctamente');
                }
            }).fail(function () {

                bootbox.alert('Ocurrio un error favor de intentar nuevamente, si el problema persiste favor de notificar al administrador del sistema');
            });
        });

        $('#selectTipoPregunta').change(function () {

            // alert(this.value);
            $('#txtLongitud').val('');
            if (this.value == 2 || this.value == 3 || this.value == 4 || this.value == 7) {
                $("#panelPreguntaMultiple").css("display", "block");
                $("#btnPreguntaSimple").css("display", "none");

            }
            else {
                $("#panelPreguntaMultiple").css("display", "none");
                $("#btnPreguntaSimple").css("display", "block");


            }
            if (this.value == 1 || this.value == 2) {
                $("#longitudPnl").css("display", "block");

            }
            else {
                $("#longitudPnl").css("display", "none");
            }

            if (this.value == 6 || this.value == 7) {
                $("#entradaNumerica").css("display", "block");
            }
            else {
                $("#entradaNumerica").css("display", "none");
            }

            //if (this.value == 8) {
            //    $("#panelRadioButton").css("display", "block");
            //}
            //else {
            //    $("#panelRadioButton").css("display", "none");
            //}

            //if (this.value == 9 || this.value == 11)
            //{
            //    $("#panelCalendario").css("display", "block");
            //}
            //else {
            //    $("#panelCalendario").css("display", "none");
            //}

            //if (this.value == 10 || this.value == 11)
            //{
            //    $("#panelTiempo").css("display", "block");
            //}
            //else
            //{
            //    $("#panelTiempo").css("display", "none");
            //}

            //if (this.value == 12) {
            //    $("#panelGeoLocalizacion").css("display", "block");
            //}
            //else {
            //    $("#panelGeoLocalizacion").css("display", "none");
            //}

        });

        $('#btnNuevaPregunta').click(function (e) {
            $("#pnlNuevo").css("display", "block");
            $("#txtNumericalOrder").val('0');
        });

        $('#btnAgregar').click(function (e) {

            var txtRespuesta = $('#txtRespuesta').val();
            var txtLongitud = $('#txtLongitud').val();
            var txtPregunta = $('#txtPregunta').val();
            var selectTipoPregunta = $('#selectTipoPregunta').val();

            //alert(selectTipoPregunta);
        
            if (txtPregunta.length > 0) {
                var txtRespuesta = $('#txtRespuesta').val();
                if (selectTipoPregunta == 1 || selectTipoPregunta == 2) {
                    if (txtRespuesta.length > 0 && txtLongitud.length > 0) {
                        var nFilas = $('#dt_Respuestas tr').length
                        $('#dt_Respuestas tr:last').after('<tr><td>' + nFilas + '</td><td>' + txtRespuesta + '</td><td><div class="btn-group"><button type="button" class="removeRespuestabutton btn btn-mini" title="Eliminar"><i class="icon-trash"></i></button></div></td></tr>');

                        $('#longitudPnl').hide();
                        $('#txtRespuesta').val('');

                    }
                    else {
                        if (txtLongitud.length < 1) {
                            bootbox.alert('Ingrese la longitud de la pregunta');
                            return;
                        }
                        if (txtRespuesta.length < 1) {
                            bootbox.alert('Ingrese la respuesta');
                            return;
                        }
                    }
                }
                if (selectTipoPregunta == 3 || selectTipoPregunta == 4) {
                    if (txtRespuesta.length > 0) {
                        // var txtRespuesta = $('#txtRespuesta').val();
                       
                        var nFilas = $('#dt_Respuestas tr').length
                        $('#dt_Respuestas tr:last').after('<tr><td>' + nFilas + '</td><td>' + txtRespuesta + '</td><td><div class="btn-group"><button type="button" class="removeRespuestabutton btn btn-mini" title="Eliminar"><i class="icon-trash"></i></button></div></td></tr>');

                        $('#txtRespuesta').val('');
                    }
                    else {
                        if (txtRespuesta.length < 1) {
                            bootbox.alert('Ingrese la respuesta');
                            return;
                        }
                    }
                }
                if (selectTipoPregunta == 6 || selectTipoPregunta == 7) {
                    if (txtRespuesta.length > 0) {
                        var nFilas = $('#dt_Respuestas tr').length
                        $('#dt_Respuestas tr:last').after('<tr><td>' + nFilas + '</td><td>' + txtRespuesta + '</td><td><div class="btn-group"><button type="button" class="removeRespuestabutton btn btn-mini" title="Eliminar"><i class="icon-trash"></i></button></div></td></tr>');

                        // $('#entradaNumerica').hide();
                        // $('#txtEntradaNumerica').val('');

                        $('#txtRespuesta').val('');

                    }
                    else {
                        if (txtRespuesta.length < 1) {
                            bootbox.alert('Ingrese la respuesta');
                            return;
                        }
                    }
                }

            }
            else {
                if (txtPregunta.length < 1) {
                    bootbox.alert('Escriba la pregunta');
                    return;
                }
            }
        });

        $(document).on('click', 'button.removebutton', function (e) {
            

            if (confirm("¿ Esta seguro de eliminar la pregunta ?")) {
                var id_value = $(this).attr('id'); //get id value
               
                var data = { idNumericalOrder: id_value };
                $.ajax({
                    type: 'POST',
                    data: data,
                    url: './Question/EliminarPregunta'
                }).done(function (request) {
                    if (request.Status === 'Ok') {

                        bootbox.dialog('Se Elimino la Pregunta correctamente', {
                            "label": "OK",
                            "class": "btn-beoro-3"
                        });
                        setTimeout(function () {
                            window.location.href = './Question';
                        }, 2000);
                    }
                }).fail(function () {

                    bootbox.alert('Ocurrio un error favor de intentar nuevamente, si el problema persiste favor de notificar al administrador del sistema');
                });

            }
            else {
                return false;
            }



        });

        $(document).on('click', 'button.removeRespuestabutton', function () {
            //var id_value = $(this).attr('id'); //get id value
            //alert(id_value);
            $(this).closest('tr').remove();
            return false;
        });

        $(document).on('click', 'button.editarButton', function (e) {
            var id_value = $(this).attr('id');
            id_value = id_value.substring(3, id_value.length)


            var data = { idNumericalOrder: id_value };
            $.ajax({
                type: 'POST',
                data: data,
                url: './Question/EditarPregunta'
            }).done(function (request) {
                if (request.Status === 'Ok') {

                    var preguntaEditada = request.preguntaEditada;
                    var numericalOrder = request.numericalOrder;
                    var question = request.question;
                    var idQuestionType = request.idQuestionType
                    var questionAdditional = request.questionAdditional;
                    var textLength = request.textLength;
                    $("#pnlNuevo").css("display", "block");
                    $("#pnlModificaPregunta").css("display", "block");
                    $("#txtPregunta").val(question);
                    $("#selectTipoPregunta").val(idQuestionType).change();
                    $("#selectTipoPregunta").attr("readonly", "readonly");
                    $("#txtLongitud").val(textLength);
                    $("#txtNumericalOrder").removeAttr("readonly");
                    $("#txtNumericalOrder").val(numericalOrder);
                    $("#txtNumericalOrder").attr("readonly", "readonly");
                    var respuestas = questionAdditional.split("|");
                    var nFilas = respuestas.length;
                    var contador = 1;
                    $('#dt_Respuestas tr').slice(1).remove();
                    for (var i = 0; i < nFilas - 1; i++) {
                        $('#dt_Respuestas tr:last').after('<tr><td>' + contador + '</td><td>' + respuestas[i] + '</td><td><div class="btn-group"><button type="button" class="removeRespuestabutton btn btn-mini" title="Eliminar"><i class="icon-trash"></i></button></div></td></tr>');
                        contador = contador + 1;
                    }
                }
                else {
                    bootbox.alert('Ocurrio una excepcion favor de intentar nuevamente, si el problema persiste favor de notificar al administrador del sistema');
                }
            }).fail(function () {

                bootbox.alert('Ocurrio un error favor de intentar nuevamente, si el problema persiste favor de notificar al administrador del sistema');
            });
        });


        $(document).on('click', 'button.downButton', function (e) {
            var id_value = $(this).attr('id'); //get id value
            id_value = id_value.substring(3, id_value.length)
            //alert(id_value);
            //return
            var data = { idNumericalOrder: id_value };
            $.ajax({
                type: 'POST',
                data: data,
                url: './Question/BajarPregunta'
            }).done(function (request) {
                if (request.Status === 'Ok') {

                    bootbox.dialog('Se Actualizo el orden correctamente', {
                        "label": "OK",
                        "class": "btn-beoro-3"
                    });
                    setTimeout(function () {
                        window.location.href = './Question';
                    }, 2000);
                }
            }).fail(function () {

                bootbox.alert('Ocurrio un error favor de intentar nuevamente, si el problema persiste favor de notificar al administrador del sistema');
            });
        });


        $(document).on('click', 'button.upButton', function (e) {
            var id_value = $(this).attr('id'); //get id value
            id_value = id_value.substring(3, id_value.length)
            //alert(id_value);
            //return
            var data = { idNumericalOrder: id_value };
            $.ajax({
                type: 'POST',
                data: data,
                url: './Question/SubirPregunta'
            }).done(function (request) {
                if (request.Status === 'Ok') {

                    bootbox.dialog('Se Actualizo el orden correctamente', {
                        "label": "OK",
                        "class": "btn-beoro-3"
                    });
                    setTimeout(function () {
                        window.location.href = './Question';
                    }, 2000);
                }
            }).fail(function () {

                bootbox.alert('Ocurrio un error favor de intentar nuevamente, si el problema persiste favor de notificar al administrador del sistema');
            });
        });

        $('#btnOk').click(function (e) {
            //alert($("#selectTipoPregunta").val());

            var nFilas = $("#dt_Respuestas tr").length;

            if (nFilas <= 1) {
                bootbox.alert("Agregue una respuesta antes de presionar el botón Ok!!!");
                return;
            }

            var respuestas;
            $('#dt_Respuestas tr').each(function () {

                /* Obtener todas las celdas */
                var celdas = $(this).find('td');

                ///* Mostrar el valor de cada celda */
                //celdas.each(function () {
                //    alert($(this).html());
                //});

                /* Mostrar el valor de la celda 2 */

                respuestas = respuestas + $(celdas[1]).html() + '|';

            });
            respuestas = respuestas.substr(4, respuestas.length);/*quita NaN*/
            nFilas = 0;
            var numericalOrder = parseInt($('#txtNumericalOrder').val());
            if (numericalOrder > 0) {
                nFilas = numericalOrder;

            }
            else {
                nFilas = 0;
            }


            //$('#dt_Preguntas tr').length
            var txtPregunta = $('#txtPregunta').val();
            var tipoRespuesta = $("#selectTipoPregunta option:selected").text();

            var txtLongitud = $('#txtLongitud').val();
            txtLongitud = txtLongitud.replace("_", "", ).replace("_", "", ).replace("_", "", ).replace("_", "", ).replace("_", "", );
            //alert(txtLongitud);
            if ($("#selectTipoPregunta").val() == 3 || $("#selectTipoPregunta").val() == 4) {
                txtLongitud = 0;
            }
            if ($("#selectTipoPregunta").val() == 7) {
                txtLongitud = $('#txtEntradaNumerica').val();
                txtLongitud = txtLongitud.replace("_", "", ).replace("_", "", ).replace("_", "", ).replace("_", "", ).replace("_", "", );
            }

            //Hacer la inserccion en BD
            var data = {
                numericalOrder: nFilas,
                question: txtPregunta,
                idQuestionType: $("#selectTipoPregunta").val(),
                textLength: txtLongitud,
                opcionesRespuesta: respuestas
            };
            $.ajax({
                type: 'POST',
                data: data,
                url: './Question/InsertaPregunta'
            }).done(function (request) {
                if (request.Status === 'Ok') {

                    bootbox.dialog('Se inserto la Pregunta correctamente', {
                        "label": "OK",
                        "class": "btn-beoro-3"
                    });
                    setTimeout(function () {
                        window.location.href = './Question';
                    }, 2000);
                }
            }).fail(function () {

                bootbox.alert('Ocurrio un error favor de intentar nuevamente, si el problema persiste favor de notificar al administrador del sistema');
            });

            if (respuestas.length > 1) {//dt_Respuestas
                //$('#dt_Preguntas tr:last').after('<tr><td>' + nFilas + '</td><td>' + txtPregunta + '</td><td>' + tipoRespuesta + '</td><td>' + respuestas + '</td><td>' + txtLongitud + '</td><td><div class="btn-group"><button type="button" class="removebutton btn btn-mini" title="Eliminar"><i class="icon-trash"></i></button></div></td></tr>');
                $('#dt_Respuestas tr:last').after('<tr><td>' + nFilas + '</td><td>' + txtRespuesta + '</td><td><div class="btn-group"><button type="button" class="removeRespuestabutton btn btn-mini" title="Eliminar"><i class="icon-trash"></i></button></div></td></tr>');

                $('#dt_Respuestas tr').slice(1).remove();

                $('#txtPregunta').val('');
                $('#txtRespuesta').val('');
                $("#selectTipoPregunta").val('0');
                $("#pnlNuevo").css("display", "none");
                $("#panelPreguntaMultiple").css("display", "none");
                $('#txtLongitud').val('');
            }
            else {
                //No haya registros
                bootbox.alert('Ingrese la(s) respuesta(s) correspondiente(s)');
                return;
            }
        });

        $('#btnPreguntaSimple').click(function (e) {


            //Validacion de campos            
            var txtPregunta = $('#txtPregunta').val();
            var txtLongitud = $('#txtLongitud').val();

            var iptCalendario = $('#iptCalendario').val();
            var iptTiempo = $('#iptTiempo').val();
            var selectTipoPregunta = $('#selectTipoPregunta').val();
            //var txtGeoLocalizacionUno = $('#txtGeoLocalizacionUno').val();
            //var txtGeoLocalizacionDos = $('#txtGeoLocalizacionDos').val();
            var txtRespuesta = $('#txtRespuesta').val();

            if (selectTipoPregunta == 1) {

                txtLongitud = txtLongitud.replace("_", "", ).replace("_", "", ).replace("_", "", ).replace("_", "", ).replace("_", "", );
                if (txtLongitud <= 0) {
                    bootbox.alert('Ingrese la longitud de la pregunta con un número mayor a cero!!!');
                    return;
                }
                if ((txtLongitud.length > 0) && (txtPregunta.length > 0)) {
                    var nFilas = $('#dt_Preguntas tr').length

                    var numericalOrder = parseInt($('#txtNumericalOrder').val());
                    if (numericalOrder > 0) {
                        nFilas = numericalOrder;

                    }
                    else {
                        nFilas = 0;
                    }

                    var txtPregunta = $('#txtPregunta').val();
                    var respuestas = $('#txtRespuesta').val();
                    var tipoRespuesta = $("#selectTipoPregunta option:selected").text();
                    var txtLongitud = $('#txtLongitud').val();
                    txtLongitud = txtLongitud.replace("_", "", ).replace("_", "", ).replace("_", "", ).replace("_", "", ).replace("_", "", );

                    var data = {

                        numericalOrder: nFilas,
                        question: txtPregunta,
                        idQuestionType: $("#selectTipoPregunta").val(),
                        textLength: txtLongitud,
                        opcionesRespuesta: respuestas
                    };
                    $.ajax({
                        type: 'POST',
                        url: './Question/InsertaPregunta',
                        data: data
                    }).done(function (request) {
                        if (request.Status === 'Ok') {


                            bootbox.dialog('Se inserto la Pregunta correctamente', {
                                "label": "OK",
                                "class": "btn-beoro-3"
                            });
                            setTimeout(function () {
                                window.location.href = './Question';
                            }, 2000);


                        }
                    }).fail(function () {

                        bootbox.alert('Ocurrio un error favor de intentar nuevamente, si el problema persiste favor de notificar al administrador del sistema');
                    });
                    $('#txtPregunta').val('');
                    $('#txtRespuesta').val('');
                    $("#selectTipoPregunta").val('0');
                    $("#panelPreguntaMultiple").css("display", "none");
                    $("#btnPreguntaSimple").css("display", "none");
                    $('#longitudPnl').hide();
                    $('#txtLongitud').val('');
                }
                else {
                    if (txtPregunta.length < 1 && txtLongitud.length < 1) {
                        bootbox.alert('Ingrese la pregunta y la longitud');
                        return;
                    }
                    if (txtPregunta.length < 1) {
                        bootbox.alert('Escriba la pregunta');
                        return;
                    }
                    if (txtLongitud.length < 1) {
                        bootbox.alert('Ingrese la longitud de la pregunta');
                        return;
                    }

                }
            }

            if (selectTipoPregunta == 4) {

                if ((txtRespuesta.length > 0) && (txtPregunta.length > 0)) {
                    var nFilas = $('#dt_Preguntas tr').length;
                    var numericalOrder = parseInt($('#txtNumericalOrder').val());
                    if (numericalOrder > 0) {
                        nFilas = numericalOrder;

                    }
                    else {
                        nFilas = 0;
                    }

                    var txtPregunta = $('#txtPregunta').val();
                    var respuestas = $('#txtRespuesta').val();
                    var tipoRespuesta = $("#selectTipoPregunta option:selected").text();
                    var txtLongitud = $('#txtLongitud').val();
                    txtLongitud = txtLongitud.replace("_", "", ).replace("_", "", ).replace("_", "", ).replace("_", "", ).replace("_", "", );

                    if ($("#selectTipoPregunta").val() == 4) {
                        txtLongitud = 0;
                    }
                    var data = {

                        numericalOrder: nFilas,
                        question: txtPregunta,
                        idQuestionType: $("#selectTipoPregunta").val(),
                        textLength: txtLongitud,
                        opcionesRespuesta: respuestas
                    };
                    $.ajax({
                        type: 'POST',
                        url: './Question/InsertaPregunta',
                        data: data
                    }).done(function (request) {
                        if (request.Status === 'Ok') {
                            bootbox.dialog('Se inserto la Pregunta correctamente', {
                                "label": "OK",
                                "class": "btn-beoro-3"
                            });
                            setTimeout(function () {
                                window.location.href = './Question';
                            }, 2000);
                        }
                    }).fail(function () {

                        bootbox.alert('Ocurrio un error favor de intentar nuevamente, si el problema persiste favor de notificar al administrador del sistema');
                    });
                    $('#txtPregunta').val('');
                    $('#txtRespuesta').val('');
                    $("#selectTipoPregunta").val('0');
                    $("#panelPreguntaMultiple").css("display", "none");
                    $("#btnPreguntaSimple").css("display", "none");
                }
                else {
                    if (txtPregunta.length < 1 && txtLongitud.length < 1) {
                        bootbox.alert('Ingrese la pregunta y la respuesta');
                        return;
                    }
                    if (txtPregunta.length < 1) {
                        bootbox.alert('Escriba la pregunta');
                        return;
                    }
                    if (txtRespuesta.length < 1) {
                        bootbox.alert('Ingrese la respuesta solicitada');
                        return;
                    }

                }
            }

            if (selectTipoPregunta == 5) {

                if (txtPregunta.length < 1) {
                    bootbox.alert('Escriba la pregunta');
                    return;
                }

                var nFilas = $('#dt_Preguntas tr').length

                var numericalOrder = parseInt($('#txtNumericalOrder').val());
                if (numericalOrder > 0) {
                    nFilas = numericalOrder;

                }
                else {
                    nFilas = 0;
                }

                var txtPregunta = $('#txtPregunta').val();
                var respuestas = "";
                var tipoRespuesta = $("#selectTipoPregunta option:selected").text();
                var txtLongitud = 0;


                var data = {

                    numericalOrder: nFilas,
                    question: txtPregunta,
                    idQuestionType: $("#selectTipoPregunta").val(),
                    textLength: txtLongitud,
                    opcionesRespuesta: respuestas
                };
                $.ajax({
                    type: 'POST',
                    url: './Question/InsertaPregunta',
                    data: data
                }).done(function (request) {
                    if (request.Status === 'Ok') {


                        bootbox.dialog('Se inserto la Pregunta correctamente', {
                            "label": "OK",
                            "class": "btn-beoro-3"
                        });
                        setTimeout(function () {
                            window.location.href = './Question';
                        }, 2000);


                    }
                }).fail(function () {

                    bootbox.alert('Ocurrio un error favor de intentar nuevamente, si el problema persiste favor de notificar al administrador del sistema');
                });
                $('#txtPregunta').val('');
                $('#txtRespuesta').val('');
                $("#selectTipoPregunta").val('0');
                $("#panelPreguntaMultiple").css("display", "none");
                $("#btnPreguntaSimple").css("display", "none");
                $('#longitudPnl').hide();
                $('#txtLongitud').val('');

            }


            if (selectTipoPregunta == 12) {

                if (txtPregunta.length < 1) {
                    bootbox.alert('Escriba la pregunta');
                    return;
                }

                var nFilas = $('#dt_Preguntas tr').length;
                var numericalOrder = parseInt($('#txtNumericalOrder').val());
                if (numericalOrder > 0) {
                    nFilas = numericalOrder;

                }
                else {
                    nFilas = 0;
                }
                var txtPregunta = $('#txtPregunta').val();

                if (txtPregunta.length < 1) {
                    bootbox.alert('Escriba la pregunta');
                    return;
                }
                var respuestas = 'Latitud|Longitud|';
                var tipoRespuesta = $("#selectTipoPregunta option:selected").text();
                var txtLongitud = 50; //$('#txtLongitud').val();

                var data = {
                    numericalOrder: nFilas,
                    question: txtPregunta,
                    idQuestionType: $("#selectTipoPregunta").val(),
                    textLength: txtLongitud,
                    opcionesRespuesta: respuestas,

                };
                $.ajax({
                    type: 'POST',
                    url: './Question/InsertaPregunta',
                    data: data
                }).done(function (request) {
                    if (request.Status === 'Ok') {


                        bootbox.dialog('Se inserto la Pregunta correctamente', {
                            "label": "OK",
                            "class": "btn-beoro-3"
                        });
                        setTimeout(function () {
                            window.location.href = './Question';
                        }, 2000);


                    }
                }).fail(function () {

                    bootbox.alert('Ocurrio un error favor de intentar nuevamente, si el problema persiste favor de notificar al administrador del sistema');
                });
                $('#txtPregunta').val('');
                $('#txtRespuesta').val('');
                $("#selectTipoPregunta").val('0');
                $("#panelPreguntaMultiple").css("display", "none");
                $("#btnPreguntaSimple").css("display", "none");
                //$('#panelGeoLocalizacion').hide();
                //$('#txtGeoLocalizacionUno').val('');
                //$('#txtGeoLocalizacionDos').val('');


            }

            //entrada Numerica 6 y 7
            if (selectTipoPregunta == 6 || selectTipoPregunta == 7) {

                var txtEntradaNumerica = $('#txtEntradaNumerica').val();
                txtEntradaNumerica = txtEntradaNumerica.replace("_", "", ).replace("_", "", ).replace("_", "", ).replace("_", "", ).replace("_", "", );
                txtEntradaNumerica = txtEntradaNumerica.replace("_", "", ).replace("_", "", ).replace("_", "", ).replace("_", "", ).replace("_", "", );
                if (txtEntradaNumerica <= 0) {
                    bootbox.alert('Ingrese la longitud de la pregunta con un número mayor a cero!!!');
                    return;
                }

                if (txtPregunta.length > 0) {
                    var nFilas = 0;// $('#dt_Preguntas tr').length
                    var txtPregunta = $('#txtPregunta').val();
                    var respuestas = '';
                    var tipoRespuesta = $("#selectTipoPregunta option:selected").text();

                    var data = {
                        numericalOrder: nFilas,
                        question: txtPregunta,
                        idQuestionType: $("#selectTipoPregunta").val(),
                        textLength: txtEntradaNumerica,
                        opcionesRespuesta: respuestas
                    };
                    $.ajax({
                        type: 'POST',
                        url: './Question/InsertaPregunta',
                        data: data
                    }).done(function (request) {
                        if (request.Status === 'Ok') {
                            bootbox.dialog('Se inserto la Pregunta correctamente', {
                                "label": "OK",
                                "class": "btn-beoro-3"
                            });
                            setTimeout(function () {
                                window.location.href = './Question';
                            }, 2000);

                        }
                    }).fail(function () {

                        bootbox.alert('Ocurrio un error favor de intentar nuevamente, si el problema persiste favor de notificar al administrador del sistema');
                    });
                    $('#txtPregunta').val('');
                    $('#txtRespuesta').val('');
                    $("#selectTipoPregunta").val('0');
                    $("#panelPreguntaMultiple").css("display", "none");
                    $("#btnPreguntaSimple").css("display", "none");
                    $('#entradaNumerica').hide();
                    $('#txtEntradaNumerica').val('');

                    //$('#panelCalendario').hide();
                    //$('#iptCalendario').val('');

                    $('#panelTiempo').hide();
                    $('#iptTiempo').val('');

                }
                else {
                    if (selectTipoPregunta == 6) {
                        if ((txtPregunta.length < 1) && (txtEntradaNumerica.length < 1)) {
                            bootbox.alert('Ingrese la pregunta y la longitud');
                            return;
                        }
                        if (txtPregunta.length < 1) {
                            bootbox.alert('Escriba la pregunta');
                            return;
                        }
                        if (txtEntradaNumerica.length < 1) {
                            bootbox.alert('Escriba la longitud de la respuesta');
                            return;
                        }
                    }
                    if (selectTipoPregunta == 9) {
                        if ((txtPregunta.length < 1) && (iptCalendario.length < 1)) {
                            bootbox.alert('Ingrese la pregunta y la longitud');
                            return;
                        }
                        if (txtPregunta.length < 1) {
                            bootbox.alert('Ingrese la pregunta');
                            return;
                        }
                        if (iptCalendario.length < 1) {
                            bootbox.alert('Ingrese la fecha solicitada');
                            return;
                        }
                    }
                    if (selectTipoPregunta == 10) {
                        if ((txtPregunta.length < 1) && (iptTiempo.length < 1)) {
                            bootbox.alert('Ingrese la pregunta y el tiempo');
                            return;
                        }
                        if (txtPregunta.length < 1) {
                            bootbox.alert('Ingrese la pregunta');
                            return;
                        }
                        if (iptTiempo.length < 1) {
                            bootbox.alert('Ingrese la fecha solicitada');
                            return;
                        }
                    }
                    if (selectTipoPregunta == 11) {
                        if ((txtPregunta.length < 1) && (iptTiempo.length < 1) && (iptCalendario.length < 1)) {
                            bootbox.alert('Ingrese los datos solicitados');
                            return;
                        }
                        if (txtPregunta.length < 1) {
                            bootbox.alert('Ingrese la pregunta');
                            return;
                        }
                        if (iptTiempo.length < 1) {
                            bootbox.alert('Ingrese la fecha solicitada');
                            return;
                        }
                        if (iptCalendario.length < 1) {
                            bootbox.alert('Ingrese el tiempo solicitado');
                            return;
                        }
                    }
                }
            }
            //Fin Entrada Numerica

            if (selectTipoPregunta == 9 || selectTipoPregunta == 10 || selectTipoPregunta == 11) {

                // var txtEntradaNumerica = $('#txtEntradaNumerica').val();
                var txtPregunta = $('#txtPregunta').val();
                if (txtPregunta.length > 0) {
                    var nFilas = $('#dt_Preguntas tr').length
                    var numericalOrder = parseInt($('#txtNumericalOrder').val());
                    if (numericalOrder > 0) {
                        nFilas = numericalOrder;

                    }
                    else {
                        nFilas = 0;
                    }
                    var respuestas = '';
                    var tipoRespuesta = $("#selectTipoPregunta option:selected").text();

                    var data = {
                        numericalOrder: nFilas,
                        question: txtPregunta,
                        idQuestionType: $("#selectTipoPregunta").val(),
                        textLength: 30,//txtLongitud,
                        opcionesRespuesta: respuestas
                    };
                    $.ajax({
                        type: 'POST',
                        url: './Question/InsertaPregunta',
                        data: data
                    }).done(function (request) {
                        if (request.Status === 'Ok') {

                            bootbox.dialog('Se inserto la Pregunta correctamente', {
                                "label": "OK",
                                "class": "btn-beoro-3"
                            });
                            setTimeout(function () {
                                window.location.href = './Question';
                            }, 2000);


                        }
                    }).fail(function () {

                        bootbox.alert('Ocurrio un error favor de intentar nuevamente, si el problema persiste favor de notificar al administrador del sistema');
                    });
                    $('#txtPregunta').val('');
                    $('#txtRespuesta').val('');
                    $("#selectTipoPregunta").val('0');
                    $("#panelPreguntaMultiple").css("display", "none");
                    $("#btnPreguntaSimple").css("display", "none");
                    $('#entradaNumerica').hide();
                    $('#txtEntradaNumerica').val('');

                    //$('#panelCalendario').hide();
                    //$('#iptCalendario').val('');                    

                    $('#panelTiempo').hide();
                    $('#iptTiempo').val('');

                }
                else {


                    if (selectTipoPregunta == 9) {

                        if (txtPregunta.length < 1) {
                            bootbox.alert('Ingrese la pregunta');
                            return;
                        }

                    }
                    if (selectTipoPregunta == 10) {

                        if (txtPregunta.length < 1) {
                            bootbox.alert('Ingrese la pregunta');
                            return;
                        }

                    }
                    if (selectTipoPregunta == 11) {
                        if ((txtPregunta.length < 1) && (iptTiempo.length < 1) && (iptCalendario.length < 1)) {
                            bootbox.alert('Ingrese los datos solicitados');
                            return;
                        }
                        if (txtPregunta.length < 1) {
                            bootbox.alert('Ingrese la pregunta');
                            return;
                        }
                        if (iptTiempo.length < 1) {
                            bootbox.alert('Ingrese la fecha solicitada');
                            return;
                        }
                        if (iptCalendario.length < 1) {
                            bootbox.alert('Ingrese el tiempo solicitado');
                            return;
                        }
                    }
                }
            }

            if (selectTipoPregunta == 8) {
                //var radioNo = $('#radioNo').val();//document.getElementById('radioNo').val();
                //var radioSi = $('#radioSi').val();//document.getElementById('radioSi').val();

                var nFilas = $('#dt_Preguntas tr').length;
                var numericalOrder = parseInt($('#txtNumericalOrder').val());
                if (numericalOrder > 0) {
                    nFilas = numericalOrder;

                }
                else {
                    nFilas = 0;
                }
                var txtPregunta = $('#txtPregunta').val();

                if (txtPregunta.length < 1) {
                    bootbox.alert('Escriba la pregunta');
                    return;
                }
                var respuestas = '';
                var tipoRespuesta = $("#selectTipoPregunta option:selected").text();
                var txtLongitud = 0; //$('#txtLongitud').val();

                var data = {
                    numericalOrder: nFilas,
                    question: txtPregunta,
                    idQuestionType: $("#selectTipoPregunta").val(),
                    textLength: txtLongitud,
                    opcionesRespuesta: respuestas
                };
                $.ajax({
                    type: 'POST',
                    url: './Question/InsertaPregunta',
                    data: data
                }).done(function (request) {
                    if (request.Status === 'Ok') {
                        bootbox.dialog('Se inserto la Pregunta correctamente', {
                            "label": "OK",
                            "class": "btn-beoro-3"
                        });
                        setTimeout(function () {
                            window.location.href = './Question';
                        }, 2000);


                    }
                }).fail(function () {

                    bootbox.alert('Ocurrio un error favor de intentar nuevamente, si el problema persiste favor de notificar al administrador del sistema');
                });
                $('#txtPregunta').val('');
                $('#txtRespuesta').val('');
                $("#selectTipoPregunta").val('0');
                $("#panelPreguntaMultiple").css("display", "none");
                $("#btnPreguntaSimple").css("display", "none");
                //$('#panelGeoLocalizacion').hide();
                //$('#txtGeoLocalizacionUno').val('');
                //$('#txtGeoLocalizacionDos').val('');
                document.getElementById('radioNo').checked = false;
                document.getElementById('radioSi').checked = false;
                //$('#panelRadioButton').hide();


            }

        });

        $('#btnCancelar').click(function (e) {

            $('#dt_Respuestas tr').slice(1).remove();
            $('#txtPregunta').val('');
            $('#txtRespuesta').val('');
            $("#selectTipoPregunta").removeAttr("readonly");
            $("#selectTipoPregunta").val('0');
            $('#txtLongitud').val('');
            $('#panelPreguntaMultiple').hide();

            $('#pnlNuevo').hide();
            $("#btnPreguntaSimple").css("display", "none");
            $("#entradaNumerica").css("display", "none");
            $("#longitudPnl").css("display", "none");
            $("#txtNumericalOrder").removeAttr("readonly");
            $("#txtNumericalOrder").val('0');
            $('#pnlModificaPregunta').hide();

        });

        $('#radioSi').click(function (e) {
            // $("#radioNo").val('0');
            document.getElementById('radioNo').checked = false;
        });

        $('#radioNo').click(function (e) {
            // $("#radioSi").val('0');
            document.getElementById('radioSi').checked = false;
        });

        $('#btnCerrarCuestionario').click(function (e) {
            var nFilas = $('#dt_Preguntas tr').length;

            if (nFilas <= 1) {
                bootbox.alert("Para cerrar el cuestionario debe tener al menos una pregunta !!!");
                return;
            }

            $.ajax({
                type: 'POST',
                //data: data,
                url: './Question/CerrarEncuesta'
            }).done(function (request) {
                if (request.Status === 'Ok') {

                    bootbox.dialog('El cuestionario se cerro exitosamente', {
                        "label": "OK",
                        "class": "btn-beoro-3"
                    });
                    setTimeout(function () {
                        window.location.href = './SurveyClosed';
                    }, 2000);
                }
            }).fail(function () {

                bootbox.alert('Ocurrio un error favor de intentar nuevamente, si el problema persiste favor de notificar al administrador del sistema');
            });
        });


    }




};