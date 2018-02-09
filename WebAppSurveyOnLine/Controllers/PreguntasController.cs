using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.Mvc;
using Survey.Entidades;
namespace WebAppSurveyOnLine.Controllers
{

    public class PreguntasController : Controller
    {
        Survey.Entidades.Ctl_Surveys viewModel = new Survey.Entidades.Ctl_Surveys();
        // GET: Preguntas
        public ActionResult Index()
        {
           
            var viewModel = (Survey.Entidades.Ctl_Surveys)Session["viewModel"];
            return View(viewModel);
        }

        public JsonResult GetPreguntas()
        {
            Ctl_Survey_Questions pregunta = new Ctl_Survey_Questions();

            var viewModel = (Survey.Entidades.Ctl_Surveys)Session["viewModel"];

            DataSet ds = Survey.Business.Encuesta.SelectPreguntas(viewModel);

            List<Ctl_Survey_Questions> listaPreguntas = new List<Ctl_Survey_Questions>();
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                var preguntaResponse = new Ctl_Survey_Questions();
                preguntaResponse.numericalOrder = int.Parse(ds.Tables[0].Rows[i]["NumericalOrder"].ToString());
                preguntaResponse.question = ds.Tables[0].Rows[i]["Question"].ToString();
                preguntaResponse.questionType = ds.Tables[0].Rows[i]["questionType"].ToString();
                if (int.Parse(ds.Tables[0].Rows[i]["IdQuestionType"].ToString()) == 1 || int.Parse(ds.Tables[0].Rows[i]["IdQuestionType"].ToString()) == 2)
                {
                    preguntaResponse.textLength = int.Parse(ds.Tables[0].Rows[i]["TextLength"].ToString());
                }
                switch (int.Parse(ds.Tables[0].Rows[i]["IdQuestionType"].ToString()))
                {
                    case 2:
                    case 3:
                    case 4:
                    case 7:
                        preguntaResponse.optionQuestion = ds.Tables[0].Rows[i]["questionAdditional"].ToString();
                        break;
                    default:
                        preguntaResponse.optionQuestion = "";
                        break;
                }
                

                listaPreguntas.Add(preguntaResponse);
            }
            if (listaPreguntas.Count > 0)
            {

                return Json(new { Status = "Ok", Result = listaPreguntas }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { Status = "Ok" }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult InsertaPregunta(int numericalOrder,string question, int idQuestionType,int textLength,string opcionesRespuesta)
        {
            Ctl_Survey_Questions pregunta = new Ctl_Survey_Questions();

            var viewModel = (Survey.Entidades.Ctl_Surveys)Session["viewModel"];

            pregunta.idSurvey = viewModel.idSurvey;
            pregunta.numericalOrder = numericalOrder;
            pregunta.question = question;
            pregunta.idQuestionType = idQuestionType;
            pregunta.textLength = textLength;
            pregunta.optionQuestion = opcionesRespuesta;

           DataSet ds= Survey.Business.Encuesta.InsertarPregunta(pregunta);
            List<Ctl_Survey_Questions> listaPreguntas = new List<Ctl_Survey_Questions>();
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                var preguntaResponse = new Ctl_Survey_Questions();
                preguntaResponse.numericalOrder =int.Parse(ds.Tables[0].Rows[i]["NumericalOrder"].ToString());
                preguntaResponse.question = ds.Tables[0].Rows[i]["Question"].ToString();
                preguntaResponse.questionType = ds.Tables[0].Rows[i]["questionType"].ToString();
                if (int.Parse(ds.Tables[0].Rows[i]["IdQuestionType"].ToString()) == 1 || int.Parse(ds.Tables[0].Rows[i]["IdQuestionType"].ToString()) == 2)
                {
                    preguntaResponse.textLength = int.Parse(ds.Tables[0].Rows[i]["TextLength"].ToString());
                }
                switch (int.Parse(ds.Tables[0].Rows[i]["IdQuestionType"].ToString()))
                {
                    case 2:
                    case 3:
                    case 4:
                    case 7:
                        preguntaResponse.optionQuestion = ds.Tables[0].Rows[i]["questionAdditional"].ToString();
                        break;
                    default:
                        preguntaResponse.optionQuestion = "";
                        break;
                }


                listaPreguntas.Add(preguntaResponse);
            }

            
          return Json(new { Status = "Ok", Result = listaPreguntas }, JsonRequestBehavior.AllowGet);
        }
    }
}