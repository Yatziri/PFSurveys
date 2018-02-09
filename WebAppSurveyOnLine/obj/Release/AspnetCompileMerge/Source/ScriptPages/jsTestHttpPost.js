$(document).ready(function () {
    
    survey.acciones();
});

survey = {

    acciones: function () {

        $("#btnAceptar").click(function (e) {
            e.preventDefault();
            var idActividad = $('#idActividad').val();
            var idUnidadResponsable = $('#idUnidadResponsable').val();

            if (!$.isNumeric(idActividad)) {
                bootbox.alert('No se puede configurar una encuesta si no se manda parámetros en idActividad!!!');
                return;
            }

            if (!$.isNumeric(idUnidadResponsable)) {
                bootbox.alert('No se puede configurar una encuesta si no se manda parámetros idUnidadResponsable!!!');
                return;
            }

            if (idActividad <= 0 || idUnidadResponsable <= 0 || idActividad === '' || idUnidadResponsable === '') {
                bootbox.alert('El idActividad o el idUnidadResponsable no pueden ser cero, menor a cero o vacios, favor de rectificar!!!');
                return;
            }

            
            var data = { idActividad: idActividad, idUnidadResponsable: idUnidadResponsable};
            $.ajax({
                type: 'POST',
                data: data,
                url: './TestHttpPost/IngresoPost'
            }).done(function (request) {
                if (request.Status === 'Ok') {
                    
                    window.location = request.resultUri;
                }
            }).fail(function () {

                bootbox.alert('Ocurrio un error favor de intentar nuevamente, si el problema persiste favor de notificar al administrador del sistema');
            });

        });
        $("#btnConsultaEncuesta").click(function (e) {
            e.preventDefault();
            window.location = '../EncuestaDinamica/Index?idTipoActividad=' + $('#idTipoActividad').val() + '&idUsuario=' + $('#idUsuario').val() + '&tarea=' + $('#tarea').val();

        });
    },
};