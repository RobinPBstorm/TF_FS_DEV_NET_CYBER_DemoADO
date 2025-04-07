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

        
        public Trainer? GetById(int trainerId) {
            Trainer? trainer = null;

            connection.Open();
            using SqlCommand command = connection.CreateCommand();

            command.CommandText = "select * from [dbo].[Trainer] where id = @Id";
            command.Parameters.AddWithValue("Id", trainerId);

            using (SqlDataReader reader = command.ExecuteReader()) {
                if (reader.Read()) {
                    trainer = Mapper.TrainerMapper(reader);
                }
            }

            connection.Close();
            return trainer;
        }

        public bool CreateSansOutput(Trainer trainer) {
            connection.Open();
            using (SqlCommand command = connection.CreateCommand()) {
                // Attention injection SQL
                //command.CommandText = $"insert into [dbo].[Trainer] ([Firstname, [LastName], [BirthDate]) VALUES ({trainer.FirstName}, {trainer.LastName})";

                // Requête parametrees
                command.CommandText = "insert into [dbo].[Trainer] ([Firstname], [LastName], [BirthDate]) " +
                    "VALUES (@FirstName, @LastName, @BirthDate)";

                // SqlParameter Generique
                SqlParameter firstNameParam = new SqlParameter()
                {
                    ParameterName = "FirstName",
                    Value = trainer.FirstName
                };

                SqlParameter lastNameParam = new SqlParameter()
                {
                    ParameterName = "LastName",
                    Value = trainer.LastName
                };

                SqlParameter birthDateParam = new SqlParameter()
                {
                    ParameterName = "BirthDate",
                    Value = trainer.BirthDate
                };

                command.Parameters.Add(firstNameParam);
                command.Parameters.Add(lastNameParam);
                command.Parameters.Add(birthDateParam);

                // Sql Server only:
                //command.Parameters.AddWithValue("FirstName", trainer.FirstName);

                int rows = command.ExecuteNonQuery();
            }
            connection.Close();
            return true;
        }

        public Trainer Create(Trainer trainer)
        {
            int newId = -1;

            connection.Open();
            using (SqlCommand command = connection.CreateCommand())
            {
                // Attention injection SQL
                //command.CommandText = $"insert into [dbo].[Trainer] ([Firstname, [LastName], [BirthDate]) VALUES ({trainer.FirstName}, {trainer.LastName})";

                // Requête parametrees
                command.CommandText = "insert into [dbo].[Trainer] ([Firstname], [LastName], [BirthDate]) " +
                    "output inserted.ID " +
                    "values (@FirstName, @LastName, @BirthDate);";

                // SqlParameter Generique
                SqlParameter firstNameParam = new SqlParameter()
                {
                    ParameterName = "FirstName",
                    Value = trainer.FirstName
                };

                SqlParameter lastNameParam = new SqlParameter()
                {
                    ParameterName = "LastName",
                    Value = trainer.LastName
                };

                SqlParameter birthDateParam = new SqlParameter()
                {
                    ParameterName = "BirthDate",
                    Value = trainer.BirthDate is null ? DBNull.Value : trainer.BirthDate
                };

                command.Parameters.Add(firstNameParam);
                command.Parameters.Add(lastNameParam);
                command.Parameters.Add(birthDateParam);

                // Sql Server only:
                //command.Parameters.AddWithValue("FirstName", trainer.FirstName);

                newId = (int)command.ExecuteScalar();
            }
            connection.Close();

            Trainer? createdTrainer = GetById(newId);

            if (createdTrainer is null) {
                throw new Exception("Une erreur s'est produite lors de la création du trainer!");
            }

            return createdTrainer;
        }
    }
}
