using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebAppSurveyOnLine.Controllers
{
    public class UsuarioController : Controller
    {
        // GET: Usuario
        public ActionResult Index()
        {
            List<Survey.Entidades.Usuario> listaUsuarios = Survey.Business.Usuario.ConsultaListaUsuarios();
            return View(listaUsuarios);
        }

        public JsonResult CrearUsuario(string email, string password,string name,string lastName,string surName,int id)
        {
			password = Survey.Business.Encriptar.SHA256Encrypt(password);

            if (id == 0)
            {
                List<Survey.Entidades.Usuario> listaUsuarios = Survey.Business.Usuario.CrearUsuario(email, password, name, lastName, surName);
            }
            if(id>0)
            {
                List<Survey.Entidades.Usuario> listaUsuarios = Survey.Business.Usuario.UpdateUsuario(email, password, name, lastName, surName,id);
            }

			return Json(new { Status = "Ok" });
			//return View(listaUsuarios);
        }

        public JsonResult EliminarUsuario( int id)
        {
            
            if (id > 0)
            {
                List<Survey.Entidades.Usuario> listaUsuarios = Survey.Business.Usuario.EliminarUsuario(id);
            }

            return Json(new { Status = "Ok" });
            //return View(listaUsuarios);
        }


        public JsonResult EditarUsuario(string id)
		{
			id = id.Substring(3);
		    Survey.Entidades.Usuario usuario = Survey.Business.Usuario.EditarUsuario(int.Parse(id));

			return Json(new { Status = "Ok", usuario= usuario });
			//return View(listaUsuarios);
		}
	}
}