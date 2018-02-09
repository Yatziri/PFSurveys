$(document).ready(function () {
    //* datatables
    survey.user();

});

survey = {
    user: function () {

        $('#btnCrear').click(function (e) {
            var email = $("#email").val();
            if (email == "") {
                bootbox.alert("Ingrese el email");
                return;
            }
            var password = $("#password").val();
            if (password == "") {
                bootbox.alert("Ingrese la contraseña");
                return;
            }
            var name = $("#name").val();
            if (name == "") {
                bootbox.alert("Ingrese el nombre");
                return;
            }
            var lastName = $("#lastName").val();
            if (lastName == "") {
                bootbox.alert("Ingrese el apellido paterno");
                return;
            }
            var id = $("#id").val();
            if (id=="")
            {
                id = 0;
            }
            var surName = $("#surName").val();
             var data = {
                 email:email ,
                 password:password ,
                 name: name,
                 lastName:lastName ,
                 surName: surName,
                 id:id
            };

            $.ajax({
                type: 'POST',
                data: data,
                url: '../Usuario/CrearUsuario'
            }).done(function (request) {
                if (request.Status === 'Ok') {
                    $("#id").removeClass("readOnly");
                    $("#div_id").css("display", "none");
                    bootbox.dialog('El Usuario fue creado/actualizado exitosamente!!!', {
                        "label": "OK",
                        "class": "btn-beoro-3"
                    });
                    setTimeout(function () {
                        window.location.href = './Index';
                    }, 2000);
                }
            }).fail(function () {

                bootbox.alert('Ocurrio un error favor de intentar nuevamente, si el problema persiste favor de notificar al administrador del sistema');
            });
        });


        $(document).on('click', 'button.editarButton', function () {
            var id_value = $(this).attr('id'); //get id value
            //alert(id_value);
            //return;
            var data = { id: id_value };
            $.ajax({
                type: 'POST',
                data: data,
                url: '../Usuario/EditarUsuario'
            }).done(function (request) {
                if (request.Status === 'Ok') {
                    $("#email").val(request.usuario.email);
                    $("#password").val(request.usuario.password);
                    $("#name").val(request.usuario.name);
                    $("#lastName").val(request.usuario.lastName);
                    $("#surName").val(request.usuario.surName);
                    $("#id").val(request.usuario.id);   
                    $("#id").attr("readonly", "readonly");
                    $("#div_id").css("display", "block");
                }
            }).fail(function () {

                bootbox.alert('Ocurrio un error favor de intentar nuevamente, si el problema persiste favor de notificar al administrador del sistema');
            });

        });

        $(document).on('click', 'button.removebutton', function () {
            var id_value = $(this).attr('id'); //get id value
            if (id_value==0)
            {
                return;
            }
            //return;
            var data = { id: id_value };
            $.ajax({
                type: 'POST',
                data: data,
                url: '../Usuario/EliminarUsuario'
            }).done(function (request) {
                if (request.Status === 'Ok') {
                    $("#id").removeClass("readOnly");
                    $("#div_id").css("display", "none");
                    bootbox.dialog('El usuario se elimino exitosamente!!!', {
                        "label": "OK",
                        "class": "btn-beoro-3"
                    });
                    setTimeout(function () {
                        window.location.href = './Index';
                    }, 2000);
                }
              
            }).fail(function () {

                bootbox.alert('Ocurrio un error favor de intentar nuevamente, si el problema persiste favor de notificar al administrador del sistema');
            });

        });


    }
};