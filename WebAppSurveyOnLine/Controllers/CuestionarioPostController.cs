using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebAppSurveyOnLine.Controllers
{
    public class CuestionarioPostController : Controller
    {
        // GET: CuestionarioPost
        public ActionResult Index(Survey.Entidades.Ctl_Surveys viewModel)
        {
			//  Survey.Entidades.Ctl_Surveys viewModel = new Survey.Entidades.Ctl_Surveys();
			var idActividad = viewModel.idActivity; //HttpContext.Request.Params.Get("idActividad");
			var idUnidadResponsable = viewModel.idUnit; //HttpContext.Request.Params.Get("idUnidadResponsable");
			if (idActividad != null)
            {

                viewModel.idActivity =  idActividad;
                viewModel.idUnit = idUnidadResponsable;


                viewModel = Survey.Business.Encuesta.ExisteCuestionario(viewModel);

                if (viewModel.idSurvey > 0)
                {
                    Session["viewModel"] = viewModel;
                    return RedirectToAction("Index", "Preguntas", viewModel);
                }
                else
                {
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

       


        public ActionResult CrearEncuesta(int idActividad, int idUnidadResponsable)
        {
            var viewModel = new Survey.Entidades.Ctl_Surveys();
            viewModel.idActivity = idActividad;
            viewModel.idUnit = idUnidadResponsable;

           
            return RedirectToAction("index", "Cuestionario", viewModel);
        }
    }
}