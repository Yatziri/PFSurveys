using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebAppSurveyOnLine.Controllers
{
    public class SurveyPostController : Controller
    {
		// GET: SurveyPost
		public ActionResult Index()
		{
            try
            {

                Survey.Entidades.Ctl_Surveys viewModel = new Survey.Entidades.Ctl_Surveys();
                var idActividadSTR = HttpContext.Request.Params.Get("idActividad");
                var idUnidadResponsableSTR = HttpContext.Request.Params.Get("idUnidadResponsable");

                if (idActividadSTR == null || idUnidadResponsableSTR == null)
                {
                    ViewBag.Message = "Error";
                    return View();
                }

                if(!IsNumeric(idActividadSTR))
                {
                    ViewBag.Message = "Error";
                    return View();
                }

                if (!IsNumeric(idUnidadResponsableSTR))
                {
                    ViewBag.Message = "Error";
                    return View();
                }

                var idActividad = int.Parse(HttpContext.Request.Params.Get("idActividad"));
                var idUnidadResponsable = int.Parse(HttpContext.Request.Params.Get("idUnidadResponsable"));

                if(idActividad <= 0 || idUnidadResponsable <= 0  )
                {
                    ViewBag.Message = "Error";
                    return View();
                }

                if (idActividad != null)
                {

                    viewModel.idActivity = idActividad;
                    viewModel.idUnit = idUnidadResponsable;


                    viewModel = Survey.Business.Encuesta.ExisteCuestionario(viewModel);

                    if (viewModel.idSurvey > 0)
                    {
                        if (viewModel.status == 0)
                        {
                            Session["viewModel"] = viewModel;
                            return RedirectToAction("Index", "SurveyClosed", viewModel);
                        }
                        else
                        {
                            Session["viewModel"] = viewModel;
                            return RedirectToAction("Index", "Question", viewModel);
                        }
                    }


                    if (viewModel.name == null)
                    {
                        return View(viewModel);
                    }

                    if (viewModel.status == 0)
                    {
                        return View(viewModel);
                    }

                }
                else
                {
                    viewModel.idActivity = 0;
                    viewModel.idUnit = 0;
                }


                return View(viewModel);
            }
            catch
            {
                ViewBag.Messagge = "Error";
                return View();
            }
		}

        public static bool IsNumeric(string s)
        {
            float output;
            return float.TryParse(s, out output);
        }


        public ActionResult CrearEncuesta(int idActividad, int idUnidadResponsable)
		{
			var viewModel = new Survey.Entidades.Ctl_Surveys();
			viewModel.idActivity = idActividad;
			viewModel.idUnit = idUnidadResponsable;

			//Survey.Business.Encuesta.CrearEncuesta(viewModel);
			return RedirectToAction("index", "Cuestionario", viewModel);
		}
	}
}