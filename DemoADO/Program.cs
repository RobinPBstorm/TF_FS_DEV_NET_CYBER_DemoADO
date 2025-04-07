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
            //TrainerRepository trainerRepository = new TrainerRepository(connectionString);
            //Trainer newTrainer = new Trainer(-1, "Philippe", "Haerens", new DateTime(1997, 9, 9), true);
            //Trainer created = trainerRepository.Create(newTrainer);
            //         Console.WriteLine("New trainer: " + created.Id);
            #endregion

            #region Exercise Insert, update et delete
            //         Student student1 = new Student() { 
            //	Active = true,
            //	FirstName = "Hello",
            //	LastName = "World",
            //	SectionId = 2,
            //	BirthDate = DateTime.Now,
            //	YearResult = 11
            //};

            //Student createdStudent1 = studentRepository.Create(student1);
            //Console.WriteLine("New student: " + createdStudent1.Id);

            //student1.Id = 3;
            //student1.FirstName = "Bonjour";
            //         Student updatedStudent1 = studentRepository.Update(student1);

            studentRepository.Delete(3);
            #endregion

            #region Procédure stockee
            #region Démo
            /*
			string nouvelleSection = "Surnaturel";

			using (SqlConnection connection = new SqlConnection()) {
				connection.ConnectionString = connectionString;

				using (SqlCommand command = connection.CreateCommand()) {
					command.CommandText = "[dbo].[AddSection]";
					command.CommandType = CommandType.StoredProcedure;

					// paramètre d'entrée
					command.Parameters.AddWithValue("@Section_Name", nouvelleSection);

					// paramètre de sortie
					SqlParameter outputSectionId = new SqlParameter()
					{
						ParameterName = "@Section_Id",
						Value = 0,
						Direction = ParameterDirection.Output,
					};
					command.Parameters.Add(outputSectionId);

					connection.Open();
					command.ExecuteNonQuery();
					connection.Close();

                    Console.WriteLine("Id de la nouvelle section: " + outputSectionId.Value);
				}
			}*/
            #endregion

            #region Exercices
            // Appelez la procédure pour changer de « Section » l’étudiant vous représentant
            int studentId = 2;
            int newSectionId = 3;

            // récupération de l'étudiant
            Student? studentToUpdate = studentRepository.GetById(studentId);

            if (studentToUpdate != null)
            {
                // appeler la procedure
                using (SqlConnection connection = new SqlConnection())
                {
                    connection.ConnectionString = connectionString;

                    using (SqlCommand command = connection.CreateCommand())
                    {
                        // prepare command
                        command.CommandText = "[dbo].[UpdateStudent]";
                        command.CommandType = CommandType.StoredProcedure;

                        // params
                        command.Parameters.AddWithValue("@Student_Id", studentToUpdate.Id);
                        command.Parameters.AddWithValue("@Section_Id", newSectionId);
                        command.Parameters.AddWithValue("@Year_Result", studentToUpdate.YearResult);

                        // executer
                        connection.Open();
                        command.ExecuteNonQuery();
                        connection.Close();
                    }
                }
            }

            // Appelez la procédure pour supprimer votre voisin de la base de données
            int studentIdToDelete = 1;

            // appeler la procedure
            using (SqlConnection connection = new SqlConnection())
            {
                connection.ConnectionString = connectionString;

                using (SqlCommand command = connection.CreateCommand())
                {
                    // prepare command
                    command.CommandText = "[dbo].[DeleteStudent]";
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@Student_Id", studentIdToDelete);

                    // executer
                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }
            }

            #endregion
            #endregion

            #region Transaction
            List<Trainer> trainers = new List<Trainer>() {
                new Trainer(-1, "Dipper", "Pines", null, true),
                new Trainer(-1, "Mabbel", "Pines", null, true),
                new Trainer(-1, "Bill", "Cypher", null, true)
            };

            using (SqlConnection connection = new SqlConnection())
            {
                connection.ConnectionString = connectionString;

                connection.Open();

                using (SqlTransaction transaction = connection.BeginTransaction())
                {
                    try
                    {
                        foreach (Trainer trainer in trainers)
                        {
                            using (SqlCommand command = connection.CreateCommand())
                            {
                                command.CommandText = "insert into [dbo].[Trainer] ([FirstName], [LastName], [BirthDate]) " +
                                    "values (@FirstName, @LastName, @BirthDate)";

                                command.Transaction = transaction;

                                command.Parameters.AddWithValue("FirstName", trainer.FirstName);
                                command.Parameters.AddWithValue("LastName", trainer.LastName);
                                command.Parameters.AddWithValue("BirthDate", trainer.BirthDate is null ? DBNull.Value : trainer.BirthDate);

                                command.ExecuteNonQuery();
                            }
                        }

                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex);
                        transaction.Rollback();
                    }
                }
                connection.Close();
            }
            #endregion

            #region Abstract factory
            List<Course> courses = new List<Course>();
            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                string search = "S";

                using (IDbCommand cmd = connection.CreateCommand())
                {
                    cmd.CommandText = "SELECT * FROM Course WHERE name LIKE @Search";

                    IDbDataParameter paramSearch = cmd.CreateParameter();
                    paramSearch.ParameterName = "Search";
                    paramSearch.Value = "%" + search + "%";
                    cmd.Parameters.Add(paramSearch);

                    connection.Open();
                    using (IDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Course course = new Course()
                            {
                                Id = (int)reader["Id"],
                                Name = reader["Name"] is DBNull ? null : (string)reader["Name"]
                            };
                            courses.Add(course);
                        }
                    }
                    connection.Close();
                }
                Console.WriteLine("Total: " + courses.Count);
            }
            #endregion
        }
    }
}
