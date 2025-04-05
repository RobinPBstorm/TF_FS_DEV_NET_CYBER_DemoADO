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

		public StudentRepository (string connectionString)
		{
			connection = new SqlConnection(connectionString);
		}

		public Student StudentMapper(IDataRecord record)
		{
			return new Student
			{
				Id = (int) record["Id"],
				FirstName = (string) record["FirstName"],
				LastName = (string) record["LastName"],
				BirthDate = record["BirthDate"] == DBNull.Value ? null : (DateTime) record["BirthDate"],
				YearResult = (int) record["YearResult"],
				SectionId = (int) record["SectionId"],
				Active = (bool) record["IsActive"]
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

	}
}
