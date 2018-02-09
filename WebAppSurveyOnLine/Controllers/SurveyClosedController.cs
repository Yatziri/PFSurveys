using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Web;
using System.Web.Mvc;
using Survey.Entidades;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.tool.xml;
using iTextSharp.text.html.simpleparser;
namespace WebAppSurveyOnLine.Controllers
{
    public class SurveyClosedController : Controller
    {
        // GET: SurveyClosed
        public ActionResult Index()
        {
            var viewModel = new Survey.Entidades.Question();

            Survey.Entidades.Ctl_Surveys viewModelSurvey = (Survey.Entidades.Ctl_Surveys)Session["viewModel"];

            viewModel.encuesta = viewModelSurvey;


            viewModel.preguntas = GetPreguntas(viewModelSurvey);


            return View(viewModel);
        }

        public List<Ctl_Survey_Questions> GetPreguntas(Survey.Entidades.Ctl_Surveys viewModelSurvey)
        {
            Ctl_Survey_Questions pregunta = new Ctl_Survey_Questions();


            DataSet ds = Survey.Business.Encuesta.SelectPreguntas(viewModelSurvey);

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
                if (int.Parse(ds.Tables[0].Rows[i]["IdQuestionType"].ToString()) == 6 || int.Parse(ds.Tables[0].Rows[i]["IdQuestionType"].ToString()) == 7)
                {
                    preguntaResponse.textLength = int.Parse(ds.Tables[0].Rows[i]["TextLength"].ToString() == "" ? "0" : ds.Tables[0].Rows[i]["TextLength"].ToString());
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

            return listaPreguntas;
        }


        [HttpPost]
        [ValidateInput(false)]
        public ActionResult VistaPreliminar(string GridHtml)
        {
            try
            {
                Survey.Entidades.Ctl_Surveys viewModelSurvey = (Survey.Entidades.Ctl_Surveys)Session["viewModel"];

                string HTMLContent =Survey.Business.Encuesta.GetPreguntasPDF(viewModelSurvey);


                //string HTMLContent = "Hello <b>World</b>";

                if (HTMLContent.Length > 0)
                {
                    using (MemoryStream stream = new System.IO.MemoryStream())
                    {
                        StringReader sr = new StringReader(HTMLContent);
                        Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 100f, 0f);
                        PdfWriter writer = PdfWriter.GetInstance(pdfDoc, stream);
                        pdfDoc.Open();
                        XMLWorkerHelper.GetInstance().ParseXHtml(writer, pdfDoc, sr);
                        pdfDoc.Close();
                        return File(stream.ToArray(), "application/pdf", "Grid.pdf");
                    }
                }


                return Json(new { Status = "false" }, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(new { Status = "false" }, JsonRequestBehavior.AllowGet);
            }
        }

        //private string GetPreguntasPDF(Survey.Entidades.Ctl_Surveys viewModelSurvey)
        //{
        //    var campo = "";
        //    Ctl_Survey_Questions pregunta = new Ctl_Survey_Questions();

        //    Survey.Entidades.Ctl_Surveys viewModel = new Ctl_Surveys();


        //    DataSet ds = Survey.Business.Encuesta.SelectPreguntas(viewModelSurvey);


        //    List<Ctl_Survey_Questions> listaPreguntas = new List<Ctl_Survey_Questions>();
        //    var datos = "<table>";
        //    datos = datos + "<tr><td></td><td>Encuesta: " + viewModelSurvey.name + "</td><td></td></tr>";
        //    datos = datos + "<tr><td>idEncuesta: " + viewModelSurvey.idSurvey + "</td><td></td><td></td></tr>";
        //    datos = datos + "<tr><td>idActividad: " + viewModelSurvey.idActivity + "</td><td></td><td></td></tr>";
        //    datos = datos + "<tr><td>idUnidad: " + viewModelSurvey.idUnit + "</td><td></td><td></td></tr>";
        //    datos = datos + "<tr><td></td><td></td><td></td></tr>";
        //    datos = datos + "<tr><td colspan='3'>_____________________________________________________________________________________</td></tr>";
        //    datos = datos + "<tr><td colspan='3'>Preguntas:</td></tr>";
        //    datos = datos + "</table><br/><br/><br/>";
        //    var renglon = "";

        //    if (ds.Tables[0].Rows.Count > 0)
        //    {
        //        renglon = "<table>";
        //        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        //        {
        //            var preguntaResponse = new Ctl_Survey_Questions();
        //            preguntaResponse.numericalOrder = int.Parse(ds.Tables[0].Rows[i]["NumericalOrder"].ToString());
        //            preguntaResponse.question = ds.Tables[0].Rows[i]["Question"].ToString();
        //            preguntaResponse.questionType = ds.Tables[0].Rows[i]["questionType"].ToString();
        //            preguntaResponse.idQuestionType = int.Parse(ds.Tables[0].Rows[i]["IdQuestionType"].ToString());
        //            preguntaResponse.id = int.Parse(ds.Tables[0].Rows[i]["Id"].ToString());
        //            if (int.Parse(ds.Tables[0].Rows[i]["IdQuestionType"].ToString()) == 1 || int.Parse(ds.Tables[0].Rows[i]["IdQuestionType"].ToString()) == 2)
        //            {
        //                preguntaResponse.textLength = int.Parse(ds.Tables[0].Rows[i]["TextLength"].ToString());
        //            }
        //            switch (int.Parse(ds.Tables[0].Rows[i]["IdQuestionType"].ToString()))
        //            {
        //                case 2:
        //                case 3:
        //                case 4:
        //                case 7:
        //                    preguntaResponse.optionQuestion = ds.Tables[0].Rows[i]["questionAdditional"].ToString();
        //                    break;
        //            }


        //            if (preguntaResponse.idQuestionType == 1)
        //            {

        //                renglon = renglon + "<tr><td>" + preguntaResponse.numericalOrder + "</td><td>" + preguntaResponse.question + "</td><td></td></tr>";
        //                renglon = renglon + "<tr><td></td><td colspan='2'>Escriba su respuesta: _______________________________________ </td></tr>";


        //            }
        //            if (ds.Tables[0].Rows[i]["idQuestionType"].ToString() == "2")
        //            {
        //                preguntaResponse.optionQuestion = "|" + preguntaResponse.optionQuestion;
        //                var opcion = preguntaResponse.optionQuestion.Split('|');
        //                var opcionesAnidadas = "";
        //                for (int j = 1; j < opcion.Count() - 1; j++)
        //                {
        //                    opcionesAnidadas = opcionesAnidadas + opcion[j] + ": _______________________________________<br/>";
        //                }

        //                renglon = renglon + "<tr><td>" + preguntaResponse.numericalOrder + "</td><td>" + preguntaResponse.question + "</td><td></td></tr>";

        //                renglon = renglon + "<tr><td></td><td colspan='2'>" + opcionesAnidadas + "</td></tr>";



        //            }
        //            if (ds.Tables[0].Rows[i]["idQuestionType"].ToString() == "3")
        //            {
        //                preguntaResponse.optionQuestion = "|" + preguntaResponse.optionQuestion;
        //                var opcion = preguntaResponse.optionQuestion.Split('|');
        //                var opcionesAnidadas = "";
        //                for (int j = 1; j < opcion.Count() - 1; j++)
        //                {
        //                    opcionesAnidadas = opcionesAnidadas + "___ " + opcion[j] + "<br/>";
        //                }


        //                renglon = renglon + "<tr><td>" + preguntaResponse.numericalOrder + "</td><td>" + preguntaResponse.question + "</td><td></td></tr>";
        //                renglon = renglon + "<tr><td></td><td colspan='2'>" + "Marque con X la(s) opcion(es) correcta(s): <br/>" + opcionesAnidadas + "</td></tr>";


        //            }
        //            if (ds.Tables[0].Rows[i]["idQuestionType"].ToString() == "4")
        //            {
        //                preguntaResponse.optionQuestion = "|" + preguntaResponse.optionQuestion;
        //                var opcion = preguntaResponse.optionQuestion.Split('|');
        //                var opcionesAnidadas = "";
        //                for (int j = 1; j < opcion.Count() - 1; j++)
        //                {
        //                    opcionesAnidadas = opcionesAnidadas + opcion[j] + "<br/>";
        //                }
        //                renglon = renglon + "<tr><td>" + preguntaResponse.numericalOrder + "</td><td>" + preguntaResponse.question + "</td><td></td></tr>";
        //                renglon = renglon + "<tr><td></td><td colspan='2'>" + "Subraye la opción correcta: <br/>" + opcionesAnidadas + "</td></tr>";
        //            }

        //            if (ds.Tables[0].Rows[i]["idQuestionType"].ToString() == "6")
        //            {
        //                renglon = renglon + "<tr><td>" + preguntaResponse.numericalOrder + "</td><td>" + preguntaResponse.question + "</td><td></td></tr>";
        //                renglon = renglon + "<tr><td></td><td colspan='2'>Escriba su respuesta: _______________________________________ </td></tr>";
        //            }
        //            if (ds.Tables[0].Rows[i]["idQuestionType"].ToString() == "7")
        //            {
        //                preguntaResponse.optionQuestion = "|" + preguntaResponse.optionQuestion;
        //                var opcion = preguntaResponse.optionQuestion.Split('|');
        //                var opcionesAnidadas = "";
        //                for (int j = 1; j < opcion.Count() - 1; j++)
        //                {
        //                    opcionesAnidadas = opcionesAnidadas + opcion[j] + ": _______________________________________<br/>";
        //                }

        //                renglon = renglon + "<tr><td>" + preguntaResponse.numericalOrder + "</td><td>" + preguntaResponse.question + "</td><td></td></tr>";

        //                renglon = renglon + "<tr><td></td><td colspan='2'>" + opcionesAnidadas + "</td></tr>";

        //            }
        //            if (ds.Tables[0].Rows[i]["idQuestionType"].ToString() == "8")
        //            {
        //                renglon = renglon + "<tr><td>" + preguntaResponse.numericalOrder + "</td><td>" + preguntaResponse.question + "</td><td></td></tr>";
        //                renglon = renglon + "<tr><td></td><td colspan='2'>Marca tu respuesta con una X: Si___    No___</td></tr>";
        //            }
        //            if (ds.Tables[0].Rows[i]["idQuestionType"].ToString() == "9")
        //            {
        //                renglon = renglon + "<tr><td>" + preguntaResponse.numericalOrder + "</td><td>" + preguntaResponse.question + "</td><td></td></tr>";
        //                renglon = renglon + "<tr><td></td><td colspan='2'>Escriba su respuesta(dd/mm/aaaa): _______________________________________ </td></tr>";
        //            }

        //            if (ds.Tables[0].Rows[i]["idQuestionType"].ToString() == "10")
        //            {
        //                renglon = renglon + "<tr><td>" + preguntaResponse.numericalOrder + "</td><td>" + preguntaResponse.question + "</td><td></td></tr>";
        //                renglon = renglon + "<tr><td></td><td colspan='2'>Escriba su respuesta(hh:mm): _______________________________________ </td></tr>";
        //            }

        //            if (ds.Tables[0].Rows[i]["idQuestionType"].ToString() == "11")
        //            {
        //                renglon = renglon + "<tr><td>" + preguntaResponse.numericalOrder + "</td><td>" + preguntaResponse.question + "</td><td></td></tr>";
        //                renglon = renglon + "<tr><td></td><td colspan='2'>Escriba su respuesta(dd/mm/aaaa hh:mm:ss): _______________________________________ </td></tr>";
        //            }
        //            if (ds.Tables[0].Rows[i]["idQuestionType"].ToString() == "12")
        //            {
        //                renglon = renglon + "<tr><td>" + preguntaResponse.numericalOrder + "</td><td>" + preguntaResponse.question + "</td><td></td></tr>";
        //                renglon = renglon + "<tr><td></td><td colspan='2'>Escriba su respuesta: Latitud _________________ Longitud _________________ </td></tr>";

        //            }
        //            renglon = renglon + "<tr><td>.</td><td colspan='2'> </td></tr>";

        //            listaPreguntas.Add(preguntaResponse);
        //        }

        //        renglon = renglon + "</table>";
        //    }

        //    return datos + renglon;
        //}

    }
}