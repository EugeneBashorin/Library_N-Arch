using Entities.Entities;
using LibraryProject.Configurations;
using LibraryProject.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace LibraryProject.Extention_Classes
{
    public static class MagazineExtention
    {
        public static void SetMagazineListToDb(this List<Magazine> magazineList, string connectionString)
        {
            StringBuilder insertSqlExpression = new StringBuilder(300);
            insertSqlExpression.Append("INSERT INTO Magazines ([Name], [Category], [Publisher],[Price]) VALUES");

            foreach (Magazine item in magazineList)
            {
                if (item == magazineList.Last())
                {
                    insertSqlExpression.Append($"('{item.Name}','{item.Category}','{item.Publisher}','{item.Price}');");
                }
                else
                {
                    insertSqlExpression.Append($"('{item.Name}','{item.Category}','{item.Publisher}','{item.Price}'),");
                }
            }

            string InsertSqlExpression = insertSqlExpression.ToString();
            string DeleteSqlExpression = "DELETE FROM Magazines";

            using (SqlConnection con = new SqlConnection(connectionString))
            {

                SqlCommand command = new SqlCommand(DeleteSqlExpression, con);
                try
                {
                    con.Open();
                    command.ExecuteNonQuery();
                }
                catch (Exception)
                {
                }

                command = new SqlCommand(InsertSqlExpression, con);
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
}