using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZstdSharp.Unsafe;

namespace BasketTeam
{
    internal class Program
    {
        public static Connect conn = new Connect();
        public static void GetAllData()
        {
            conn.Connection.Open();

            string sql = "SELECT * FROM `player`";

            MySqlCommand cmd = new MySqlCommand(sql, conn.Connection);

            MySqlDataReader dr = cmd.ExecuteReader();

                dr.Read();
                
                do
                {
                var player = new
                    Id = dr.GetInt32(0),
                    Name = dr.GetString(1),
                    Height = dr.GetInt32(2),
                    Weight = dr.GetInt32(3),
                    CreatedTime = dr.GetDateTime(4),
                };
                Console.WriteLine($"Játékos adatok: {player.Name},{player.Height}");
            }
                while (dr.Read());

            dr.Close();

            conn.Connection.Close();
            public static void AddPlayer(string name, int height, int weight)
        {
            using (var connection = Connect.GetConnection())
            {
                string query = "INSERT INTO Player (Name, Height, Weight) VALUES (@Name, @Height, @Weight);";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Name", name);
                    command.Parameters.AddWithValue("@Height", height);
                    command.Parameters.AddWithValue("@Weight", weight);
                    command.ExecuteNonQuery();
                    Console.WriteLine("Player added successfully.");
                }
            }
        }

        public static void DeletePlayer(int id)
        {
            using (var connection = Connect.GetConnection())
            {
                string query = "DELETE FROM Player WHERE Id = @Id;";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);
                    command.ExecuteNonQuery();
                    Console.WriteLine($"Player with ID {id} deleted.");
                }
            }
        }

        public static void UpdatePlayer(int id, string name, int height, int weight)
        {
            using (var connection = Connect.GetConnection())
            {
                string query = "UPDATE Player SET Name = @Name, Height = @Height, Weight = @Weight WHERE Id = @Id;";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);
                    command.Parameters.AddWithValue("@Name", name);
                    command.Parameters.AddWithValue("@Height", height);
                    command.Parameters.AddWithValue("@Weight", weight);
                    command.ExecuteNonQuery();
                    Console.WriteLine($"Player with ID {id} updated.");
                }
            }
        }
        static void Main(string[] args)
        {
        Console.WriteLine("Welcome to BasketTeam!");

        while (true)
        {
            Console.WriteLine("\nChoose an option:");
            Console.WriteLine("1. View all players");
            Console.WriteLine("2. Add a new player");
            Console.WriteLine("3. Delete a player");
            Console.WriteLine("4. Update a player");
            Console.WriteLine("5. Exit");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    PlayerService.GetAllPlayers();
                    break;
                case "2":
                    Console.Write("Enter Name: ");
                    string name = Console.ReadLine();
                    Console.Write("Enter Height: ");
                    int height = int.Parse(Console.ReadLine());
                    Console.Write("Enter Weight: ");
                    int weight = int.Parse(Console.ReadLine());
                    PlayerService.AddPlayer(name, height, weight);
                    break;
                case "3":
                    Console.Write("Enter Player ID to delete: ");
                    int idToDelete = int.Parse(Console.ReadLine());
                    PlayerService.DeletePlayer(idToDelete);
                    break;
                case "4":
                    Console.Write("Enter Player ID to update: ");
                    int idToUpdate = int.Parse(Console.ReadLine());
                    Console.Write("Enter New Name: ");
                    string newName = Console.ReadLine();
                    Console.Write("Enter New Height: ");
                    int newHeight = int.Parse(Console.ReadLine());
                    Console.Write("Enter New Weight: ");
                    int newWeight = int.Parse(Console.ReadLine());
                    PlayerService.UpdatePlayer(idToUpdate, newName, newHeight, newWeight);
                    break;
                case "5":
                    return;
                default:
                    Console.WriteLine("Invalid choice.");
                    break;
            }
            GetAllData();

        }
}
}
