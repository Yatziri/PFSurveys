using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.Collections;

namespace Survey.DataAccess
{
    public class DataBaseHelper
    {
        public static DataSet ExecuteQueryWithDataset(string spName, SqlParameter[] parameters)
        {
            try
            {
                return ExecuteQuery(spName, parameters);
            }
            catch ( Exception ex)
            {
                Console.WriteLine("Error " + ex.Message + " has occurred: " + ex.Message);
                throw;
            }
        }

        public static DataSet ExecuteQueryWithDataset(string spName)
        {
            try
            {
                return ExecuteQuery(spName);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error " + ex.Message + " has occurred: " + ex.Message);
                throw;
            }
        }

        private static DataSet ExecuteQuery(string spName, SqlParameter[] parameters)
        {
            DataSet ds = new DataSet();
            try
            {
                SqlConnection conn = Conexion();
                using (SqlCommand cmd = new SqlCommand(spName, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddRange(parameters);
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {

                        da.Fill(ds);

                    }
                }
                return ds;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error " + ex + " has occurred: " + ex.Message);
                throw;
            }
        }

        private static DataSet ExecuteQuery(string spName)
        {
            DataSet ds = new DataSet();
            try
            {
                SqlConnection conn = Conexion();
                using (SqlCommand cmd = new SqlCommand(spName, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {

                        da.Fill(ds);

                    }
                }
                return ds;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error " + ex + " has occurred: " + ex.Message);
                throw;
            }
        }

        private static SqlConnection Conexion()
        {
            //string constr = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["SQLConnString"].ConnectionString;

            return conn;
        }

    }
}
