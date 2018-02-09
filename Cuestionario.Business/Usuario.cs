using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace Survey.Business
{
    [Serializable]
    public class Usuario
    {
        public static List<Survey.Entidades.Usuario> ConsultaListaUsuarios()
        {
            
            DataSet ds = Survey.DataAccess.DataBaseHelper.ExecuteQueryWithDataset("uSp_ConsultaUsuarios");

            List<Survey.Entidades.Usuario> listaUsuarios = new List<Entidades.Usuario>();
            Survey.Entidades.Usuario usuario;
            for(int i=0;i<ds.Tables[0].Rows.Count;i++)
            {
                usuario = new Entidades.Usuario();
                usuario.email = ds.Tables[0].Rows[i]["email"].ToString();
                usuario.id = ds.Tables[0].Rows[i]["id"].ToString();
                usuario.lastName = ds.Tables[0].Rows[i]["lastName"].ToString();
                usuario.name = ds.Tables[0].Rows[i]["name"].ToString();
                usuario.status = ds.Tables[0].Rows[i]["status"].ToString();
                usuario.surName = ds.Tables[0].Rows[i]["surName"].ToString();
                listaUsuarios.Add(usuario);
            }


            return listaUsuarios;
        }

        public static List<Survey.Entidades.Usuario> CrearUsuario(string email, string password, string name, string lastName, string surName)
        {
            SqlParameter[] parametros = new SqlParameter[5];
            parametros[0] = new SqlParameter("@email", email);
            parametros[1] = new SqlParameter("@password", password);
            parametros[2] = new SqlParameter("@name", name);
            parametros[3] = new SqlParameter("@lastName", lastName);
            parametros[4] = new SqlParameter("@surName", surName);

            DataSet ds = Survey.DataAccess.DataBaseHelper.ExecuteQueryWithDataset("uSp_CrearUsuario",parametros);

            List<Survey.Entidades.Usuario> listaUsuarios = new List<Entidades.Usuario>();
            Survey.Entidades.Usuario usuario;
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                usuario = new Entidades.Usuario();
                usuario.email = ds.Tables[0].Rows[i]["email"].ToString();
                usuario.id = ds.Tables[0].Rows[i]["id"].ToString();
                usuario.lastName = ds.Tables[0].Rows[i]["lastName"].ToString();
                usuario.name = ds.Tables[0].Rows[i]["name"].ToString();
                usuario.status = ds.Tables[0].Rows[i]["status"].ToString();
                usuario.surName = ds.Tables[0].Rows[i]["surName"].ToString();
                listaUsuarios.Add(usuario);
            }


            return listaUsuarios;
        }

        public static List<Survey.Entidades.Usuario> UpdateUsuario(string email, string password, string name, string lastName, string surName,int id)
        {
            SqlParameter[] parametros = new SqlParameter[6];
            parametros[0] = new SqlParameter("@email", email);
            parametros[1] = new SqlParameter("@password", password);
            parametros[2] = new SqlParameter("@name", name);
            parametros[3] = new SqlParameter("@lastName", lastName);
            parametros[4] = new SqlParameter("@surName", surName);
            parametros[5] = new SqlParameter("@id", id);
            DataSet ds = Survey.DataAccess.DataBaseHelper.ExecuteQueryWithDataset("uSp_UpdateUsuario", parametros);

            List<Survey.Entidades.Usuario> listaUsuarios = new List<Entidades.Usuario>();
            Survey.Entidades.Usuario usuario;
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                usuario = new Entidades.Usuario();
                usuario.email = ds.Tables[0].Rows[i]["email"].ToString();
                usuario.id = ds.Tables[0].Rows[i]["id"].ToString();
                usuario.lastName = ds.Tables[0].Rows[i]["lastName"].ToString();
                usuario.name = ds.Tables[0].Rows[i]["name"].ToString();
                usuario.status = ds.Tables[0].Rows[i]["status"].ToString();
                usuario.surName = ds.Tables[0].Rows[i]["surName"].ToString();
                listaUsuarios.Add(usuario);
            }


            return listaUsuarios;
        }


        public static List<Survey.Entidades.Usuario> EliminarUsuario( int id)
        {
            SqlParameter[] parametros = new SqlParameter[1];
            parametros[0] = new SqlParameter("@id", id);

            DataSet ds = Survey.DataAccess.DataBaseHelper.ExecuteQueryWithDataset("uSp_EliminarUsuario", parametros);

            List<Survey.Entidades.Usuario> listaUsuarios = new List<Entidades.Usuario>();
            Survey.Entidades.Usuario usuario;
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                usuario = new Entidades.Usuario();
                usuario.email = ds.Tables[0].Rows[i]["email"].ToString();
                usuario.id = ds.Tables[0].Rows[i]["id"].ToString();
                usuario.lastName = ds.Tables[0].Rows[i]["lastName"].ToString();
                usuario.name = ds.Tables[0].Rows[i]["name"].ToString();
                usuario.status = ds.Tables[0].Rows[i]["status"].ToString();
                usuario.surName = ds.Tables[0].Rows[i]["surName"].ToString();
                listaUsuarios.Add(usuario);
            }


            return listaUsuarios;
        }


        public static Survey.Entidades.Usuario EditarUsuario(int id)
		{
			SqlParameter[] parametros = new SqlParameter[1];
			parametros[0] = new SqlParameter("@id", id);
			 

			DataSet ds = Survey.DataAccess.DataBaseHelper.ExecuteQueryWithDataset("uSp_EditarUsuario", parametros);

			 
			Survey.Entidades.Usuario usuario;
			 
				usuario = new Entidades.Usuario();
				usuario.email = ds.Tables[0].Rows[0]["email"].ToString();
				usuario.id = ds.Tables[0].Rows[0]["id"].ToString();
				usuario.lastName = ds.Tables[0].Rows[0]["lastName"].ToString();
				usuario.name = ds.Tables[0].Rows[0]["name"].ToString();
				usuario.status = ds.Tables[0].Rows[0]["status"].ToString();
				usuario.surName = ds.Tables[0].Rows[0]["surName"].ToString();
				 
		


			return usuario;
		}
	}
}
