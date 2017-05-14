using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using System.Data;

namespace DataAccess
{
    public class DataAccess
    {
        private string _ConnectionString;
        public string ConnectionString
        {
            get
            {
                return _ConnectionString;
            }
            set
            {
                _ConnectionString = value;
            }
        }

        #region "DataAccess Functions"

        /// <summary>
        /// GetReader Function
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="trans"></param>
        /// <param name="cmdtype"></param>
        /// <param name="parameters"></param>
        /// <param name="Sql"></param>
        /// <returns></returns>
        private MySqlDataReader GetReader(MySqlConnection conn, MySqlTransaction trans, CommandType cmdtype, Parameters parameters, string Sql)
        {
            MySqlCommand cmd;
            MySqlDataReader reader;

            cmd = new MySqlCommand(Sql, conn);
            cmd.CommandType = cmdtype;

            //set transaction
            if (trans != null)
            {
                cmd.Transaction = trans;
            }

            //set parameters
            if (parameters != null)
            {
                foreach (MySqlParameter param in parameters.collection.Values)
                {
                    cmd.Parameters.Add(param);
                }
            }

            reader = cmd.ExecuteReader();

            return reader;
        }

        public MySqlDataReader GetReader(MySqlConnection conn, MySqlTransaction trans, CommandType cmdtype, string Sql)
        {
            return GetReader(conn, trans, cmdtype, null, Sql);
        }

        public MySqlDataReader GetReader(MySqlConnection conn, MySqlTransaction trans, string Sql)
        {
            return GetReader(conn, trans, CommandType.Text, null, Sql);
        }

        public MySqlDataReader GetReader(MySqlConnection conn, string Sql)
        {
            return GetReader(conn, null, CommandType.Text, null, Sql);
        }

        public MySqlDataReader GetReader(MySqlConnection conn, Parameters parameters, string Sql)
        {
            return GetReader(conn, null, CommandType.Text, parameters, Sql);
        }

        public MySqlDataReader GetReader(MySqlConnection conn, CommandType cmdtype, string Sql, Parameters parameters)
        {
            return GetReader(conn, null, cmdtype, parameters, Sql);
        }

        public Object GetScalar(MySqlConnection conn, MySqlTransaction trans, string Sql, Parameters parameters)
        {
            Object obj;
            MySqlCommand cmd;

            cmd = new MySqlCommand(Sql, conn, trans);

            if (parameters != null)
            {
                foreach (MySqlParameter param in parameters.collection.Values)
                {
                    cmd.Parameters.Add(param);
                }
            }

            obj = cmd.ExecuteScalar();

            return obj;
        }

        public Object GetScalar(MySqlConnection conn, string Sql, Parameters parameters)
        {
            return GetScalar(conn, null, Sql, parameters);
        }

        public Object GetScalar(MySqlConnection conn, string Sql)
        {
            return GetScalar(conn, null, Sql, null);
        }

        public int ExecuteNonQuery(MySqlConnection conn, MySqlTransaction trans, string Sql, Parameters parameters)
        {
            return ExecuteNonQuery(conn, trans, CommandType.Text, Sql, parameters);
        }

        public int ExecuteNonQuery(MySqlConnection conn, MySqlTransaction trans, CommandType cmdtype, string Sql, Parameters parameters)
        {
            MySqlCommand cmd;

            cmd = new MySqlCommand(Sql, conn, trans);
            cmd.CommandType = cmdtype;

            if (parameters != null)
            {
                foreach (MySqlParameter param in parameters.collection.Values)
                {
                    cmd.Parameters.Add(param);
                }
            }

            return cmd.ExecuteNonQuery();
        }

        #endregion

        #region "Connection Handlers"

        public MySqlConnection GetDatabaseConnection()
        {
            try
            {
                //validate connection string.
                if ((ConnectionString == "") || (ConnectionString == null))
                {
                    throw new Exception(ConnectionErrors.ConnectionError_BlankConnectionString.ToString() + " - " + (int)ConnectionErrors.ConnectionError_BlankConnectionString);
                }

                MySqlConnection conn = new MySqlConnection(ConnectionString);
                conn.Open();
                return conn;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void CloseConnection(MySqlConnection conn)
        {
            if (conn.State != ConnectionState.Closed)
            {
                conn.Close();
            }
        }

        #endregion
    }
}
