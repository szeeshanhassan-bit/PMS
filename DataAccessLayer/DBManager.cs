using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace DataAccessLayer
{
    public class DBManager : IDBManager
    {
        private readonly SqlConnection dbConnection = new SqlConnection();
        private SqlDataAdapter dbAdapter = new SqlDataAdapter();
        private readonly SqlCommand dbCommand = new SqlCommand();
        private string connString;
        private string queryString;

        /// <summary>
        /// Constructor with dependency injection of connection string
        /// </summary>
        public DBManager(string connectionString)
        {
            if (string.IsNullOrWhiteSpace(connectionString))
                throw new ArgumentNullException(nameof(connectionString));

            connString = connectionString;
            queryString = connString;
        }

        /// <summary>
        /// Legacy constructor for direct configuration access
        /// </summary>
        public DBManager()
        {
            try
            {
                var legacyConn = ConfigurationManager.ConnectionStrings["conn"]?.ConnectionString
                    ?? ConfigurationManager.ConnectionStrings["connAssuieGreen"]?.ConnectionString;
                connString = legacyConn ?? throw new InvalidOperationException("No connection string found");
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Failed to initialize database connection string", ex);
            }

            queryString = connString;
        }





        public void setConnectionString(string connStr)
        {

            try
            {
                connString = connStr;
            }
            catch (Exception ex)
            {

                throw ex;
            }


        }
        public string getConnectionString()
        {

            try
            {
                return connString;
            }
            catch (Exception ex)
            {

                throw ex;
            }


        }
        public void CreateConnection()
        {
            try
            {
                dbConnection.ConnectionString = connString;
                dbConnection.Open();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void CloseConnection()
        {
            try
            {
                if (dbConnection.State != 0)
                    dbConnection.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable showRecords(string query)
        {
            DataTable table = new DataTable();
            try
            {
                if (dbConnection.State == 0)
                {
                    CreateConnection();
                }
                dbCommand.Connection = dbConnection;
                dbCommand.CommandText = query;
                dbCommand.CommandType = CommandType.Text;
                dbAdapter = new SqlDataAdapter(dbCommand);
                dbAdapter.Fill(table);

            }
            catch (Exception)
            {

                dbAdapter.Dispose();
                dbConnection.Close();
            }
            finally
            {
                dbAdapter.Dispose();
                dbConnection.Close();
            }
            return table;
        }

        public void getData(DataTable dt, string query)
        {

            try
            {
                if (dbConnection.State == 0)
                {
                    CreateConnection();
                }
                dbCommand.Connection = dbConnection;
                dbCommand.CommandText = query;
                dbCommand.CommandType = CommandType.Text;
                dbAdapter = new SqlDataAdapter(dbCommand);
                dbAdapter.Fill(dt);
            }
            catch (Exception)
            {
                dbAdapter.Dispose();
                dbConnection.Close();

            }

            finally
            {
                dbAdapter.Dispose();
                dbConnection.Close();
            }

        }

        public int ExecuteQuery(string query)
        {
            try
            {
                if (dbConnection.State == 0)
                {
                    CreateConnection();
                }
                dbCommand.Connection = dbConnection;
                dbCommand.CommandText = query;
                dbCommand.CommandType = CommandType.Text;
                return dbCommand.ExecuteNonQuery();
            }
            catch (Exception exp)
            {
                throw exp;
            }
            finally
            {
                dbAdapter.Dispose();
                dbConnection.Close();
            }
        }

        public int executeQuery(string query)
        {
            try
            {
                if (dbConnection.State == 0)
                {
                    CreateConnection();
                }
                dbCommand.Connection = dbConnection;
                dbCommand.CommandText = query;
                dbCommand.CommandType = CommandType.Text;
                return dbCommand.ExecuteNonQuery();
            }
            catch (Exception exp)
            {
                throw exp;
            }
            finally
            {
                dbAdapter.Dispose();
                dbConnection.Close();
            }
        }
        public int executeQuery(string query, SqlConnection conn, ref SqlTransaction sqlTran)
        {
            dbCommand.Connection = conn;
            dbCommand.CommandText = query;
            dbCommand.CommandType = CommandType.Text;
            dbCommand.Transaction = sqlTran;
            return dbCommand.ExecuteNonQuery();

        }
        public Int64 GetMaxID(string query)
        {
            try
            {
                if (dbConnection.State == 0)
                {
                    CreateConnection();
                }
                dbCommand.Connection = dbConnection;
                dbCommand.CommandText = query;
                dbCommand.CommandType = CommandType.Text;
                return (Int64)dbCommand.ExecuteScalar();
            }
            catch (Exception exp)
            {
                throw exp;
            }
        }

        public Int64 getMaxID(string query)
        {
            try
            {
                if (dbConnection.State == 0)
                {
                    CreateConnection();
                }
                dbCommand.Connection = dbConnection;
                dbCommand.CommandText = query;
                dbCommand.CommandType = CommandType.Text;
                return (Int32)dbCommand.ExecuteScalar();
            }
            catch (Exception exp)
            {
                throw exp;
            }
        }
        public Int64 getMaxID(string query, SqlConnection conn, ref SqlTransaction sqltran)
        {

            dbCommand.Connection = conn;
            dbCommand.CommandText = query;
            dbCommand.Transaction = sqltran;
            dbCommand.CommandType = CommandType.Text;
            return (Int64)dbCommand.ExecuteScalar();

        }
        public decimal getLastAmount(string query, SqlConnection conn, ref SqlTransaction sqltran)
        {

            dbCommand.Connection = conn;
            dbCommand.CommandText = query;
            dbCommand.Transaction = sqltran;
            dbCommand.CommandType = CommandType.Text;
            return (decimal)dbCommand.ExecuteScalar();

        }


        public DataTable GetRecords(string getQuery)
        {
            SqlConnection conn = new SqlConnection(queryString);
            SqlCommand command = new SqlCommand(getQuery, conn);
            using (conn)
            {
                conn.Open();
                SqlDataReader reader = command.ExecuteReader();
                DataTable table = new DataTable();
                table.Load(reader);
                return table;
            }
        }


    }
}
