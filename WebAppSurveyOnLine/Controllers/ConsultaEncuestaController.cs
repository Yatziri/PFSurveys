using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Survey.Business;

namespace WebAppSurveyOnLine.Controllers
{
    public class ConsultaEncuestaController : Controller
    {
        // GET: ConsultaEncuesta
        public ActionResult Index()
        {
            List<Survey.Entidades.Consulta> viewModel = Survey.Business.Encuesta.Consulta();


            if (Session["dataTable"] != null)
            {
                DataTable dt = (DataTable)Session["dataTable"];
                ViewBag.columnas = dt.Columns;
                ViewBag.columnasTxt = ((List<string>)Session["dataTable1"]);
                ViewBag.rows = dt.Rows;
                //dt.Columns[].ColumnName;
            }

            return View(viewModel);
        }

        public JsonResult ConsultaRespuestas(int id)
        {
            DataSet ds = Survey.Business.Encuesta.ConsultaEncuestaRespuestas(id);

            List<string> nombreCampos = new List<string>();
            var campo = "";
            for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
            {
                campo = ds.Tables[1].Rows[i]["Question"].ToString();
                if (ds.Tables[1].Rows[i]["idQuestionType"].ToString() == "1")
                {


                    nombreCampos.Add(campo);
                }
                if (ds.Tables[1].Rows[i]["idQuestionType"].ToString() == "2")
                {


                    var textoseparado = ds.Tables[1].Rows[i]["questionAdditional"].ToString().Split('|');

                    for (int j = 0; j < textoseparado.Count() - 1; j++)
                    {
                        nombreCampos.Add("(" + textoseparado[j] + ")");
                    }

                }

                if (ds.Tables[1].Rows[i]["idQuestionType"].ToString() == "3")
                {

                    var textoseparado = ds.Tables[1].Rows[i]["questionAdditional"].ToString().Split('|');

                    for (int j = 0; j < textoseparado.Count() - 1; j++)
                    {
                        nombreCampos.Add("(" + textoseparado[j] + ")");
                    }
                }
                if (ds.Tables[1].Rows[i]["idQuestionType"].ToString() == "4")
                {


                    nombreCampos.Add(campo);
                }
                if (ds.Tables[1].Rows[i]["idQuestionType"].ToString() == "5")
                {


                    nombreCampos.Add(campo);
                }
                if (ds.Tables[1].Rows[i]["idQuestionType"].ToString() == "6")
                {


                    nombreCampos.Add(campo);
                }
                if (ds.Tables[1].Rows[i]["idQuestionType"].ToString() == "7")
                {
                    var textoseparado = ds.Tables[1].Rows[i]["questionAdditional"].ToString().Split('|');

                    for (int j = 0; j < textoseparado.Count() - 1; j++)
                    {
                        nombreCampos.Add("(" + textoseparado[j] + ")");
                    }
                }

                if (ds.Tables[1].Rows[i]["idQuestionType"].ToString() == "8")
                {


                    nombreCampos.Add(campo);
                }
                if (ds.Tables[1].Rows[i]["idQuestionType"].ToString() == "9")
                {


                    nombreCampos.Add(campo);
                }
                if (ds.Tables[1].Rows[i]["idQuestionType"].ToString() == "10")
                {


                    nombreCampos.Add(campo);
                }

                if (ds.Tables[1].Rows[i]["idQuestionType"].ToString() == "11")
                {


                    nombreCampos.Add(campo);
                }
                if (ds.Tables[1].Rows[i]["idQuestionType"].ToString() == "12")
                {
                    // var campoLatitud = "FIELD_" + ds.Tables[1].Rows[i]["id"].ToString() + "_lat";
                    //var latitud = ds.Tables[0].Rows[0][campoLatitud].ToString();
                    nombreCampos.Add("Latitud");
                    //var campoLongitud = "FIELD_" + ds.Tables[1].Rows[i]["id"].ToString() + "_lon";
                    nombreCampos.Add("Longitud");


                }

            }


            List<DataRow> list = ds.Tables[0].AsEnumerable().ToList();
            Session["dataTable"] = ds.Tables[0];
            Session["dataTable1"] = nombreCampos;
            //list[].Table.Columns
            return Json(new { Status = "Ok" }, JsonRequestBehavior.AllowGet);
            //return Json(new { Status = "Ok", Result = listaPreguntas }, JsonRequestBehavior.AllowGet);
        }

    }
}