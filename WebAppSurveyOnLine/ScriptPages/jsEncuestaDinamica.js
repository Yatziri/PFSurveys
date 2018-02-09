$(document).ready(function () {
  //LoadPreguntas();
    survey.encuestaControles();
    jQuery(function ($) {
        $(".input-datetime").mask("999-99-9999");
    });
});



survey = {

    

    encuestaControles: function () {
       
        jQuery('body').on('change', 'input.texto', function () {
            //alert(this.id);
     

            var id_value = $(this).attr('id'); //get id value
           // var name_value = $('.my_class').$(this).attr('name'); //get name value
            var value = $(this).attr('value'); //get value any input or tag
            var data = {
                idTipoActividad: $("#idTipoActividad").val(),
                idUsuario: $("#idUsuario").val(),
                tarea: $("#tarea").val(),
                campo: id_value,
                valor:value,
                tipoDato:'texto'
            };
            //alert(1);
            $.ajax({
                type: 'POST',
                data: data,
                url: './EncuestaDinamica/GuardarRespuesta'
            }).done(function (request) {
                //alert("se guardo correctamente");
            }).fail(function () {

                bootbox.alert('Ocurrio un error favor de intentar nuevamente, si el problema persiste favor de notificar al administrador del sistema');
                });


        });
        jQuery('body').on('change', 'input.fecha', function () {
            //alert(this.id);


            var id_value = $(this).attr('id'); //get id value
            // var name_value = $('.my_class').$(this).attr('name'); //get name value
            var value = $(this).attr('value'); //get value any input or tag
            var data = {
                idTipoActividad: $("#idTipoActividad").val(),
                idUsuario: $("#idUsuario").val(),
                tarea: $("#tarea").val(),
                campo: id_value,
                valor: value,
                tipoDato: 'fecha'
            };
            //alert(1);
            $.ajax({
                type: 'POST',
                data: data,
                url: './EncuestaDinamica/GuardarRespuesta'
            }).done(function (request) {
                //alert("se guardo correctamente");
            }).fail(function () {

                bootbox.alert('Ocurrio un error favor de intentar nuevamente, si el problema persiste favor de notificar al administrador del sistema');
            });


        });
        jQuery('body').on('change', 'input.fechaTiempo', function () {
            //alert(this.id);


            var id_value = $(this).attr('id'); //get id value
            var campoFecha = id_value.substring(6, id_value.length);
            campoFecha = "FIELDD_" + campoFecha

            var value = $(this).attr('value'); //get value any input or tag

            value = $('#' + campoFecha).val() + ' ' + value;
            //alert(value)
            //return;
            var data = {
                idTipoActividad: $("#idTipoActividad").val(),
                idUsuario: $("#idUsuario").val(),
                tarea: $("#tarea").val(),
                campo: id_value,
                valor: value,
                tipoDato: 'fechaTiempo'
            };
            //alert(1);
            $.ajax({
                type: 'POST',
                data: data,
                url: './EncuestaDinamica/GuardarRespuesta'
            }).done(function (request) {
                if (request.Status == "false") {
                    bootbox.alert('Revise el formato del campo!!!');
                }
            }).fail(function () {

                bootbox.alert('Ocurrio un error favor de intentar nuevamente, si el problema persiste favor de notificar al administrador del sistema');
            });


        });

        jQuery('body').on('change', 'input.checkbox', function () {
            //alert(this.id);


            var id_value = $(this).attr('id'); //get id value
            // var name_value = $('.my_class').$(this).attr('name'); //get name value
            var value=0
            if ($(this).is(':checked')) {
                value = 1;
            } else {
                value=0
            }

           
            var data = {
                idTipoActividad: $("#idTipoActividad").val(),
                idUsuario: $("#idUsuario").val(),
                tarea: $("#tarea").val(),
                campo: id_value,
                valor: value,
                tipoDato: 'entero'
            };
            //alert(1);
            $.ajax({
                type: 'POST',
                data: data,
                url: './EncuestaDinamica/GuardarRespuesta'
            }).done(function (request) {
                //alert("se guardo correctamente");
            }).fail(function () {

                bootbox.alert('Ocurrio un error favor de intentar nuevamente, si el problema persiste favor de notificar al administrador del sistema');
            });

        });

        jQuery('body').on('change', 'input.radioCheck', function () {
            //alert(this.id);


            var id_value = $(this).attr('name'); //get id value
            id_value = id_value.substring(0, id_value.length - 1)
            var name_value = $(this).attr('name'); //get name value

            var valor = $(this).attr('value');
            var value = 0
            if (valor === 'si') {
                name_value = name_value + '2'
                $(name_value).attr('checked', true); 
                //$(this).is(':checked')
                value = 1;
            } else {
                name_value = name_value + '1'
                $(name_value).attr('checked', true); 
                
                value = 0;
            }
           // $(this).is(':checked')
            //alert(name_value);
            //return;
            var data = {
                idTipoActividad: $("#idTipoActividad").val(),
                idUsuario: $("#idUsuario").val(),
                tarea: $("#tarea").val(),
                campo: id_value,
                valor: value,
                tipoDato: 'entero'
            };
            //alert(1);
            $.ajax({
                type: 'POST',
                data: data,
                url: './EncuestaDinamica/GuardarRespuesta'
            }).done(function (request) {
                //alert("se guardo correctamente");
            }).fail(function () {

                bootbox.alert('Ocurrio un error favor de intentar nuevamente, si el problema persiste favor de notificar al administrador del sistema');
            });

        });

        jQuery('body').on('change', 'select.combobox', function () {
            //alert(this.id);


            var id_value = $(this).attr('id'); //get id value
            
            var value = $(this).attr('value'); //get value any input or tag

            var data = {
                idTipoActividad: $("#idTipoActividad").val(),
                idUsuario: $("#idUsuario").val(),
                tarea: $("#tarea").val(),
                campo: id_value,
                valor: value,
                tipoDato: 'entero'
            };
            //alert(1);
            $.ajax({
                type: 'POST',
                data: data,
                url: './EncuestaDinamica/GuardarRespuesta'
            }).done(function (request) {
                //alert("se guardo correctamente");
            }).fail(function () {

                bootbox.alert('Ocurrio un error favor de intentar nuevamente, si el problema persiste favor de notificar al administrador del sistema');
            });
        });

        $(".addressCls").change(function () {
            alert("Handler for .change() called.");
        });

        $('#btnCerrarEncuesta').click(function (e) {
          

            $.ajax({
                type: 'POST',
                //data: data,
                url: './EncuestaDinamica/CerrarEncuesta'
            }).done(function (request) {
                if (request.Status === 'Ok') {

                    bootbox.dialog('El cuestionario se cerro exitosamente', {
                        "label": "OK",
                        "class": "btn-beoro-3"
                    });
                    setTimeout(function () {
                        window.location = './EncuestaDinamica?idTipoActividad='+ request.idTipoActividad +'&idUsuario='+ request.idUsuario +'&tarea='+ request.tarea;
                    }, 2000);
                    //$('#btnCerrarEncuesta').enable(false);
                }
                else
                {
                    bootbox.alert("Revise las preguntas faltan algunas por responder, sin exito el cierre del cuestionario");
                }
            }).fail(function () {

                bootbox.alert('Ocurrio un error favor de intentar nuevamente, si el problema persiste favor de notificar al administrador del sistema');
            });
        });

        $(document).on('click', 'button.uploadButton', function (e) {

             
            var id_value = $(this).attr('id'); //get id value
            id_value = id_value.substring(10, id_value.length)

            id_value = "FIELD_" + id_value;

            var fileUpload = $("#" + id_value).get(0);
                var files = fileUpload.files;

                // Create FormData object  
                var fileData = new FormData();

                // Looping over all files and add it to FormData object  
                for (var i = 0; i < files.length; i++) {
                    fileData.append(files[i].name, files[i]);
                }

                // Adding one more key to FormData object  
                var idTipoActividad = $("#idTipoActividad").val();
                fileData.append('idTipoActividad', idTipoActividad);
                var idUsuario = $("#idUsuario").val();
                fileData.append('idUsuario', idUsuario);
                var tarea = $("#tarea").val();
                fileData.append('tarea', tarea);
                var campo = id_value;
                fileData.append('campo', campo);
                //var valor = value;
                fileData.append('valor','');
                var tipoDato = 'texto';
                fileData.append('tipoDato', tipoDato);
                fileData.append('id_value', id_value);
                  
                $.ajax({
                    url: './EncuestaDinamica/UploadFiles',
                    type: "POST",
                    contentType: false, // Not to set any content header  
                    processData: false, // Not to process data  
                    data: fileData,
                    success: function (result) {
                        alert(result);
                    },
                    error: function (err) {
                        alert(err.statusText);
                    }
                });
            


        });
    }

    

};