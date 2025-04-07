using Microsoft.Data.SqlClient;
using System.Data;

namespace SqlUtility
{
    public static class Connection
    {
        /*
         * Usage:
         * Count the number of students that have a year result greater than 10
         * ExecuteScalar(conn, "SELECT COUNT(*) FROM Student WHERE YearResult > @0", 10);
         */
        public static T ExecuteScalar<T>(SqlConnection conn, string sql, params object[] args)
        {
            using (SqlCommand command = new SqlCommand(sql, conn))
            {
                for (int i = 0; i < args.Length; i++)
                {
                    command.Parameters.AddWithValue($"@{i}", args[i]);
                }

                return (T)command.ExecuteScalar();
            }
        }

        public static IEnumerable<T> ExecuteReader<T>(SqlConnection conn, string sql, Func<SqlDataReader, T> readerFunc, params object[] args)
        {
            using (SqlCommand command = new SqlCommand(sql, conn))
            {
                for (int i = 0; i < args.Length; i++)
                {
                    command.Parameters.AddWithValue($"@{i}", args[i]);
                }

                List<T> list = new List<T>();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(readerFunc(reader));
                    }
                }

                return list;
            }
        }

        public static int ExecuteNonQuery(string sql, SqlConnection conn, params object[] args)
        {
            using (SqlCommand command = new SqlCommand(sql, conn))
            {
                for (int i = 0; i < args.Length; i++)
                {
                    command.Parameters.AddWithValue($"@{i}", args[i]);
                }

                return command.ExecuteNonQuery();
            }
        }

        public static DataTable GetDataTable(SqlConnection conn, string sql, params object[] args)
        {
            using (SqlCommand command = new SqlCommand(sql, conn))
            {
                for (int i = 0; i < args.Length; i++)
                {
                    command.Parameters.AddWithValue($"@{i}", args[i]);
                }

                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable table = new DataTable();
                adapter.Fill(table);

                return table;
            }
        }
    }
}
