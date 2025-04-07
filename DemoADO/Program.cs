using DemoADO.Models;
using DemoADO.Repositories;
using Microsoft.Data.SqlClient;
using System.Data;

namespace DemoADO
{
    internal class Program
	{
		static void Main(string[] args)
		{
			string connectionString = "Server=localhost;User Id=SA;Password=Some4Complex#Password;Trust Server Certificate=True;Database=TFGOSFQ25L008DEVNET_ado";
			#region demo SQL Connection et sélection de données
			/*
			using (SqlConnection connection = new SqlConnection())
			{
				connection.ConnectionString = connectionString;

				// Console.WriteLine(connection.State);

				// Exemple de scalar
				connection.Open();
				// instructions
				SqlCommand command = connection.CreateCommand();
				command.CommandText = "SELECT COUNT(*) FROM [dbo].[Trainer];";

				int count = (int)command.ExecuteScalar();

				Console.WriteLine(count);


				connection.Close();


				// Exemple de Reader
				connection.Open();

				command.CommandText = "SELECT [Id], [FirstName], [lastName] " +
					"FROM [dbo].[Trainer];";

				SqlDataReader reader = command.ExecuteReader();

				while (reader.Read())
				{
					Console.WriteLine($"{reader["Id"]} {reader["FirstName"]} {reader["LastName"]}");
				}

				connection.Close();
				// Exemple d'injection SQL
				connection.Open();
				string id = "1 OR 1 = 1";
				//command.CommandText = "SELECT [Id], [FirstName], [lastName] " +
				//	"FROM [dbo].[Trainer] " +
				//	"WHERE [Id] = " + id;


				// Utilisation de commande paramètrée
				string id2 = "3";
				command.CommandText = "SELECT [Id], [FirstName], [lastName], [BirthDate], [IsActive] " +
					"FROM [dbo].[Trainer] " +
					"WHERE [Id] = @idRecherche";

				command.Parameters.AddWithValue("idRecherche", id2);

				SqlDataReader reader2 = command.ExecuteReader();

				while (reader2.Read())
				{
					Trainer trainer = Mapper.TrainerMapper(reader2);
					Console.WriteLine($"{trainer.FirstName}");
				}

				connection.Close();


				TrainerRepository trainerRepository = new TrainerRepository(connectionString);

				foreach(Trainer trainer in trainerRepository.GetAll())
				{
					Console.WriteLine($"{trainer.Id} {trainer.LastName} {trainer.FirstName}");
				}
			}*/
			#endregion
			#region exercice
			// Pour chaque étudiant afficher son ID, Nom et son Prénom
			StudentRepository studentRepository = new StudentRepository(connectionString);

			foreach (Student student in studentRepository.GetAll())
			{
				Console.WriteLine($"{student.Id} {student.LastName} {student.FirstName}");
			}

			// Afficher la moyenne de l'ensemble des étudiants
			Console.WriteLine(studentRepository.GetYearResultAverage());
            #endregion

            #region Demo insert
			TrainerRepository trainerRepository = new TrainerRepository(connectionString);
			Trainer newTrainer = new Trainer(-1, "Philippe", "Haerens", new DateTime(1997, 9, 9), true);
			Trainer created = trainerRepository.Create(newTrainer);
            Console.WriteLine("New trainer: " + created.Id);
            #endregion
        }
    }
}
