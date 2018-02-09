$(document).ready(function () {
    //survey.viaPost();
    survey.generarEncuesta();
});


survey = {

    generarEncuesta: function () {

        $("#btnGenerar").click(function (e) {
            e.preventDefault();

            var nameEncuesta = $('#name').val();
          
            if (nameEncuesta == "")
            {
                bootbox.alert("El título de la encuesta no puede estar vacío");
                return;
            }
         
            var data = {
                name: $('#name').val()
            };

           
            $.ajax({
                type: 'POST',
                url: './Cuestionario/CrearEncuesta',
                data: data
            }).done(function (request) {

                if (request.Status == "Ok") {
                    window.location.href = './Question';
                }
                else {
                    bootbox.alert('Ocurrio una Excepción favor de intentar nuevamente, si el problema persiste favor de notificar al administrador del sistema');
                }


                }).fail(function () {
                
                bootbox.alert('Ocurrio un error favor de intentar nuevamente, si el problema persiste favor de notificar al administrador del sistema');
            });
        });



    },


    //viaPost: function () {

    //    $("#btnAceptar").click(function (e) {
    //        e.preventDefault();

      
    //        window.location.href = './CuestionarioPost/Index?idActividad=' + $('#idActividad').val() + '&idUnidadResponsable=' + $('#idUnidadResponsable').val();
            
    //    });

    //}
};