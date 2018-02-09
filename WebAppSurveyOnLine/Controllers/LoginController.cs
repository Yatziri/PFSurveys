using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebAppSurveyOnLine.Controllers
{
	public class LoginController : Controller
	{
		// GET: Login
		public ActionResult Index()
		{
            

			return View();
		}

		[HttpPost]
		public ActionResult Acceso()
		{
		    var usuario = HttpContext.Request.Params.Get("login_name");
			var contrasena = HttpContext.Request.Params.Get("login_password");
            contrasena = Survey.Business.Encriptar.SHA256Encrypt(contrasena);
            var bandera = Survey.Business.Acceso.ValidaUsuario( usuario, contrasena);
            if (bandera)
            {
                return RedirectToAction("Index", "TestHttpPost");
            }
            else
            {
                TempData["msg"] = "<script>alert('El usuario no existe, intente nuevamente!!!');</script>";
            }

            return View("Index");
        }
	}

	 
}