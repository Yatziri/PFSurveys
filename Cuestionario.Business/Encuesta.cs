using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Survey.Entidades;

namespace Survey.Business
{
    [Serializable]
    public class Encuesta
    {

        public static List<Entidades.Consulta> Consulta()
        {
           List<Entidades.Consulta> listaEncuestas = new List<Entidades.Consulta>();

            DataSet ds = Survey.DataAccess.DataBaseHelper.ExecuteQueryWithDataset("uSp_ConsultaEncuesta");

            if (ds.Tables[0].Rows.Count > 0)
            {

                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    var encuesta = new Entidades.Consulta();
                    if (ds.Tables[0].Rows[i]["NombreEncuesta"].ToString() != "")
                    {
						encuesta.id = int.Parse(ds.Tables[0].Rows[i]["id"].ToString());
						encuesta.idSurvey = int.Parse(ds.Tables[0].Rows[i]["idSurvey"].ToString());
                        encuesta.Usuario = int.Parse(ds.Tables[0].Rows[i]["usuario"].ToString()==""?"0": ds.Tables[0].Rows[i]["usuario"].ToString());
                        encuesta.Actividad = int.Parse(ds.Tables[0].Rows[i]["actividad"].ToString());
                        encuesta.Unidad = int.Parse(ds.Tables[0].Rows[i]["unidad"].ToString());
                        encuesta.NombreEncuesta = ds.Tables[0].Rows[i]["NombreEncuesta"].ToString();
                        encuesta.TableName = ds.Tables[0].Rows[i]["NombreEncuesta"].ToString();
                        listaEncuestas.Add(encuesta);
                    }


                }
            }

            return listaEncuestas;
        }

		public static DataSet ConsultaEncuestaRespuestas(int id)
		{
            SqlParameter[] parametros = new SqlParameter[1];

            parametros[0] = new SqlParameter("@id", id);

            DataSet ds = Survey.DataAccess.DataBaseHelper.ExecuteQueryWithDataset("uSp_ConsultaUsuarioEncuesta",parametros);

			

			return ds;
		}

		public static bool CrearDirectorio(string ruta)
        {
            try
            {
                if (Directory.Exists(ruta))
                {
                    //Console.WriteLine("That path exists already.");
                    return true;
                }

                // Try to create the directory.
                DirectoryInfo di = Directory.CreateDirectory(ruta);

                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }
        public static DataSet CrearEncuesta(Ctl_Surveys viewModel)
        {
            SqlParameter[] parametros = new SqlParameter[3];

            parametros[0] = new SqlParameter("@name", viewModel.name);
            parametros[1] = new SqlParameter("@idActivity", viewModel.idActivity);
            parametros[2] = new SqlParameter("@idUnit", viewModel.idUnit);

            DataSet ds = Survey.DataAccess.DataBaseHelper.ExecuteQueryWithDataset("usp_CrearEncuesta", parametros);
            bool bandera = ds.Tables[0].Rows[0]["flag"].ToString() == "1" ? true : false;

            return ds;
        }

        public static DataSet SelectPreguntasRespuestas(int idActividad, int idUsuario, string task)
        {
            SqlParameter[] parametros = new SqlParameter[3];

            parametros[0] = new SqlParameter("@idActividad", idActividad);
            parametros[1] = new SqlParameter("@idUsuario", idUsuario);
            parametros[2] = new SqlParameter("@task", task);


            DataSet ds = Survey.DataAccess.DataBaseHelper.ExecuteQueryWithDataset("usp_SelectPreguntasRespuestas", parametros);
            //  bool bandera = ds.Tables[0].Rows[0]["flag"].ToString() == "true" ? true : false;

            return ds;
        }
        public static DataSet SelectPreguntas(Ctl_Surveys viewModel)
        {
            SqlParameter[] parametros = new SqlParameter[1];

            parametros[0] = new SqlParameter("@idSurvey", viewModel.idSurvey);


            DataSet ds = Survey.DataAccess.DataBaseHelper.ExecuteQueryWithDataset("usp_SelectPreguntas", parametros);
            //  bool bandera = ds.Tables[0].Rows[0]["flag"].ToString() == "true" ? true : false;

            return ds;
        }

        public static DataSet InsertUpdatePregunta(string idTipoActividad, string idUsuario, string tarea, string campo, string valor,string tipoDato)
        {
            SqlParameter[] parametros = new SqlParameter[6];

            parametros[0] = new SqlParameter("@idTipoActividad", idTipoActividad);
            parametros[1] = new SqlParameter("@idUsuario", idUsuario);
            parametros[2] = new SqlParameter("@tarea", tarea);
            parametros[3] = new SqlParameter("@campo", campo);
            parametros[4] = new SqlParameter("@valor", valor);
            parametros[5] = new SqlParameter("@tipoDato", tipoDato);


            DataSet ds = Survey.DataAccess.DataBaseHelper.ExecuteQueryWithDataset("usp_InsertUpdatePregunta", parametros);
            //  bool bandera = ds.Tables[0].Rows[0]["flag"].ToString() == "true" ? true : false;

            return ds;
        }

        public static DataSet InsertUsuario(int idTipoActividad, int idUsuario, string  tarea)
        {
            SqlParameter[] parametros = new SqlParameter[3];

            parametros[0] = new SqlParameter("@idTipoActividad", idTipoActividad);
            parametros[1] = new SqlParameter("@idUsuario", idUsuario);
            parametros[2] = new SqlParameter("@tarea", tarea);
         

            DataSet ds = Survey.DataAccess.DataBaseHelper.ExecuteQueryWithDataset("usp_InsertUsuario", parametros);
            //  bool bandera = ds.Tables[0].Rows[0]["flag"].ToString() == "true" ? true : false;

            return ds;
        }

        public static Ctl_Surveys ExisteCuestionario(Ctl_Surveys viewModel)
        {
			try
			{
				SqlParameter[] parametros = new SqlParameter[2];

				parametros[0] = new SqlParameter("@idActivity", viewModel.idActivity);
				parametros[1] = new SqlParameter("@idUnit", viewModel.idUnit);

				DataSet ds = Survey.DataAccess.DataBaseHelper.ExecuteQueryWithDataset("usp_ExisteCuestionario", parametros);

				if (ds.Tables[0].Rows.Count > 0)
				{
					viewModel.name = ds.Tables[0].Rows[0]["name"].ToString();
					viewModel.status = int.Parse(ds.Tables[0].Rows[0]["status"].ToString());
					viewModel.idSurvey = int.Parse(ds.Tables[0].Rows[0]["Id"].ToString());
				}



				return viewModel;
			}
			catch(Exception ex)
			{
				throw(ex);
			}
        }

        public static DataSet BajarPregunta(int idSurvey, int idNumericalOrder)
        {
            SqlParameter[] parametros = new SqlParameter[2];

            parametros[0] = new SqlParameter("@idSurvey", idSurvey);
            parametros[1] = new SqlParameter("@idNumericalOrder", idNumericalOrder);



            DataSet ds = Survey.DataAccess.DataBaseHelper.ExecuteQueryWithDataset("usp_BajarPregunta", parametros);

            //if (ds.Tables[0].Rows.Count > 0)
            //{
            //    viewModel.name = ds.Tables[0].Rows[0]["name"].ToString();
            //    viewModel.status = int.Parse(ds.Tables[0].Rows[0]["status"].ToString());
            //}


            return ds;
        }

        public static DataSet SubirPregunta(int idSurvey, int idNumericalOrder)
        {
            SqlParameter[] parametros = new SqlParameter[2];

            parametros[0] = new SqlParameter("@idSurvey", idSurvey);
            parametros[1] = new SqlParameter("@idNumericalOrder", idNumericalOrder);



            DataSet ds = Survey.DataAccess.DataBaseHelper.ExecuteQueryWithDataset("usp_SubirPregunta", parametros);

            //if (ds.Tables[0].Rows.Count > 0)
            //{
            //    viewModel.name = ds.Tables[0].Rows[0]["name"].ToString();
            //    viewModel.status = int.Parse(ds.Tables[0].Rows[0]["status"].ToString());
            //}


            return ds;
        }

        public static DataSet EliminarPregunta(int idSurvey, int idNumericalOrder)
		{
			SqlParameter[] parametros = new SqlParameter[2];

			parametros[0] = new SqlParameter("@idSurvey", idSurvey);
			parametros[1] = new SqlParameter("@idNumericalOrder", idNumericalOrder);
			


			DataSet ds = Survey.DataAccess.DataBaseHelper.ExecuteQueryWithDataset("usp_EliminarPreguntas", parametros);

			//if (ds.Tables[0].Rows.Count > 0)
			//{
			//    viewModel.name = ds.Tables[0].Rows[0]["name"].ToString();
			//    viewModel.status = int.Parse(ds.Tables[0].Rows[0]["status"].ToString());
			//}


			return ds;
		}

        public static DataSet EditarPregunta(int idSurvey, int idNumericalOrder)
        {
            SqlParameter[] parametros = new SqlParameter[2];

            parametros[0] = new SqlParameter("@idSurvey", idSurvey);
            parametros[1] = new SqlParameter("@idNumericalOrder", idNumericalOrder);



            DataSet ds = Survey.DataAccess.DataBaseHelper.ExecuteQueryWithDataset("usp_EditarPregunta", parametros);

            //if (ds.Tables[0].Rows.Count > 0)
            //{
            //    viewModel.name = ds.Tables[0].Rows[0]["name"].ToString();
            //    viewModel.status = int.Parse(ds.Tables[0].Rows[0]["status"].ToString());
            //}


            return ds;
        }



        public static DataSet InsertarPregunta(Ctl_Survey_Questions viewModelQuestion)
        {
            SqlParameter[] parametros = new SqlParameter[6];

            parametros[0] = new SqlParameter("@idSurvey", viewModelQuestion.idSurvey);
            parametros[1] = new SqlParameter("@numericalOrder", viewModelQuestion.numericalOrder);
            parametros[2] = new SqlParameter("@question", viewModelQuestion.question);
            parametros[3] = new SqlParameter("@idQuestionType", viewModelQuestion.idQuestionType);
            parametros[4] = new SqlParameter("@textLength", viewModelQuestion.textLength);
            parametros[5] = new SqlParameter("@optionQuestion", viewModelQuestion.optionQuestion);


            DataSet ds = Survey.DataAccess.DataBaseHelper.ExecuteQueryWithDataset("usp_InsertUpdateQuestion", parametros);

            //if (ds.Tables[0].Rows.Count > 0)
            //{
            //    viewModel.name = ds.Tables[0].Rows[0]["name"].ToString();
            //    viewModel.status = int.Parse(ds.Tables[0].Rows[0]["status"].ToString());
            //}


            return ds;
        }

        public static bool CerrarEncuesta(int idSurvey)
        {
            try
            {
                SqlParameter[] parametros = new SqlParameter[1];

                parametros[0] = new SqlParameter("@idSurvey", idSurvey);

                DataSet ds = Survey.DataAccess.DataBaseHelper.ExecuteQueryWithDataset("uSp_CerrarEncuesta", parametros);

                bool bandera =ds.Tables[0].Rows[0]["bandera"].ToString()=="1"?true:false;

                return bandera;
            }catch(Exception ex)
            {
                return false;
            }
        }

        public static bool CerrarEncuestaConRespuestas(int idActividad, string task, int idUsuario)
        {
            try
            {
                SqlParameter[] parametros = new SqlParameter[3];

                parametros[0] = new SqlParameter("@idActividad", idActividad);
                parametros[1] = new SqlParameter("@idUsuario", idUsuario);
                parametros[2] = new SqlParameter("@task", task);
                

                DataSet ds = Survey.DataAccess.DataBaseHelper.ExecuteQueryWithDataset("uSp_CerrarEncuestaConRespuestas", parametros);

                bool bandera = ds.Tables[0].Rows[0]["bandera"].ToString() == "1" ? true : false;

                return bandera;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public static string GetPreguntasPDF(int idActividad, string task, int idUsuario)
        {

            DataSet ds = SelectPreguntasRespuestas(idActividad,idUsuario,task);

            
            var datos = "<table>";
            datos = datos + "<tr><td></td><td>Encuesta: " + ds.Tables[0].Rows[0]["Name"].ToString() + "</td><td></td></tr>";
            datos = datos + "<tr><td>idEncuesta: " + ds.Tables[0].Rows[0]["IdSurver"].ToString() + "</td><td></td><td></td></tr>";
            datos = datos + "<tr><td>idActividad: " + ds.Tables[0].Rows[0]["idActivity"].ToString()  + "</td><td></td><td></td></tr>";
            datos = datos + "<tr><td>idUnidad: " + ds.Tables[0].Rows[0]["IdUnit"].ToString() + "</td><td></td><td></td></tr>";
            datos = datos + "<tr><td></td><td></td><td></td></tr>";
            datos = datos + "<tr><td colspan='3'>_____________________________________________________________________________________</td></tr>";
            datos = datos + "<tr><td colspan='3'>Preguntas:</td></tr>";
            datos = datos + "</table><br/><br/><br/>";
            var renglon = TablaPreguntas(ds);

            return datos + renglon;
        }

        public static string GetPreguntasRespuestasPDF(int idActividad, string task, int idUsuario)
        {

            DataSet ds = SelectPreguntasRespuestas(idActividad, idUsuario, task);


            var datos = "<table>";
            datos = datos + "<tr><td></td><td>Encuesta: " + ds.Tables[0].Rows[0]["Name"].ToString() + "</td><td></td></tr>";
            datos = datos + "<tr><td>idEncuesta: " + ds.Tables[0].Rows[0]["IdSurver"].ToString() + "</td><td></td><td></td></tr>";
            datos = datos + "<tr><td>idActividad: " + ds.Tables[0].Rows[0]["idActivity"].ToString() + "</td><td></td><td></td></tr>";
            datos = datos + "<tr><td>idUnidad: " + ds.Tables[0].Rows[0]["IdUnit"].ToString() + "</td><td></td><td></td></tr>";
            datos = datos + "<tr><td></td><td></td><td></td></tr>";
            datos = datos + "<tr><td colspan='3'>_____________________________________________________________________________________</td></tr>";
            datos = datos + "<tr><td colspan='3'>Preguntas:</td></tr>";
            datos = datos + "</table><br/><br/><br/>";

            var renglon = TablaPreguntasRespuetas(ds);

            return datos + renglon;
        }

        public static string GetPreguntasPDF(Survey.Entidades.Ctl_Surveys viewModelSurvey)
        {

            Ctl_Survey_Questions pregunta = new Ctl_Survey_Questions();

            Survey.Entidades.Ctl_Surveys viewModel = new Ctl_Surveys();


            DataSet ds = Survey.Business.Encuesta.SelectPreguntas(viewModelSurvey);


            
            var datos = "<table>";
            datos = datos + "<tr><td></td><td>Encuesta: " + viewModelSurvey.name + "</td><td></td></tr>";
            datos = datos + "<tr><td>idEncuesta: " + viewModelSurvey.idSurvey + "</td><td></td><td></td></tr>";
            datos = datos + "<tr><td>idActividad: " + viewModelSurvey.idActivity + "</td><td></td><td></td></tr>";
            datos = datos + "<tr><td>idUnidad: " + viewModelSurvey.idUnit + "</td><td></td><td></td></tr>";
            datos = datos + "<tr><td></td><td></td><td></td></tr>";
            datos = datos + "<tr><td colspan='3'>_____________________________________________________________________________________</td></tr>";
            datos = datos + "<tr><td colspan='3'>Preguntas:</td></tr>";
            datos = datos + "</table><br/><br/><br/>";
            var  renglon = TablaPreguntas(ds);



            return datos + renglon;
        }

        private static string TablaPreguntas(DataSet ds)
        {
            try
            {
                string renglon = "";
                List<Ctl_Survey_Questions> listaPreguntas = new List<Ctl_Survey_Questions>();
                if (ds.Tables[0].Rows.Count > 0)
                {
                    renglon = "<table>";
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


                        if (preguntaResponse.idQuestionType == 1)
                        {

                            renglon = renglon + "<tr><td>" + preguntaResponse.numericalOrder + "</td><td>" + preguntaResponse.question + "</td><td></td></tr>";
                            renglon = renglon + "<tr><td></td><td colspan='2'>Escriba su respuesta: _______________________________________ </td></tr>";


                        }
                        if (ds.Tables[0].Rows[i]["idQuestionType"].ToString() == "2")
                        {
                            preguntaResponse.optionQuestion = "|" + preguntaResponse.optionQuestion;
                            var opcion = preguntaResponse.optionQuestion.Split('|');
                            var opcionesAnidadas = "";
                            for (int j = 1; j < opcion.Count() - 1; j++)
                            {
                                opcionesAnidadas = opcionesAnidadas + opcion[j] + ": _______________________________________<br/>";
                            }

                            renglon = renglon + "<tr><td>" + preguntaResponse.numericalOrder + "</td><td>" + preguntaResponse.question + "</td><td></td></tr>";

                            renglon = renglon + "<tr><td></td><td colspan='2'>" + opcionesAnidadas + "</td></tr>";



                        }
                        if (ds.Tables[0].Rows[i]["idQuestionType"].ToString() == "3")
                        {
                            preguntaResponse.optionQuestion = "|" + preguntaResponse.optionQuestion;
                            var opcion = preguntaResponse.optionQuestion.Split('|');
                            var opcionesAnidadas = "";
                            for (int j = 1; j < opcion.Count() - 1; j++)
                            {
                                opcionesAnidadas = opcionesAnidadas + "___ " + opcion[j] + "<br/>";
                            }


                            renglon = renglon + "<tr><td>" + preguntaResponse.numericalOrder + "</td><td>" + preguntaResponse.question + "</td><td></td></tr>";
                            renglon = renglon + "<tr><td></td><td colspan='2'>" + "Marque con X la(s) opcion(es) correcta(s): <br/>" + opcionesAnidadas + "</td></tr>";


                        }
                        if (ds.Tables[0].Rows[i]["idQuestionType"].ToString() == "4")
                        {
                            preguntaResponse.optionQuestion = "|" + preguntaResponse.optionQuestion;
                            var opcion = preguntaResponse.optionQuestion.Split('|');
                            var opcionesAnidadas = "";
                            for (int j = 1; j < opcion.Count() - 1; j++)
                            {
                                opcionesAnidadas = opcionesAnidadas + opcion[j] + "<br/>";
                            }
                            renglon = renglon + "<tr><td>" + preguntaResponse.numericalOrder + "</td><td>" + preguntaResponse.question + "</td><td></td></tr>";
                            renglon = renglon + "<tr><td></td><td colspan='2'>" + "Subraye la opción correcta: <br/>" + opcionesAnidadas + "</td></tr>";
                        }

                        if (ds.Tables[0].Rows[i]["idQuestionType"].ToString() == "6")
                        {
                            renglon = renglon + "<tr><td>" + preguntaResponse.numericalOrder + "</td><td>" + preguntaResponse.question + "</td><td></td></tr>";
                            renglon = renglon + "<tr><td></td><td colspan='2'>Escriba su respuesta: _______________________________________ </td></tr>";
                        }
                        if (ds.Tables[0].Rows[i]["idQuestionType"].ToString() == "7")
                        {
                            preguntaResponse.optionQuestion = "|" + preguntaResponse.optionQuestion;
                            var opcion = preguntaResponse.optionQuestion.Split('|');
                            var opcionesAnidadas = "";
                            for (int j = 1; j < opcion.Count() - 1; j++)
                            {
                                opcionesAnidadas = opcionesAnidadas + opcion[j] + ": _______________________________________<br/>";
                            }

                            renglon = renglon + "<tr><td>" + preguntaResponse.numericalOrder + "</td><td>" + preguntaResponse.question + "</td><td></td></tr>";

                            renglon = renglon + "<tr><td></td><td colspan='2'>" + opcionesAnidadas + "</td></tr>";

                        }
                        if (ds.Tables[0].Rows[i]["idQuestionType"].ToString() == "8")
                        {
                            renglon = renglon + "<tr><td>" + preguntaResponse.numericalOrder + "</td><td>" + preguntaResponse.question + "</td><td></td></tr>";
                            renglon = renglon + "<tr><td></td><td colspan='2'>Marca tu respuesta con una X: Si___    No___</td></tr>";
                        }
                        if (ds.Tables[0].Rows[i]["idQuestionType"].ToString() == "9")
                        {
                            renglon = renglon + "<tr><td>" + preguntaResponse.numericalOrder + "</td><td>" + preguntaResponse.question + "</td><td></td></tr>";
                            renglon = renglon + "<tr><td></td><td colspan='2'>Escriba su respuesta(dd/mm/aaaa): _______________________________________ </td></tr>";
                        }

                        if (ds.Tables[0].Rows[i]["idQuestionType"].ToString() == "10")
                        {
                            renglon = renglon + "<tr><td>" + preguntaResponse.numericalOrder + "</td><td>" + preguntaResponse.question + "</td><td></td></tr>";
                            renglon = renglon + "<tr><td></td><td colspan='2'>Escriba su respuesta(hh:mm): _______________________________________ </td></tr>";
                        }

                        if (ds.Tables[0].Rows[i]["idQuestionType"].ToString() == "11")
                        {
                            renglon = renglon + "<tr><td>" + preguntaResponse.numericalOrder + "</td><td>" + preguntaResponse.question + "</td><td></td></tr>";
                            renglon = renglon + "<tr><td></td><td colspan='2'>Escriba su respuesta(dd/mm/aaaa hh:mm:ss): _______________________________________ </td></tr>";
                        }
                        if (ds.Tables[0].Rows[i]["idQuestionType"].ToString() == "12")
                        {
                            renglon = renglon + "<tr><td>" + preguntaResponse.numericalOrder + "</td><td>" + preguntaResponse.question + "</td><td></td></tr>";
                            renglon = renglon + "<tr><td></td><td colspan='2'>Escriba su respuesta: Latitud _________________ Longitud _________________ </td></tr>";

                        }
                        renglon = renglon + "<tr><td>.</td><td colspan='2'> </td></tr>";

                        listaPreguntas.Add(preguntaResponse);
                    }

                    renglon = renglon + "</table>";
                }
                return renglon;
            }
            catch
            {
                return "";
            }
        }

        private static string TablaPreguntasRespuetas(DataSet ds)
        {
            try
            {
                string renglon = "";
                List<Ctl_Survey_Questions> listaPreguntas = new List<Ctl_Survey_Questions>();
                if (ds.Tables[0].Rows.Count > 0)
                {
                    renglon = "<table>";
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


                        if (preguntaResponse.idQuestionType == 1)
                        {

                            renglon = renglon + "<tr><td>" + preguntaResponse.numericalOrder + "</td><td>" + preguntaResponse.question + "</td><td></td></tr>";
                            renglon = renglon + "<tr><td></td><td colspan='2'>Respuesta: "+ ds.Tables[1].Rows[0]["Field_"+preguntaResponse.id].ToString() +"</td></tr>";


                        }
                        if (ds.Tables[0].Rows[i]["idQuestionType"].ToString() == "2")
                        {
                            preguntaResponse.optionQuestion = "|" + preguntaResponse.optionQuestion;
                            var opcion = preguntaResponse.optionQuestion.Split('|');
                            var opcionesAnidadas = "";
                            for (int j = 1; j < opcion.Count() - 1; j++)
                            {
                                opcionesAnidadas = opcionesAnidadas + opcion[j] + ": "+ ds.Tables[1].Rows[0]["Field_" + preguntaResponse.id +"_"+ j.ToString()].ToString() + " <br/>";
                            }

                            renglon = renglon + "<tr><td>" + preguntaResponse.numericalOrder + "</td><td>" + preguntaResponse.question + "</td><td></td></tr>";

                            renglon = renglon + "<tr><td></td><td colspan='2'>" + opcionesAnidadas + "</td></tr>";



                        }
                        if (ds.Tables[0].Rows[i]["idQuestionType"].ToString() == "3")
                        {
                            preguntaResponse.optionQuestion = "|" + preguntaResponse.optionQuestion;
                            var opcion = preguntaResponse.optionQuestion.Split('|');
                            var opcionesAnidadas = "";
                            var respuesta = "";
                            for (int j = 1; j < opcion.Count() - 1; j++)
                            {
                                respuesta = ds.Tables[1].Rows[0]["Field_" + preguntaResponse.id + "_" + j.ToString()].ToString() == "1" ? " X " : " _ ";
                                opcionesAnidadas = opcionesAnidadas + respuesta  + opcion[j] + "<br/>";
                            }


                            renglon = renglon + "<tr><td>" + preguntaResponse.numericalOrder + "</td><td>" + preguntaResponse.question + "</td><td></td></tr>";
                            renglon = renglon + "<tr><td></td><td colspan='2'>" + "Marque con X la(s) opcion(es) correcta(s): <br/>" + opcionesAnidadas + "</td></tr>";


                        }
                        if (ds.Tables[0].Rows[i]["idQuestionType"].ToString() == "4")
                        {
                            preguntaResponse.optionQuestion = "|" + preguntaResponse.optionQuestion;
                            var opcion = preguntaResponse.optionQuestion.Split('|');
                            var opcionesAnidadas = "";
                            var respuesta = "";
                            for (int j = 1; j < opcion.Count() - 1; j++)
                            {
                                respuesta =int.Parse( ds.Tables[1].Rows[0]["Field_" + preguntaResponse.id].ToString()) == j ? "<u>"+opcion[j]+"</u>" : opcion[j];
                                opcionesAnidadas = opcionesAnidadas + respuesta + "<br/>";
                            }
                            renglon = renglon + "<tr><td>" + preguntaResponse.numericalOrder + "</td><td>" + preguntaResponse.question + "</td><td></td></tr>";
                            renglon = renglon + "<tr><td></td><td colspan='2'>" + "Subraye la opción correcta: <br/>" + opcionesAnidadas + "</td></tr>";
                        }

                        if (ds.Tables[0].Rows[i]["idQuestionType"].ToString() == "5")
                        {
                            renglon = renglon + "<tr><td>" + preguntaResponse.numericalOrder + "</td><td>" + preguntaResponse.question + "</td><td></td></tr>";
                            renglon = renglon + "<tr><td></td><td colspan='2'>Respuesta: " + ds.Tables[1].Rows[0]["Field_" + preguntaResponse.id].ToString()+ " </td></tr>";
                        }

                        if (ds.Tables[0].Rows[i]["idQuestionType"].ToString() == "6")
                        {
                            renglon = renglon + "<tr><td>" + preguntaResponse.numericalOrder + "</td><td>" + preguntaResponse.question + "</td><td></td></tr>";
                            renglon = renglon + "<tr><td></td><td colspan='2'>Respuesta: " + ds.Tables[1].Rows[0]["Field_" + preguntaResponse.id].ToString()+ "</td></tr>";
                        }
                        if (ds.Tables[0].Rows[i]["idQuestionType"].ToString() == "7")
                        {
                            preguntaResponse.optionQuestion = "|" + preguntaResponse.optionQuestion;
                            var opcion = preguntaResponse.optionQuestion.Split('|');
                            var opcionesAnidadas = "";
                            for (int j = 1; j < opcion.Count() - 1; j++)
                            {
                                opcionesAnidadas = opcionesAnidadas + opcion[j] + ": "+ ds.Tables[1].Rows[0]["Field_" + preguntaResponse.id + "_" + j.ToString()].ToString() + "<br/>";
                            }

                            renglon = renglon + "<tr><td>" + preguntaResponse.numericalOrder + "</td><td>" + preguntaResponse.question + "</td><td></td></tr>";

                            renglon = renglon + "<tr><td></td><td colspan='2'>" + opcionesAnidadas + "</td></tr>";

                        }
                        if (ds.Tables[0].Rows[i]["idQuestionType"].ToString() == "8")
                        {
                            renglon = renglon + "<tr><td>" + preguntaResponse.numericalOrder + "</td><td>" + preguntaResponse.question + "</td><td></td></tr>";
                            string respuesta= ds.Tables[1].Rows[0]["Field_" + preguntaResponse.id].ToString() == "1"?" Resuesta Si: X No   ":"  Si:    No: X";
                            renglon = renglon + "<tr><td></td><td colspan='2'> "+ respuesta +" </td></tr>";
                        }
                        if (ds.Tables[0].Rows[i]["idQuestionType"].ToString() == "9")
                        {
                            renglon = renglon + "<tr><td>" + preguntaResponse.numericalOrder + "</td><td>" + preguntaResponse.question + "</td><td></td></tr>";
                            var respuesta = "";
                            if(ds.Tables[1].Rows[0]["Field_" + preguntaResponse.id] != null)
                            {
                                respuesta = DateTime.Parse(ds.Tables[1].Rows[0]["Field_" + preguntaResponse.id].ToString()).ToString("dd/MM/yyyy");
                            }
                            
                            renglon = renglon + "<tr><td></td><td colspan='2'> Respuesta :"+ respuesta  + " </td></tr>";
                        }

                        if (ds.Tables[0].Rows[i]["idQuestionType"].ToString() == "10")
                        {
                            renglon = renglon + "<tr><td>" + preguntaResponse.numericalOrder + "</td><td>" + preguntaResponse.question + "</td><td></td></tr>";
                            renglon = renglon + "<tr><td></td><td colspan='2'> Respuesta :"+ ds.Tables[1].Rows[0]["Field_" + preguntaResponse.id].ToString() + " </td></tr>";
                        }

                        if (ds.Tables[0].Rows[i]["idQuestionType"].ToString() == "11")
                        {
                            renglon = renglon + "<tr><td>" + preguntaResponse.numericalOrder + "</td><td>" + preguntaResponse.question + "</td><td></td></tr>";

                            var respuesta = "";
                            if (ds.Tables[1].Rows[0]["Field_" + preguntaResponse.id] != null)
                            {
                                respuesta = DateTime.Parse(ds.Tables[1].Rows[0]["Field_" + preguntaResponse.id].ToString()).ToString("dd/MM/yyyy hh:mm:ss");
                            }

                            renglon = renglon + "<tr><td></td><td colspan='2'>  Respuesta :"+ respuesta+" </td></tr>";
                        }
                        if (ds.Tables[0].Rows[i]["idQuestionType"].ToString() == "12")
                        {
                            renglon = renglon + "<tr><td>" + preguntaResponse.numericalOrder + "</td><td>" + preguntaResponse.question + "</td><td></td></tr>";

                            var latitud = ds.Tables[1].Rows[0]["Field_" + preguntaResponse.id.ToString() + "_lat"].ToString();
                            var longitud = ds.Tables[1].Rows[0]["Field_" + preguntaResponse.id.ToString() + "_lon"].ToString();
                            renglon = renglon + "<tr><td></td><td colspan='2'> Latitud: "+latitud +"  Longitud: "+ longitud +" </td></tr>";

                        }
                        renglon = renglon + "<tr><td>.</td><td colspan='2'> </td></tr>";

                        listaPreguntas.Add(preguntaResponse);
                    }

                    renglon = renglon + "</table>";
                }
                return renglon;
            }
            catch(Exception ex)
            {
                throw (ex);
                //return "";
            }
        }
    }
}
