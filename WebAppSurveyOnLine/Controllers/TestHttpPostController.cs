using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebAppSurveyOnLine.Controllers
{
	public class TestHttpPostController : Controller
	{
		// GET: TestHttpPost
		public ActionResult Index()
		{
			
			string[] myCookies = Request.Cookies.AllKeys;
			foreach (string cookie in myCookies)
			{
				Response.Cookies[cookie].Expires = DateTime.Now.AddDays(-1);
			}


			//put your logout logic here, remove the user object from the session.
			Hashtable sessions = (Hashtable)System.Web.HttpContext.Current.Application["WEB_SESSIONS_OBJECT"];
			if (sessions == null)
			{
				sessions = new Hashtable();
			}

			Session.Abandon();
	

			Response.Cookies.Clear();
		
			Session.Clear();
			Session.RemoveAll();
			
			System.Web.HttpContext.Current.Application.Lock();
			System.Web.HttpContext.Current.Application["WEB_SESSIONS_OBJECT"] = sessions;
			System.Web.HttpContext.Current.Application.UnLock();

			return View();
		}

        [HttpPost]
        public ActionResult IngresoPost(string idActividad, string idUnidadResponsable)
        {
            Survey.Entidades.Ctl_Surveys viewModel = new Survey.Entidades.Ctl_Surveys();


            //viewModel.idActivity = int.Parse(idActividad);
            //viewModel.idUnit = int.Parse(idUnidadResponsable);

            return Json(new { Status = "Ok", resultUri =Url.Action("Index", "SurveyPost", new { idActividad= idActividad, idUnidadResponsable= idUnidadResponsable }) }, JsonRequestBehavior.AllowGet);

            // return RedirectToAction("Index", "SurveyPost", new { idActividad = idActividad, idUnidadResponsable = idUnidadResponsable });

            //return RedirectToAction("Index", "SurveyPost", viewModel);

            //return RedirectToAction("Index", "CuestionarioPost",viewModel);
        }




        [HttpPost]
        public ActionResult TestContestaEncuesta(int idTipoActividad, int idUsuario, string tarea)
        {
            Survey.Entidades.Ctl_Surveys viewModel = new Survey.Entidades.Ctl_Surveys();


            

            return RedirectToAction("Index" , "EncuestaDinamica",new {idTipoActividad =idTipoActividad ,idUsuario = idUsuario ,tarea =  tarea });

                //return RedirectToAction("Index", "CuestionarioPost",viewModel);
            }
    }
}