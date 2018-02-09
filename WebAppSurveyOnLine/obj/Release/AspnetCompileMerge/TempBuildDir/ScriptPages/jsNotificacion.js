
$(document).ready(function () {
   
    survey_notifications.n_bootbox();
});

//* notifications
survey_notifications = {
   
    n_bootbox: function () {


        $(window).on("load", function () {
            bootbox.dialog("I am a custom dialog", [{
                "label": "Success!",
                "class": "btn-success",
                "callback": function () {
                    console.log("great success");
                }
            }, {
                "label": "Danger!",
                "class": "btn-danger",
                "callback": function () {
                    console.log("uh oh, look out!");
                }
            }, {
                "label": "Click ME!",
                "class": "btn-primary",
                "callback": function () {
                    console.log("Primary button");
                }
            }, {
                "label": "Just a button..."
            }]);
        });

       
      
       
       
        $("#bb-dialog").click(function (e) {
            e.preventDefault();
            bootbox.dialog("I am a custom dialog", [{
                "label": "Success!",
                "class": "btn-success",
                "callback": function () {
                    console.log("great success");
                }
            }, {
                "label": "Danger!",
                "class": "btn-danger",
                "callback": function () {
                    console.log("uh oh, look out!");
                }
            }, {
                "label": "Click ME!",
                "class": "btn-primary",
                "callback": function () {
                    console.log("Primary button");
                }
            }, {
                "label": "Just a button..."
            }]);
        });
      
    }
};