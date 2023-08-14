﻿using System;

public class Database
{
    private string dataSource = "";
    private string userId = "";
    private string password = "";
    private string initialCatalog = "";
	
    public static void Connect()
    {
        try
        {
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
            builder.DataSource = "<your_server>.database.windows.net";
            builder.UserID = "<your_username>";
            builder.Password = "<your_password>";
            builder.InitialCatalog = "<your_database>";

            using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
            {
                Console.WriteLine("\nQuery data example:");
                Console.WriteLine("=========================================\n");

                String sql = "SELECT name, collation_name FROM sys.databases";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Console.WriteLine("{0} {1}", reader.GetString(0), reader.GetString(1));
                        }
                    }
                }
            }
        }
        catch (SqlException e)
        {
            Console.WriteLine(e.ToString());
        }
        Console.ReadLine();
    }
	
}
