using Microsoft.Data.SqlClient;
using System.Data;

namespace SqlUtility
{
    public static class Command
    {
        public static bool AddSection(SqlConnection conn, int id, string sectionName)
        {
            try
            {
                using (SqlCommand command = new SqlCommand("AddSection", conn))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Id", id);
                    command.Parameters.AddWithValue("@SectionName", 666);

                    command.ExecuteNonQuery();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
            return true;
        }

        public static bool AddStudent(SqlConnection conn, string lastname, string firstname, DateTime birthDate, int yearResult, int sectionId)
        {
            try
            {
                using (SqlCommand command = new SqlCommand("AddStudent", conn))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Firstname", firstname);
                    command.Parameters.AddWithValue("@Lastname", lastname);
                    command.Parameters.AddWithValue("@BirthDate", birthDate);
                    command.Parameters.AddWithValue("@YearResult", yearResult);
                    command.Parameters.AddWithValue("@SectionID", sectionId);


                    command.ExecuteNonQuery();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
            return true;
        }

        public static bool UpdateStudent(SqlConnection conn, int studentId, int sectionId, int yearResult)
        {
            try
            {
                using (SqlCommand command = new SqlCommand("UpdateStudent", conn))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@StudentId", studentId);
                    command.Parameters.AddWithValue("@SectionID", sectionId);
                    command.Parameters.AddWithValue("@YearResult", yearResult);

                    command.ExecuteNonQuery();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
            return true;
        }

        public static bool DeleteStudent(SqlConnection conn, int id)
        {
            try
            {
                using (SqlCommand command = new SqlCommand("DeleteStudent", conn))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Id", id);

                    command.ExecuteNonQuery();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
            return true;
        }
    }
}
