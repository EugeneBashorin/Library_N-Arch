using DataAccessLayer.Context;
using DataAccessLayer.Interfaces;
using Entities.Entities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;

namespace DataAccessLayer.Repositories
{
    public class MagazineRepository :/* IRepository<Magazine>*/ IMagazineRepository
    {
        string _connectionString;
        public MagazineRepository(/*PublicationContext context*/string connectionString)
        {
            _connectionString = connectionString;
            //DbConnection = context;
        }

        public List<Magazine> GetAll()
        {
            List<Magazine> magazinesList;
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                magazinesList = new List<Magazine>();
                if (connection != null)
                {
                    string magazinesSelectAllExpression = "SELECT * FROM Magazines";
                    SqlCommand command = new SqlCommand(magazinesSelectAllExpression, connection);
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            magazinesList.Add(new Magazine { Id = (int)reader.GetValue(0), Name = (string)reader.GetValue(1),   Category = (string)reader.GetValue(2), Publisher = (string)reader.GetValue(3), Price = (int)reader.GetValue(4) });
                        }
                    }
                }

            }
            return magazinesList;
        }

        public Magazine GetItemById(int? id)
        {
            Magazine magazine = new Magazine();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                if (connection != null)
                {
                    string searchMagazineByIdExpression = $"SELECT * FROM Magazines WHERE Id = '{id}'";
                    SqlCommand command = new SqlCommand(searchMagazineByIdExpression, connection);
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            magazine.Id = (int)reader.GetValue(0);
                            magazine.Name = (string)reader.GetValue(1);
                            magazine.Category = (string)reader.GetValue(2);
                            magazine.Publisher = (string)reader.GetValue(3);
                            magazine.Price = (int)reader.GetValue(4);
                        }
                    }
                }
            }
            return magazine;
        }

        public void Create(Magazine magazine)
        {
            string createMagazineExpression = $"INSERT INTO Magazines([Name], [Category], [Publisher],[Price]) VALUES('{magazine.Name}','{magazine.Category}','{magazine.Publisher}','{magazine.Price}')";
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(createMagazineExpression, connection);
                try
                {
                    command.ExecuteNonQuery();
                }
                catch (Exception)
                {
                }
            }
        }

        public void Update(int? Id, Magazine magazine)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                if (connection != null)
                {
                    string editMagazineExpression = $"UPDATE Magazines SET Name = '{magazine.Name}', Category = '{magazine.Category}', Publisher = '{magazine.Publisher}', Price = '{magazine.Price}' WHERE Id = '{Id}'";
                    SqlCommand command = new SqlCommand(editMagazineExpression, connection);
                    try
                    {
                        command.ExecuteNonQuery();
                    }
                    catch (Exception)
                    {
                    }
                }
            }
        }

        public void Delete(int? id)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                if (connection != null)
                {
                    string deleteMagazineExpression = $"DELETE FROM Magazines WHERE Id = '{id}'";
                    SqlCommand command = new SqlCommand(deleteMagazineExpression, connection);
                    try
                    {
                        command.ExecuteNonQuery();
                    }
                    catch (Exception)
                    {
                    }
                }
            }
        }

        public List<Magazine> FilterByPublisher(string publisherName)
        {
            List<Magazine> magazineList;
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                magazineList = new List<Magazine>();
                if (connection != null)
                {
                    string selectAllExpression = $"SELECT * FROM Magazines WHERE Publisher = '{publisherName}'";
                    SqlCommand command = new SqlCommand(selectAllExpression, connection);
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            magazineList.Add(new Magazine { Id = (int)reader.GetValue(0), Name = (string)reader.GetValue(1),  Category = (string)reader.GetValue(2), Publisher = (string)reader.GetValue(3), Price = (int)reader.GetValue(4) });
                        }
                    }
                }
            }
            return magazineList;
        }

        public List<string> GetAllPublishers()
        {
            List<string> publishersList;
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                publishersList = new List<string>();
                if (connection != null)
                {
                    string selectAllExpression = $"SELECT DISTINCT Publisher FROM Magazines";
                    SqlCommand command = new SqlCommand(selectAllExpression, connection);
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            publishersList.Add(reader.GetString(0));
                        }
                    }
                }
            }
            return publishersList;
        }
    }
}