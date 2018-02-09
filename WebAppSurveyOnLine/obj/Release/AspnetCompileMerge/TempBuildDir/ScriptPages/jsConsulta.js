$(document).ready(function () {
    //* datatables
    survey.consultaCuestionario();
    
});

survey = {
    consultaCuestionario: function () {

        $(document).on('click', 'button.consultaButton', function () {
            var id_value = $(this).attr('id'); //get id value
            //alert(id_value);
            var data = {id: id_value};
            $.ajax({
                type: 'POST',
                data: data,
                url: './ConsultaEncuesta/ConsultaRespuestas'
            }).done(function (request) {
                if (request.Status === 'Ok') {

                    window.location.href = './ConsultaEncuesta';
                    //bootbox.alert('Se genero Correctamente');
                }
            }).fail(function () {

                bootbox.alert('Ocurrio un error favor de intentar nuevamente, si el problema persiste favor de notificar al administrador del sistema');
            });
            
        });


    }
};