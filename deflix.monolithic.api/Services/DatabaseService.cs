namespace deflix.monolithic.api.Services
{
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using deflix.monolithic.api.Interfaces;
    using Microsoft.Extensions.Configuration;

    public class DatabaseService : IDatabaseService
    {
        private readonly string _connectionString;

        public DatabaseService(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DBConnection");
        }

        public T ExecuteScalar<T>(string query, params SqlParameter[] parameters)
        {
            using var connection = new SqlConnection(_connectionString);
            connection.Open();
            using var command = new SqlCommand(query, connection);
            if (parameters != null)
            {
                command.Parameters.AddRange(parameters);
            }
            return (T)command.ExecuteScalar();
        }

        public int ExecuteNonQuery(string query, params SqlParameter[] parameters)
        {
            using var connection = new SqlConnection(_connectionString);
            connection.Open();
            using var command = new SqlCommand(query, connection);
            if (parameters != null)
            {
                command.Parameters.AddRange(parameters);
            }
            return command.ExecuteNonQuery();
        }

        public SqlDataReader ExecuteReader(string query, params SqlParameter[] parameters)
        {
            var connection = new SqlConnection(_connectionString);
            connection.Open();
            using var command = new SqlCommand(query, connection);
            if (parameters != null)
            {
                command.Parameters.AddRange(parameters);
            }
            // Important: Return reader without closing connection for usage in service
            return command.ExecuteReader(CommandBehavior.CloseConnection);
        }
    }

}
