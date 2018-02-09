using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebAppSurveyOnLine.Controllers
{
    public class MasterPageController : Controller
    {
        // GET: MasterPage
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Salir()
        {
            // Finalizar sesión
            

            
            string[] myCookies = Request.Cookies.AllKeys;
            foreach (string cookie in myCookies)
            {
                Response.Cookies[cookie].Expires = DateTime.Now.AddDays(-1);
            }


          

            Session.Abandon();
          

            Response.Cookies.Clear();
            Session["oUsuario"] = null;
            Session.Clear();
            Session.RemoveAll();





            System.Web.HttpContext.Current.Application.Lock();
          
            System.Web.HttpContext.Current.Application.UnLock();


            return RedirectToAction("Index", "Login");
        }
    }
}