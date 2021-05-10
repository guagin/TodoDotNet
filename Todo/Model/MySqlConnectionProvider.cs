using MySql.Data.MySqlClient;
using System;
using System.Data;

namespace Todo.Model
{
    public interface IConnectionProvider
    {
        public IDbConnection GetConnection();
    }
    public class MySqlConnectionProvider: IConnectionProvider
    {
        private string _HostName = "localhost";
        public string HostName
        {
            get {
                return this._HostName;
            }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("HostName could not be null");
                }

                this._HostName = value;
            }
        }

        public int Port
        {
            get;
            set;
        } = 3306;

        private string _UserName = "";
        public string UserName
        {
            get
            {
                return this._UserName;
            }
            set
            {
                if(value == null)
                {
                    throw new ArgumentNullException("UserName could not be null");
                }

                this._UserName = value;
            }
        }

        private string _Password = "password";
        public string Password
        {
            get
            {
                return _Password;
            }
            set
            {
                if(value == null)
                {
                    throw new ArgumentNullException("Passwrod could not be null");
                }

                this._Password = value;
            }
        }

        private string _Database = "database";
        public string Database
        {
            get
            {
                return this._Database;
            }
            set
            {
                if(value == null)
                {
                    throw new ArgumentNullException("Database could not be null");
                }

                this._Database = value;
            }
        }

        public string ConnectionString
        {
            get
            {
                return $@"server={this.HostName};uid={this.UserName};pwd={this.Password};database={this.Database}";
            }
        }
        public IDbConnection GetConnection()
        {
            return new MySqlConnection(ConnectionString);
        }
    }
}
