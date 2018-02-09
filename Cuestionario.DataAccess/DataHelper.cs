using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace Survey.DataAccess
{
    public abstract class DataHelper
    {
        #region "Declaración de Variables"

        protected string MServidor = "";
        protected string MBase = "";
        protected string MUsuario = "";
        protected string MPassword = "";
        protected string MCadenaConexion = "";
        protected IDbConnection MConexion;

        #endregion

        #region "Setters y Getters"

        // Nombre del equipo servidor de datos. 
        public string Servidor
        {
            get { return MServidor; }
            set { MServidor = value; }
        } // end Servidor

        // Nombre de la base de datos a utilizar.
        public string Base
        {
            get { return MBase; }
            set { MBase = value; }
        } // end Base

        // Nombre del Usuario de la BD. 
        public string Usuario
        {
            get { return MUsuario; }
            set { MUsuario = value; }
        } // end Usuario

        // Password del Usuario de la BD. 
        public string Password
        {
            get { return MPassword; }
            set { MPassword = value; }
        } // end Password

        // Cadena de conexión completa a la base.
        public abstract string CadenaConexion
        { get; set; }

        #endregion

        #region "Privadas"

        // Crea u obtiene un objeto para conectarse a la base de datos. 
        protected IDbConnection Conexion
        {
            get
            {
                // si aun no tiene asignada la cadena de conexion lo hace
                if (MConexion == null)
                    MConexion = CrearConexion(CadenaConexion);

                // si no esta abierta aun la conexion, lo abre
                if (MConexion.State != ConnectionState.Open)
                    MConexion.Open();

                // retorna la conexion en modo interfaz, para que se adapte a cualquier implementacion de los distintos fabricantes de motores de bases de datos
                return MConexion;
            } // end get
        } // end Conexion

        #endregion

        #region "Lecturas"

        // Obtiene un DataSet a partir de un Procedimiento Almacenado.
        public DataSet GetDataSet(string storedProcedure)
        {
            var mDataSet = new DataSet();
            CrearDataAdapter(storedProcedure).Fill(mDataSet);
            return mDataSet;
        } // end GetDataset


        //Obtiene un DataSet a partir de un Procedimiento Almacenado y sus parámetros. 
        public DataSet GetDataSet(string storedProcedure, params Object[] args)
        {
            var mDataSet = new DataSet();
            CrearDataAdapter(storedProcedure, args).Fill(mDataSet);
            return mDataSet;
        } // end GetDataset

        // Obtiene un DataSet a partir de un Query Sql.
        public DataSet GetDataSetSql(string comandoSql)
        {
            var mDataSet = new DataSet();
            CrearDataAdapterSql(comandoSql).Fill(mDataSet);
            return mDataSet;
        } // end GetDataSetSql

        // Obtiene un DataTable a partir de un Procedimiento Almacenado.
        public DataTable GetDataTable(string storedProcedure)
        { return GetDataSet(storedProcedure).Tables[0].Copy(); } // end GetDataTable


        //Obtiene un DataSet a partir de un Procedimiento Almacenado y sus parámetros. 
        public DataTable GetDataTable(string storedProcedure, params Object[] args)
        { return GetDataSet(storedProcedure, args).Tables[0].Copy(); } // end GetDataTable

        //Obtiene un DataTable a partir de un Query SQL
        public DataTable GetDataTableSql(string comandoSql)
        { return GetDataSetSql(comandoSql).Tables[0].Copy(); } // end GetDataTableSql

        // Obtiene un DataReader a partir de un Procedimiento Almacenado. 
        public IDataReader GetDataReader(string storedProcedure)
        {
            var com = Comando(storedProcedure);
            return com.ExecuteReader();
        } // end GetDataReader 


        // Obtiene un DataReader a partir de un Procedimiento Almacenado y sus parámetros. 
        public IDataReader GetDataReader(string storedProcedure, params object[] args)
        {
            var com = Comando(storedProcedure);
            CargarParametros(com, args);
            return com.ExecuteReader();
        } // end GetDataReader

        // Obtiene un DataReader a partir de un Procedimiento Almacenado. 
        public IDataReader GetDataReaderSql(string comandoSql)
        {
            var com = ComandoSql(comandoSql);
            return com.ExecuteReader();
        } // end GetDataReaderSql 

        // Obtiene un Valor Escalar a partir de un Procedimiento Almacenado. Solo funciona con SP's que tengan
        // definida variables de tipo output, para funciones escalares mas abajo se declara un metodo
        public object GetValorOutput(string storedProcedure)
        {
            // asignar el string sql al command
            var com = Comando(storedProcedure);
            // ejecutar el command
            com.ExecuteNonQuery();
            // declarar variable de retorno
            Object resp = null;

            // recorrer los parametros del SP
            foreach (IDbDataParameter par in com.Parameters)
                // si tiene parametros de tipo IO/Output retornar ese valor
                if (par.Direction == ParameterDirection.InputOutput || par.Direction == ParameterDirection.Output)
                    resp = par.Value;
            return resp;
        } // end GetValor


        // Obtiene un Valor a partir de un Procedimiento Almacenado, y sus parámetros. 
        public object GetValorOutput(string storedProcedure, params Object[] args)
        {
            // asignar el string sql al command
            var com = Comando(storedProcedure);
            // cargar los parametros del SP
            CargarParametros(com, args);
            // ejecutar el command
            com.ExecuteNonQuery();
            // declarar variable de retorno
            Object resp = null;

            // recorrer los parametros del SP
            foreach (IDbDataParameter par in com.Parameters)
                // si tiene parametros de tipo IO/Output retornar ese valor
                if (par.Direction == ParameterDirection.InputOutput || par.Direction == ParameterDirection.Output)
                    resp = par.Value;
            return resp;
        } // end GetValor

        // Obtiene un Valor Escalar a partir de un Procedimiento Almacenado. 
        public object GetValorOutputSql(string comadoSql)
        {
            // asignar el string sql al command
            var com = ComandoSql(comadoSql);
            // ejecutar el command
            com.ExecuteNonQuery();
            // declarar variable de retorno
            Object resp = null;

            // recorrer los parametros del Query (uso tipico envio de varias sentencias sql en el mismo command)
            foreach (IDbDataParameter par in com.Parameters)
                // si tiene parametros de tipo IO/Output retornar ese valor
                if (par.Direction == ParameterDirection.InputOutput || par.Direction == ParameterDirection.Output)
                    resp = par.Value;
            return resp;
        } // end GetValor


        // Obtiene un Valor de una funcion Escalar a partir de un Procedimiento Almacenado. 
        public object GetValorEscalar(string storedProcedure)
        {
            var com = Comando(storedProcedure);
            return com.ExecuteScalar();
        } // end GetValorEscalar

        /// Obtiene un Valor de una funcion Escalar a partir de un Procedimiento Almacenado, con Params de Entrada
        public Object GetValorEscalar(string storedProcedure, params object[] args)
        {
            var com = Comando(storedProcedure);
            CargarParametros(com, args);
            return com.ExecuteScalar();
        } // end GetValorEscalar

        // Obtiene un Valor de una funcion Escalar a partir de un Query SQL
        public object GetValorEscalarSql(string comandoSql)
        {
            var com = ComandoSql(comandoSql);
            return com.ExecuteScalar();
        } // end GetValorEscalarSql

        #endregion

        #region "Acciones"

        protected abstract IDbConnection CrearConexion(string cadena);
        protected abstract IDbCommand Comando(string storedProcedure);
        protected abstract IDbCommand ComandoSql(string comandoSql);
        protected abstract IDataAdapter CrearDataAdapter(string storedProcedure, params Object[] args);
        protected abstract IDataAdapter CrearDataAdapterSql(string comandoSql);
        protected abstract void CargarParametros(IDbCommand comando, Object[] args);

        // metodo sobrecargado para autenticarse contra el motor de BBDD
        public bool Autenticar()
        {
            if (Conexion.State != ConnectionState.Open)
                Conexion.Open();
            return true;
        }// end Autenticar

        // metodo sobrecargado para autenticarse contra el motor de BBDD
        public bool Autenticar(string vUsuario, string vPassword)
        {
            MUsuario = vUsuario;
            MPassword = vPassword;
            MConexion = CrearConexion(CadenaConexion);

            MConexion.Open();
            return true;
        }// end Autenticar


        // cerrar conexion
        public void CerrarConexion()
        {
            if (Conexion.State != ConnectionState.Closed)
                MConexion.Close();
        }

        // end CerrarConexion


        // Ejecuta un Procedimiento Almacenado en la base. 
        public int Ejecutar(string storedProcedure)
        { return Comando(storedProcedure).ExecuteNonQuery(); } // end Ejecutar

        // Ejecuta un query sql
        public int EjecutarSql(string comandoSql)
        { return ComandoSql(comandoSql).ExecuteNonQuery(); } // end Ejecutar

        //Ejecuta un Procedimiento Almacenado en la base, utilizando los parámetros. 
        public int Ejecutar(string storedProcedure, params Object[] args)
        {
            var com = Comando(storedProcedure);
            CargarParametros(com, args);
            var resp = com.ExecuteNonQuery();
            for (var i = 0; i < com.Parameters.Count; i++)
            {
                var par = (IDbDataParameter)com.Parameters[i];
                if (par.Direction == ParameterDirection.InputOutput || par.Direction == ParameterDirection.Output)
                    args.SetValue(par.Value, i - 1);
            }// end for
            return resp;
        } // end Ejecutar


        #endregion

        #region "Transacciones"

        protected IDbTransaction MTransaccion;
        protected bool EnTransaccion;

        //Comienza una Transacción en la base en uso. 
        public void IniciarTransaccion()
        {
            try
            {
                MTransaccion = Conexion.BeginTransaction();
                EnTransaccion = true;
            }// end try
            finally
            { EnTransaccion = false; }
        }// end IniciarTransaccion


        //Confirma la transacción activa. 
        public void TerminarTransaccion()
        {
            try
            { MTransaccion.Commit(); }
            finally
            {
                MTransaccion = null;
                EnTransaccion = false;
            }// end finally
        }// end TerminarTransaccion


        //Cancela la transacción activa.
        public void AbortarTransaccion()
        {
            try
            { MTransaccion.Rollback(); }
            finally
            {
                MTransaccion = null;
                EnTransaccion = false;
            }// end finally
        }// end AbortarTransaccion


        #endregion

    }// end class 
}
