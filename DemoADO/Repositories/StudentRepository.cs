using DemoADO.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace DemoADO.Repositories
{
    public class StudentRepository
    {
        SqlConnection connection;

        public StudentRepository(string connectionString)
        {
            connection = new SqlConnection(connectionString);
        }

        public Student StudentMapper(IDataRecord record)
        {
            return new Student
            {
                Id = (int)record["Id"],
                FirstName = (string)record["FirstName"],
                LastName = (string)record["LastName"],
                BirthDate = record["BirthDate"] == DBNull.Value ? null : (DateTime)record["BirthDate"],
                YearResult = (int)record["YearResult"],
                SectionId = (int)record["SectionId"],
                Active = (bool)record["IsActive"]
            };
        }

        public List<Student> GetAll()
        {
            List<Student> students = new List<Student>();

            using (SqlCommand command = connection.CreateCommand())
            {
                command.CommandText = "SELECT * FROM [Student]";

                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        students.Add(StudentMapper(reader));
                    }
                }

                connection.Close();
            }

            return students;
        }
        public float GetYearResultAverage()
        {
            float average;
            using (SqlCommand command = connection.CreateCommand())
            {
                command.CommandText = "SELECT AVG(CONVERT(FLOAT, [YearResult])) " +
                                        "FROM Student";

                connection.Open();

                // Conversion d'un type double en float
                average = Convert.ToSingle(command.ExecuteScalar());

                connection.Close();
            }

            return average;
        }

        public Student? GetById(int id)
        {
            Student? student = null;

            connection.Open();

            using SqlCommand command = connection.CreateCommand();

            command.CommandText = "select * from [dbo].[Student] where id = @Id";
            command.Parameters.AddWithValue("Id", id);

            using (SqlDataReader reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    student = StudentMapper(reader);
                }
            }

            connection.Close();
            return student;
        }

        public Student Create(Student entity)
        {
            int newId = -1;

            connection.Open();
            using (SqlCommand command = connection.CreateCommand())
            {
                command.CommandText = "insert into [dbo].[Student] ([FirstName], [LastName], [BirthDate], [YearResult], [SectionId]) " +
                    "output inserted.ID " +
                    "values (@FirstName, @LastName, @BirthDate, @YearResult, @SectionId)";

                command.Parameters.AddWithValue("FirstName", entity.FirstName);
                command.Parameters.AddWithValue("LastName", entity.LastName);
                command.Parameters.AddWithValue("BirthDate", entity.BirthDate);
                command.Parameters.AddWithValue("YearResult", entity.YearResult);
                command.Parameters.AddWithValue("SectionId", entity.SectionId);

                newId = (int)command.ExecuteScalar();
            }
            connection.Close();

            Student? student = GetById(newId);

            if (student is null)
            {
                throw new Exception("Une erreur s'est produite lors de la création de l'etudiant");
            }

            return student;
        }

        public Student Update(Student entity)
        {
            connection.Open();
            using (SqlCommand command = connection.CreateCommand())
            {
                command.CommandText = "update [dbo].[Student] set [FirstName] = @FirstName, [LastName] = @LastName, [BirthDate] = @BirthDate, [YearResult] = @YearResult, [SectionId] = @SectionId " +
                    "where Id = @Id";

                command.Parameters.AddWithValue("FirstName", entity.FirstName);
                command.Parameters.AddWithValue("LastName", entity.LastName);
                command.Parameters.AddWithValue("BirthDate", entity.BirthDate);
                command.Parameters.AddWithValue("YearResult", entity.YearResult);
                command.Parameters.AddWithValue("SectionId", entity.SectionId);
                command.Parameters.AddWithValue("Id", entity.Id);

                int rows = command.ExecuteNonQuery();
            }
            connection.Close();

            Student? student = GetById(entity.Id);

            if (student is null)
            {
                throw new Exception("Impossible de récupérer l'etudiant mit à jour.");
            }

            return student;
        }

        public void Delete(int id)
        {
            connection.Open();
            using (SqlCommand command = connection.CreateCommand())
            {
                command.CommandText = "delete from [dbo].[Student] where Id = @Id";
                command.Parameters.AddWithValue("Id", id);

                int rows = command.ExecuteNonQuery();
            }
            connection.Close();
        }
    }
}
