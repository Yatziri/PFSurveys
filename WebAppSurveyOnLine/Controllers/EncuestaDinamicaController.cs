using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Survey.Entidades;
using System.Data;
using System.IO;
using System.Configuration;
using System.Text;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.tool.xml;
using iTextSharp.text.html.simpleparser;
namespace WebAppSurveyOnLine.Controllers
{
    public class EncuestaDinamicaController : Controller
    {
        // GET: EncuestaDinamica
        Survey.Entidades.Ctl_Surveys viewModelSurvey = new Survey.Entidades.Ctl_Surveys();
        public ActionResult Index()
        {
            List<Ctl_Survey_Questions> listaPreguntas = new List<Ctl_Survey_Questions>();

            //Survey.Entidades.Ctl_Surveys viewModelSurvey = (Survey.Entidades.Ctl_Surveys)Session["viewModel"];


            if (HttpContext.Request.Params.Get("idTipoActividad") != null)
            {
                var idActividad = int.Parse(HttpContext.Request.Params.Get("idTipoActividad"));
                var idUsuario = int.Parse(HttpContext.Request.Params.Get("idUsuario"));
                var task = HttpContext.Request.Params.Get("tarea");

                Session["idActividad"] = idActividad;
                Session["task"] = task;
                Session["idUsuario"] = idUsuario;
            
                ViewBag.idActividad = idActividad;
                ViewBag.idUsuario = idUsuario;
                ViewBag.task = task;

                

                listaPreguntas = GetPreguntasLoad(idActividad, idUsuario, task);
            }

            ViewBag.Estatus =bool.Parse( Session["estatus"].ToString());
            return View(listaPreguntas);
            
        }

        public List<Ctl_Survey_Questions> GetPreguntasLoad(int idActividad, int idUsuario, string task)
        {
            var campo = "";
            Ctl_Survey_Questions pregunta = new Ctl_Survey_Questions();

            Survey.Entidades.Ctl_Surveys viewModel = new Ctl_Surveys();


            DataSet ds = Survey.Business.Encuesta.SelectPreguntasRespuestas(idActividad, idUsuario, task);
            if (ds.Tables[1].Rows.Count <= 0)
            {
                DataSet dsUsuario = Survey.Business.Encuesta.InsertUsuario(idActividad, idUsuario, task);
                 ds = Survey.Business.Encuesta.SelectPreguntasRespuestas(idActividad, idUsuario, task);
            }

            Session["idSurvey"] = ds.Tables[0].Rows[0]["idSurver"].ToString();
            Session["estatus"] = ds.Tables[1].Rows[0]["Status"].ToString()=="0"?true:false;
            List<Ctl_Survey_Questions> listaPreguntas = new List<Ctl_Survey_Questions>();
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                var preguntaResponse = new Ctl_Survey_Questions();
                preguntaResponse.numericalOrder = int.Parse(ds.Tables[0].Rows[i]["NumericalOrder"].ToString());
                preguntaResponse.question = ds.Tables[0].Rows[i]["Question"].ToString();
                preguntaResponse.questionType = ds.Tables[0].Rows[i]["questionType"].ToString();
                preguntaResponse.idQuestionType = int.Parse(ds.Tables[0].Rows[i]["IdQuestionType"].ToString());
                preguntaResponse.id = int.Parse(ds.Tables[0].Rows[i]["Id"].ToString());
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
                }

                
                    if (ds.Tables[0].Rows[i]["idQuestionType"].ToString() == "1")
                    {
                        campo = "FIELD_" + ds.Tables[0].Rows[i]["id"].ToString();
                        preguntaResponse.respuesta = ds.Tables[1].Rows[0][campo].ToString();
                    }
                    if (ds.Tables[0].Rows[i]["idQuestionType"].ToString() == "2")
                    {
                        var textoseparado = preguntaResponse.optionQuestion.Split('|');
                        var n = 1;
                        var camposAnidados = "";
                        for (int j = 0; j < textoseparado.Count() - 1; j++)
                        {
                            campo = "FIELD_" + ds.Tables[0].Rows[i]["id"].ToString() + "_" + n.ToString();
                            camposAnidados = camposAnidados + ds.Tables[1].Rows[0][campo].ToString() + '|';

                            n = n + 1;
                        }
                        preguntaResponse.respuesta = camposAnidados;


                    }
                    if (ds.Tables[0].Rows[i]["idQuestionType"].ToString() == "3")
                    {
                        var textoseparado = preguntaResponse.optionQuestion.Split('|');
                        var n = 1;
                        var camposAnidados = "";
                        for (int j = 0; j < textoseparado.Count() - 1; j++)
                        {
                            campo = "FIELD_" + ds.Tables[0].Rows[i]["id"].ToString() + "_" + n.ToString();
                            var valorCampo = ds.Tables[1].Rows[0][campo].ToString() == "" ? "0" : ds.Tables[1].Rows[0][campo].ToString();
                            camposAnidados = camposAnidados + valorCampo + "|";

                            n = n + 1;
                        }
                        preguntaResponse.respuesta = camposAnidados;


                    }
                    if (ds.Tables[0].Rows[i]["idQuestionType"].ToString() == "4")
                    {
                        campo = "FIELD_" + ds.Tables[0].Rows[i]["id"].ToString();
                        preguntaResponse.respuesta = ds.Tables[1].Rows[0][campo].ToString()==""?"0": ds.Tables[1].Rows[0][campo].ToString();
                    }
                if (ds.Tables[0].Rows[i]["idQuestionType"].ToString() == "5")
                {
                    campo = "FIELD_" + ds.Tables[0].Rows[i]["id"].ToString();
                    preguntaResponse.respuesta = ds.Tables[1].Rows[0][campo].ToString();
                }
                if (ds.Tables[0].Rows[i]["idQuestionType"].ToString() == "6")
                    {
                        campo = "FIELD_" + ds.Tables[0].Rows[i]["id"].ToString();
                        preguntaResponse.respuesta = ds.Tables[1].Rows[0][campo].ToString();
                    }
                    if (ds.Tables[0].Rows[i]["idQuestionType"].ToString() == "7")
                    {
                        var textoseparado = preguntaResponse.optionQuestion.Split('|');
                        var n = 1;
                        var camposAnidados = "";
                        for (int j = 0; j < textoseparado.Count() - 1; j++)
                        {
                            campo = "FIELD_" + ds.Tables[0].Rows[i]["id"].ToString() + "_" + n.ToString();
                            camposAnidados = camposAnidados + ds.Tables[1].Rows[0][campo].ToString() + '|';

                            n = n + 1;
                        }
                        preguntaResponse.respuesta = camposAnidados;


                    }
                    if (ds.Tables[0].Rows[i]["idQuestionType"].ToString() == "8")
                    {
                        campo = "FIELD_" + ds.Tables[0].Rows[i]["id"].ToString();
                        preguntaResponse.respuesta = ds.Tables[1].Rows[0][campo].ToString();
                    }
                    if (ds.Tables[0].Rows[i]["idQuestionType"].ToString() == "9")
                    {
                        campo = "FIELD_" + ds.Tables[0].Rows[i]["id"].ToString();
                    preguntaResponse.respuesta = ds.Tables[1].Rows[0][campo].ToString()==""? ds.Tables[1].Rows[0][campo].ToString(): DateTime.Parse(ds.Tables[1].Rows[0][campo].ToString()).ToString("yyyy-MM-dd");
                    }

                    if (ds.Tables[0].Rows[i]["idQuestionType"].ToString() == "10")
                    {
                        campo = "FIELD_" + ds.Tables[0].Rows[i]["id"].ToString();
                        preguntaResponse.respuesta = ds.Tables[1].Rows[0][campo].ToString();
                    }

                    if (ds.Tables[0].Rows[i]["idQuestionType"].ToString() == "11")
                    {
                        campo = "FIELD_" + ds.Tables[0].Rows[i]["id"].ToString();
                        preguntaResponse.respuesta = ds.Tables[1].Rows[0][campo].ToString()==""? ds.Tables[1].Rows[0][campo].ToString(): DateTime.Parse(ds.Tables[1].Rows[0][campo].ToString()).ToString("yyyy-MM-dd hh:mm:ss");
                    }
                    if (ds.Tables[0].Rows[i]["idQuestionType"].ToString() == "12")
                    {
                        var campoLatitud = "FIELD_" + ds.Tables[0].Rows[i]["id"].ToString() + "_lat";
                        var latitud = ds.Tables[1].Rows[0][campoLatitud].ToString();

                        var campoLongitud = "FIELD_" + ds.Tables[0].Rows[i]["id"].ToString() + "_lon";

                        preguntaResponse.respuesta = latitud + "|" + ds.Tables[1].Rows[0][campoLongitud].ToString();

                    }
                

                listaPreguntas.Add(preguntaResponse);
            }



            return listaPreguntas;
        }

        public JsonResult GetPreguntas(int idSurvey)
        {
            Ctl_Survey_Questions pregunta = new Ctl_Survey_Questions();

            Survey.Entidades.Ctl_Surveys viewModel = new Ctl_Surveys();
            
            viewModel.idSurvey = idSurvey;
            DataSet ds = Survey.Business.Encuesta.SelectPreguntas(viewModel);

            List<Ctl_Survey_Questions> listaPreguntas = new List<Ctl_Survey_Questions>();
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                var preguntaResponse = new Ctl_Survey_Questions();
                preguntaResponse.numericalOrder = int.Parse(ds.Tables[0].Rows[i]["NumericalOrder"].ToString());
                preguntaResponse.question = ds.Tables[0].Rows[i]["Question"].ToString();
                preguntaResponse.questionType = ds.Tables[0].Rows[i]["questionType"].ToString();
                preguntaResponse.idQuestionType =int.Parse( ds.Tables[0].Rows[i]["IdQuestionType"].ToString());
                preguntaResponse.id= int.Parse(ds.Tables[0].Rows[i]["Id"].ToString());
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
                }
                
                listaPreguntas.Add(preguntaResponse);
            }

            return Json(new { Status = "Ok", Result = listaPreguntas }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GuardarRespuesta(string idTipoActividad, string idUsuario, string tarea,string  campo,string valor,string tipoDato)
        {
            try
            {
                if (tipoDato == "fecha")
                {
                    valor = DateTime.Parse(valor).ToString("yyyy-MM-dd");
                }
                //if(tipoDato == "fechaTiempo")
                //{
                //    valor = DateTime.Parse(valor).ToString("yyyy-MM-dd hh:mm:ss");
                //}

                idTipoActividad =Session["idActividad"].ToString();
                tarea= Session["task"].ToString();
                idUsuario=Session["idUsuario"].ToString();
                DataSet ds = Survey.Business.Encuesta.InsertUpdatePregunta(idTipoActividad, idUsuario, tarea, campo, valor, tipoDato);
                return Json(new { Status = "Ok" }, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(new { Status = "false" }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult GuardarUsuario(int idTipoActividad, int idUsuario, string tarea)
        {
            try
            {
                
                DataSet ds = Survey.Business.Encuesta.InsertUsuario(idTipoActividad, idUsuario, tarea);
                return Json(new { Status = "Ok" }, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(new { Status = "false" }, JsonRequestBehavior.AllowGet);
            }
        }


        public ActionResult CerrarEncuesta()
        {
          
            var idActividad = int.Parse(Session["idActividad"].ToString());
            var task = Session["task"].ToString();
            var idUsuario = int.Parse(Session["idUsuario"].ToString());



            bool bandera = GetPreguntasCerrarCuestionario(idActividad, idUsuario, task);


            if (bandera)
            {
                bandera = Survey.Business.Encuesta.CerrarEncuestaConRespuestas(idActividad,  task,idUsuario);
                if (bandera)
                {
                    return Json(new { Status = "Ok", idTipoActividad = idActividad, idUsuario = idUsuario, tarea = task }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { Status = "False" }, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json(new { Status = "False" }, JsonRequestBehavior.AllowGet);
            }

        }

        public bool GetPreguntas(int idActividad, int idUsuario, string task)
        {
            var campo = "";
            Ctl_Survey_Questions pregunta = new Ctl_Survey_Questions();

            Survey.Entidades.Ctl_Surveys viewModel = new Ctl_Surveys();


            DataSet ds = Survey.Business.Encuesta.SelectPreguntasRespuestas(idActividad, idUsuario, task);

            bool bandera = true;

            List<Ctl_Survey_Questions> listaPreguntas = new List<Ctl_Survey_Questions>();
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                var preguntaResponse = new Ctl_Survey_Questions();
                preguntaResponse.numericalOrder = int.Parse(ds.Tables[0].Rows[i]["NumericalOrder"].ToString());
                preguntaResponse.question = ds.Tables[0].Rows[i]["Question"].ToString();
                preguntaResponse.questionType = ds.Tables[0].Rows[i]["questionType"].ToString();
                preguntaResponse.idQuestionType = int.Parse(ds.Tables[0].Rows[i]["IdQuestionType"].ToString());
                preguntaResponse.id = int.Parse(ds.Tables[0].Rows[i]["Id"].ToString());
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
                }


                if (ds.Tables[0].Rows[i]["idQuestionType"].ToString() == "1")
                {
                    campo = "FIELD_" + ds.Tables[0].Rows[i]["id"].ToString();
                    preguntaResponse.respuesta = ds.Tables[1].Rows[0][campo].ToString();
                }
                if (ds.Tables[0].Rows[i]["idQuestionType"].ToString() == "2")
                {
                    var textoseparado = preguntaResponse.optionQuestion.Split('|');
                    var n = 1;
                    var camposAnidados = "";
                    for (int j = 0; j < textoseparado.Count() - 1; j++)
                    {
                        campo = "FIELD_" + ds.Tables[0].Rows[i]["id"].ToString() + "_" + n.ToString();
                        camposAnidados = camposAnidados + ds.Tables[1].Rows[0][campo].ToString() + '|';

                        n = n + 1;
                    }
                    preguntaResponse.respuesta = camposAnidados;


                }
                if (ds.Tables[0].Rows[i]["idQuestionType"].ToString() == "3")
                {
                    var textoseparado = preguntaResponse.optionQuestion.Split('|');
                    var n = 1;
                    var camposAnidados = "";
                    for (int j = 0; j < textoseparado.Count() - 1; j++)
                    {
                        campo = "FIELD_" + ds.Tables[0].Rows[i]["id"].ToString() + "_" + n.ToString();
                        var valorCampo = ds.Tables[1].Rows[0][campo].ToString() == "" ? "0" : ds.Tables[1].Rows[0][campo].ToString();
                        camposAnidados = camposAnidados + valorCampo + "|";

                        n = n + 1;
                    }
                    preguntaResponse.respuesta = camposAnidados;


                }
                if (ds.Tables[0].Rows[i]["idQuestionType"].ToString() == "4")
                {
                    campo = "FIELD_" + ds.Tables[0].Rows[i]["id"].ToString();
                    preguntaResponse.respuesta = ds.Tables[1].Rows[0][campo].ToString() == "" ? "0" : ds.Tables[1].Rows[0][campo].ToString();
                }

                if (ds.Tables[0].Rows[i]["idQuestionType"].ToString() == "6")
                {
                    campo = "FIELD_" + ds.Tables[0].Rows[i]["id"].ToString();
                    preguntaResponse.respuesta = ds.Tables[1].Rows[0][campo].ToString();
                }
                if (ds.Tables[0].Rows[i]["idQuestionType"].ToString() == "7")
                {
                    var textoseparado = preguntaResponse.optionQuestion.Split('|');
                    var n = 1;
                    var camposAnidados = "";
                    for (int j = 0; j < textoseparado.Count() - 1; j++)
                    {
                        campo = "FIELD_" + ds.Tables[0].Rows[i]["id"].ToString() + "_" + n.ToString();
                        camposAnidados = camposAnidados + ds.Tables[1].Rows[0][campo].ToString() + '|';

                        n = n + 1;
                    }
                    preguntaResponse.respuesta = camposAnidados;


                }
                if (ds.Tables[0].Rows[i]["idQuestionType"].ToString() == "8")
                {
                    campo = "FIELD_" + ds.Tables[0].Rows[i]["id"].ToString();
                    preguntaResponse.respuesta = ds.Tables[1].Rows[0][campo].ToString();
                }
                if (ds.Tables[0].Rows[i]["idQuestionType"].ToString() == "9")
                {
                    campo = "FIELD_" + ds.Tables[0].Rows[i]["id"].ToString();
                    preguntaResponse.respuesta = ds.Tables[1].Rows[0][campo].ToString() == "" ? ds.Tables[1].Rows[0][campo].ToString() : DateTime.Parse(ds.Tables[1].Rows[0][campo].ToString()).ToString("yyyy-MM-dd");
                }

                if (ds.Tables[0].Rows[i]["idQuestionType"].ToString() == "10")
                {
                    campo = "FIELD_" + ds.Tables[0].Rows[i]["id"].ToString();
                    preguntaResponse.respuesta = ds.Tables[1].Rows[0][campo].ToString();
                }

                if (ds.Tables[0].Rows[i]["idQuestionType"].ToString() == "11")
                {
                    campo = "FIELD_" + ds.Tables[0].Rows[i]["id"].ToString();
                    preguntaResponse.respuesta = ds.Tables[1].Rows[0][campo].ToString() == "" ? ds.Tables[1].Rows[0][campo].ToString() : DateTime.Parse(ds.Tables[1].Rows[0][campo].ToString()).ToString("dd/MM/yyyy hh:mm:ss");
                }
                if (ds.Tables[0].Rows[i]["idQuestionType"].ToString() == "12")
                {
                    var campoLatitud = "FIELD_" + ds.Tables[0].Rows[i]["id"].ToString() + "_lat";
                    var latitud = ds.Tables[1].Rows[0][campoLatitud].ToString();

                    var campoLongitud = "FIELD_" + ds.Tables[0].Rows[i]["id"].ToString() + "_lon";

                    preguntaResponse.respuesta = latitud + "|" + ds.Tables[1].Rows[0][campoLongitud].ToString();

                }

                if(preguntaResponse.respuesta =="")
                {
                    bandera = false;
                    break;
                }
                listaPreguntas.Add(preguntaResponse);
            }



            return bandera;
        }


        public bool GetPreguntasCerrarCuestionario(int idActividad, int idUsuario, string task)
        {
            var campo = "";
            Ctl_Survey_Questions pregunta = new Ctl_Survey_Questions();

            Survey.Entidades.Ctl_Surveys viewModel = new Ctl_Surveys();


            DataSet ds = Survey.Business.Encuesta.SelectPreguntasRespuestas(idActividad, idUsuario, task);

            bool bandera = true;

            List<Ctl_Survey_Questions> listaPreguntas = new List<Ctl_Survey_Questions>();
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                var preguntaResponse = new Ctl_Survey_Questions();
                preguntaResponse.numericalOrder = int.Parse(ds.Tables[0].Rows[i]["NumericalOrder"].ToString());
                preguntaResponse.question = ds.Tables[0].Rows[i]["Question"].ToString();
                preguntaResponse.questionType = ds.Tables[0].Rows[i]["questionType"].ToString();
                preguntaResponse.idQuestionType = int.Parse(ds.Tables[0].Rows[i]["IdQuestionType"].ToString());
                preguntaResponse.id = int.Parse(ds.Tables[0].Rows[i]["Id"].ToString());
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
                }


                if (ds.Tables[0].Rows[i]["idQuestionType"].ToString() == "1")
                {
                    campo = "FIELD_" + ds.Tables[0].Rows[i]["id"].ToString();
                    preguntaResponse.respuesta = ds.Tables[1].Rows[0][campo].ToString();
                }
                if (ds.Tables[0].Rows[i]["idQuestionType"].ToString() == "2")
                {
                    var textoseparado = preguntaResponse.optionQuestion.Split('|');
                    var n = 1;
                    var camposAnidados = "";
                    for (int j = 0; j < textoseparado.Count() - 1; j++)
                    {
                        campo = "FIELD_" + ds.Tables[0].Rows[i]["id"].ToString() + "_" + n.ToString();
                        var valorCampo= ds.Tables[1].Rows[0][campo].ToString();

                        if (valorCampo  == "")
                        {
                            camposAnidados = "";
                            break;
                        }
                        else
                        {
                            camposAnidados = camposAnidados + valorCampo + "|";
                        }

                        n = n + 1;
                    }
                    preguntaResponse.respuesta = camposAnidados;


                }
                if (ds.Tables[0].Rows[i]["idQuestionType"].ToString() == "3")
                {
                    var textoseparado = preguntaResponse.optionQuestion.Split('|');
                    var n = 1;
                    var camposAnidados = "";
                    for (int j = 0; j < textoseparado.Count() - 1; j++)
                    {
                        campo = "FIELD_" + ds.Tables[0].Rows[i]["id"].ToString() + "_" + n.ToString();
                        var valorCampo = ds.Tables[1].Rows[0][campo].ToString();
                        camposAnidados = camposAnidados + valorCampo ;

                        n = n + 1;
                    }
                    preguntaResponse.respuesta = camposAnidados.Contains("1") ? camposAnidados : "";


                }
                if (ds.Tables[0].Rows[i]["idQuestionType"].ToString() == "4")
                {
                    campo = "FIELD_" + ds.Tables[0].Rows[i]["id"].ToString();

                    var valorCampo = ds.Tables[1].Rows[0][campo].ToString();

                    preguntaResponse.respuesta = valorCampo  == "" ? "" : valorCampo;
                }
                if (ds.Tables[0].Rows[i]["idQuestionType"].ToString() == "5")
                {
                    campo = "FIELD_" + ds.Tables[0].Rows[i]["id"].ToString();
                    preguntaResponse.respuesta = ds.Tables[1].Rows[0][campo].ToString();
                }

                if (ds.Tables[0].Rows[i]["idQuestionType"].ToString() == "6")
                {
                    campo = "FIELD_" + ds.Tables[0].Rows[i]["id"].ToString();
                    preguntaResponse.respuesta = ds.Tables[1].Rows[0][campo].ToString();
                }
                if (ds.Tables[0].Rows[i]["idQuestionType"].ToString() == "7")
                {
                    var textoseparado = preguntaResponse.optionQuestion.Split('|');
                    var n = 1;
                    var camposAnidados = "";
                    for (int j = 0; j < textoseparado.Count() - 1; j++)
                    {
                        campo = "FIELD_" + ds.Tables[0].Rows[i]["id"].ToString() + "_" + n.ToString();
                        var valorCampo= ds.Tables[1].Rows[0][campo].ToString();

                        if (valorCampo  == "")
                        {
                            camposAnidados = "";
                            break;
                        }
                        else
                        {
                            camposAnidados = camposAnidados + valorCampo + "|";
                        }

                        n = n + 1;
                    }
                    preguntaResponse.respuesta = camposAnidados;


                }
                if (ds.Tables[0].Rows[i]["idQuestionType"].ToString() == "8")
                {
                    campo = "FIELD_" + ds.Tables[0].Rows[i]["id"].ToString();
                    var valorCampo = ds.Tables[1].Rows[0][campo].ToString();
                    if ( valorCampo.Contains('0') || valorCampo.Contains('0'))
                    {
                        preguntaResponse.respuesta = valorCampo;
                    }
                    else
                    {
                        preguntaResponse.respuesta = "";
                    }
                    

                    
                }
                if (ds.Tables[0].Rows[i]["idQuestionType"].ToString() == "9")
                {
                    campo = "FIELD_" + ds.Tables[0].Rows[i]["id"].ToString();
                    preguntaResponse.respuesta = ds.Tables[1].Rows[0][campo].ToString() == "" ? ds.Tables[1].Rows[0][campo].ToString() : DateTime.Parse(ds.Tables[1].Rows[0][campo].ToString()).ToString("yyyy-MM-dd");
                }

                if (ds.Tables[0].Rows[i]["idQuestionType"].ToString() == "10")
                {
                    campo = "FIELD_" + ds.Tables[0].Rows[i]["id"].ToString();
                    preguntaResponse.respuesta = ds.Tables[1].Rows[0][campo].ToString();
                }

                if (ds.Tables[0].Rows[i]["idQuestionType"].ToString() == "11")
                {
                    campo = "FIELD_" + ds.Tables[0].Rows[i]["id"].ToString();
                    preguntaResponse.respuesta = ds.Tables[1].Rows[0][campo].ToString() == "" ? ds.Tables[1].Rows[0][campo].ToString() : DateTime.Parse(ds.Tables[1].Rows[0][campo].ToString()).ToString("dd/MM/yyyy hh:mm:ss");
                }
                if (ds.Tables[0].Rows[i]["idQuestionType"].ToString() == "12")
                {
                    var campoLatitud = "FIELD_" + ds.Tables[0].Rows[i]["id"].ToString() + "_lat";
                    var latitud = ds.Tables[1].Rows[0][campoLatitud].ToString();

                    var campoLongitud = "FIELD_" + ds.Tables[0].Rows[i]["id"].ToString() + "_lon";
                    var longitud= ds.Tables[1].Rows[0][campoLongitud].ToString();

                    if (latitud == "" || longitud=="" )
                    {
                        preguntaResponse.respuesta = "";
                    }
                    else
                    {
                        preguntaResponse.respuesta = latitud + "|" + ds.Tables[1].Rows[0][campoLongitud].ToString();
                    }

                }

                if (preguntaResponse.respuesta == "")
                {
                    bandera = false;
                    break;
                }
                listaPreguntas.Add(preguntaResponse);
            }



            return bandera;
        }


        [HttpPost]
        public ActionResult UploadFiles()
        {
            // Checking no of files injected in Request object  
            if (Request.Files.Count > 0)
            {
                try
                {
                    //  Get all files from Request object  
                    HttpFileCollectionBase files = Request.Files;
                    string id_Question = Request["id_value"].ToString();
                    string idTipoActividad = Request["idTipoActividad"].ToString();
                    string idUsuario = Request["idUsuario"].ToString();
                    string tarea = Request["tarea"].ToString();
                    string campo = Request["campo"].ToString();
                    string valor = Request["valor"].ToString();
                    string tipoDato = Request["tipoDato"].ToString();

                    for (int i = 0; i < files.Count; i++)
                    {
                        //string path = AppDomain.CurrentDomain.BaseDirectory + "Uploads/";  
                        //string filename = Path.GetFileName(Request.Files[i].FileName);  

                        HttpPostedFileBase file = files[i];
                        string fname;
                        string extension;
                        // Checking for Internet Explorer  
                        if (Request.Browser.Browser.ToUpper() == "IE" || Request.Browser.Browser.ToUpper() == "INTERNETEXPLORER")
                        {
                            string[] testfiles = file.FileName.Split(new char[] { '\\' });
                            fname = testfiles[testfiles.Length - 1];
                        }
                        else
                        {
                            fname = file.FileName;
                            extension = Path.GetExtension(fname);
                            fname = id_Question+"_" + Session["idUsuario"].ToString() + extension;
                        }

                        
                        string idSurvey = Session["idSurvey"].ToString();
                        string idActivity = Session["idActividad"].ToString();
                        string ruta = ConfigurationManager.AppSettings["ExpedienteSurvey"] + "CTL_ANSWERS_AAAA_" + idSurvey + "_" + idActivity;
                        fname = ruta +@"\\"+ fname;

                        valor = fname;
                        file.SaveAs(fname);

                        idTipoActividad = Session["idActividad"].ToString();
                        tarea = Session["task"].ToString();
                        idUsuario = Session["idUsuario"].ToString();
                        DataSet ds = Survey.Business.Encuesta.InsertUpdatePregunta(idTipoActividad, idUsuario, tarea, campo, valor, tipoDato);
                        
                    }
                    // Returns message that successfully uploaded  
                    return Json("¡El archivo ha subido correctamente!");
                }
                catch (Exception ex)
                {
                    return Json("Se produjo un error: " + ex.Message);
                }
            }
            else
            {
                return Json("No hay archivo seleccionado.");
            }
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult VistaPreliminar(string GridHtml)
        {
            try
            {
              int idActividad=int.Parse(Session["idActividad"].ToString()) ;
              string task=  Session["task"].ToString();
               int idUsuario= int.Parse(Session["idUsuario"].ToString()) ;

                string HTMLContent = Survey.Business.Encuesta.GetPreguntasPDF(idActividad,task,idUsuario);


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


        [HttpPost]
        [ValidateInput(false)]
        public ActionResult VistaPreliminarPreguntasRespuestas(string GridHtml)
        {
            try
            {
                int idActividad = int.Parse(Session["idActividad"].ToString());
                string task = Session["task"].ToString();
                int idUsuario = int.Parse(Session["idUsuario"].ToString());

                string HTMLContent = Survey.Business.Encuesta.GetPreguntasRespuestasPDF(idActividad, task, idUsuario);


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
    }
}