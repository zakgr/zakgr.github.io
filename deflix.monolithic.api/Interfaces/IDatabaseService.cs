namespace deflix.monolithic.api.Interfaces
{
    using System.Data.SqlClient;

    public interface IDatabaseService
    {
        T ExecuteScalar<T>(string query, params SqlParameter[] parameters);
        int ExecuteNonQuery(string query, params SqlParameter[] parameters);
        SqlDataReader ExecuteReader(string query, params SqlParameter[] parameters);
    }

}
