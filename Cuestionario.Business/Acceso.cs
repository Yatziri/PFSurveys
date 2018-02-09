using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;


namespace Survey.Business
{
	public class Acceso
	{
		public static bool ValidaUsuario(string usuario,string contrasena)
		{
            try
            {
                SqlParameter[] parametros = new SqlParameter[2];

                parametros[0] = new SqlParameter("@usuario", usuario);
                parametros[1] = new SqlParameter("@contrasena", contrasena);

                DataSet ds = Survey.DataAccess.DataBaseHelper.ExecuteQueryWithDataset("uSp_VerificaAcceso", parametros);
                bool bandera = ds.Tables[0].Rows[0]["bandera"].ToString() == "True" ? true : false;

                return bandera;
            }
            catch
            {
                return false;
            }
		}

       
    }
}
