using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.Common;
using MySql.Data.MySqlClient;

namespace Components
{
    public abstract class BaseConnector : IDisposable
    {

        protected abstract void CreateConnection();
        protected abstract bool OpenConnection();
        protected abstract bool CloseConnection();

        protected abstract DataTable Select(DbCommand command);
        protected abstract DbDataReader Select(string query);
        protected bool _disposed = false;

        public abstract void Dispose();
        public abstract void Dispose(bool disposing);
    }

    public class MySqlConnector : BaseConnector
    {
        private MySqlConnection connection;
        private string server;
        private string database;
        private string user;
        private string password;

        public MySqlConnector()
        {
            this.CreateConnection();
        }

        ~MySqlConnector()
        {
            this.CloseConnection();
            this.connection.Dispose();
            this.Dispose();
        }

        protected override void CreateConnection()
        {
            this.server = "localhost";
            this.database = "vandr";
            this.user = "root";
            this.password = "";
            string connectionString = "SERVER=" + server + ";" + "DATABASE=" + database + ";" + "UID=" + user + ";" + "PASSWORD=" + password + ";";
            this.connection = new MySqlConnection(connectionString);
        }

        protected override bool OpenConnection()
        {
            try
            {
                this.connection.Open();
                return true;

            }
            catch (MySqlException)
            {
                return false;

            }
        }

        protected override bool CloseConnection()
        {
            try
            {
                this.connection.Close();
                return true;

            }
            catch (MySqlException ex)
            {
                return false;
            }
        }

        protected override DbDataReader Select(string query)
        {

            if (this.OpenConnection())
            {
                using (var command = new MySqlCommand(query, this.connection))
                {

                    command.CommandTimeout = 30;
                    return command.ExecuteReader();
                }
            }
            else
            {
                throw new _MySqlConnectorException("Could not open conection to database");
            }
        }

        protected override DataTable Select(DbCommand command)
        {
            command = command as MySqlCommand;
            if (this.OpenConnection())
            {
                using (var table = new DataTable())
                {
                    table.Load(command.ExecuteReader());
                    return table;
                }
            }
            else
            {
                throw new _MySqlConnectorException("Could not open conection to database");
            }
        }

        /* REMOVE this Function to different class */
        public virtual Tuple<string, string> GetUser(string username, string pwHash)
        {

            string query = "select * from sniperrox.users where username = @username and hash = @pwHash";

            using (var command = new MySqlCommand(query, this.connection))
            {

                command.Parameters.Add("@username", MySqlDbType.VarChar);
                command.Parameters.Add("@pwHash", MySqlDbType.VarChar);
                command.Parameters["@username"].Value = username;
                command.Parameters["@pwHash"].Value = pwHash;

                using (var table = this.Select(command))
                {

                    table.Load(command.ExecuteReader());
                    if (table.Rows.Count > 0)
                    {
                        if (table.Rows.Count > 1)
                            throw new _MySqlConnectorException("Multiple records for given parameters");

                        return new Tuple<string, string>(table.Rows[0][1].ToString(), table.Rows[0][2].ToString());
                    }
                    else
                    {
                        return new Tuple<string, string>("X", "X");
                    }
                }
            }
        }

        public override void Dispose()
        {

            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        public override void Dispose(bool disposing)
        {

            if (this._disposed) return;

            if (disposing)
            {
                this.connection.Dispose();
            }
            this._disposed = true;
        }
    }

    public class MySqlApiConnector : MySqlConnector
    {

        public MySqlApiConnector()
        {
        }
    }
}
