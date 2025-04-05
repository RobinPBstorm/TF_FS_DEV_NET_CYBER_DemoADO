using DemoADO.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoADO.Repositories
{
    public class TrainerRepository
    {
        SqlConnection connection;

        public TrainerRepository(string connectionString)
        {
            connection = new SqlConnection(connectionString);
        }

        public List<Trainer> GetAll()
        {
            List<Trainer> trainers = new List<Trainer>();

            connection.Open();

            SqlCommand command = connection.CreateCommand();
            command.CommandText = "SELECT [Id], [FirstName], [lastName], [BirthDate], [IsActive] " +
                    "FROM [dbo].[Trainer] ";

            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    trainers.Add(Mapper.TrainerMapper(reader));
                }
            }

            connection.Close();

            return trainers;
        }
    }
}
