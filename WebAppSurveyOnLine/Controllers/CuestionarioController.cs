using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.Mvc;
using System.Configuration;
using Survey.Entidades;

namespace WebAppSurveyOnLine.Controllers
{
    public class CuestionarioController : Controller
    {
        Survey.Entidades.Ctl_Surveys viewModel = new Survey.Entidades.Ctl_Surveys();
        public ActionResult Index()
        {
            
                var idActividad = HttpContext.Request.Params.Get("idActivity");
                var idUnidadResponsable = HttpContext.Request.Params.Get("idUnit");
            if (idActividad != null)
            {

                viewModel.idActivity = int.Parse(idActividad);
                viewModel.idUnit = int.Parse(idUnidadResponsable);

                viewModel = Survey.Business.Encuesta.ExisteCuestionario(viewModel);

                Session["viewModel"] = viewModel;

                if (viewModel.status == 0)
                {
                    return View(viewModel);
                }
                if (viewModel.status == 1)
                {
                    return View(viewModel);
                }

            }


                return View(viewModel);
           
            
        }



        public JsonResult CrearEncuesta(string name)
        {
            try
            {

                var viewModel = (Survey.Entidades.Ctl_Surveys)Session["viewModel"];

                name = name.Replace('*', ' ');
                name = name.Replace("Delete", " ");
                name = name.Replace("delete", " ");
                name = name.Replace("Select", " ");
                name = name.Replace("select", " ");
                name = name.Replace("update", " ");
                name = name.Replace("Update", " ");
                viewModel.name = name;

                DataSet ds = Survey.Business.Encuesta.CrearEncuesta(viewModel);

                var bandera = ds.Tables[0].Rows[0]["flag"].ToString();
                viewModel.idSurvey = int.Parse(ds.Tables[0].Rows[0]["id"].ToString());

                string ruta = ConfigurationManager.AppSettings["ExpedienteSurvey"] + "CTL_ANSWERS_AAAA_" + viewModel.idSurvey.ToString() + "_" + viewModel.idActivity.ToString();

                if (Survey.Business.Encuesta.CrearDirectorio(ruta))
                {

                    

                    Session["viewModel"] = viewModel;

                }
                else
                {
                    return Json(new { Status = "false" });
                }

               
                //viewModel.status = 1;
                //ViewBag["flag"] = true;
                return Json(new { Status = "Ok" });
            }
            catch
            {
                return Json(new { Status = "false" });
            }
            
        }
    }
}