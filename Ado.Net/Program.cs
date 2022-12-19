using System.Data;
using System.Data.SqlClient;
using System.Xml.Linq;

namespace Ado.Net
{
    internal class Program
    {
        static void Main(string[] args)
        {
            GetPizzas();
            Console.WriteLine("Melumat almag istediyiniz pizzanin idsini daxil edin. Cixis ucun 0 basin:");
            int index = int.Parse(Console.ReadLine());
            if (index == 0)
                Console.WriteLine("Bitdi");
            else if (index == 2)
            {
                CreateSpecialPizza();
                Console.ReadLine();
            }

            else
                GetPizzaInfosById(index);

        }
        static string connectionString = "Server=THEBRHMLY;Database=PizzamizzaWithDotNet;Trusted_Connection=True;";
        public static async void GetPizzas()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM Pizzas", conn))
                {
                    SqlDataReader reader = await cmd.ExecuteReaderAsync();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            Console.WriteLine($"{reader["id"]} {reader["name"]} {reader["price"]} ");
                        }
                    }
                }
            }
        }
        public static async void GetPizzaInfosById(int id)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("SELECT name, price from Pizzas where id=@id", conn))
                {
                    cmd.Parameters.AddWithValue("id", SqlDbType.Int).Value = id;
                    string name = (await cmd.ExecuteScalarAsync()).ToString();
                    var price = (await cmd.ExecuteScalarAsync());
                    Console.WriteLine($"Pizza Name: {name}, Price:{price}");
                }
            }
        }
        public static async void CreateSpecialPizza()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("insert into Pizzas Values(@Name)", conn))
                {
                    Console.WriteLine("Pizzanin adini daxil edin: ");
                    string name = Console.ReadLine();
                    cmd.Parameters.AddWithValue("Name", SqlDbType.NVarChar).Value = name;
                    Console.WriteLine("Select:\n1.Pendir\n2.Sogan\n3.Biber\n");
                    while(true)
                    {
                        int value = int.Parse(Console.ReadLine());
                        if (value == 1)
                            Console.WriteLine("Pendir elave olundu!");
                        if (value == 2)
                            Console.WriteLine("Sogan elave olundu!");
                        if (value == 3)
                            Console.WriteLine("Biber elave olundu!");
                        if (value == 0)
                            break;
                    }
                    int result = await cmd.ExecuteNonQueryAsync();
                    if (result != 0)
                        Console.WriteLine("Added!");
                    else
                        Console.WriteLine("Not added!");
                }
            }
        }
        /*public static async void GetInfos(int id)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("SELECT name from Authors where id=@id",conn))
                {
                    cmd.Parameters.AddWithValue("id", SqlDbType.Int).Value=id;
                    string name = (await cmd.ExecuteScalarAsync()).ToString();
                    Console.WriteLine(name);
                }
            };
        }*/
        /*public static async void InsertAuthor(string name,string surname)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("insert into Authors Values(@name,@Surname)",conn))
                {
                    cmd.Parameters.AddWithValue("name", SqlDbType.NVarChar).Value = name;
                    cmd.Parameters.AddWithValue("Surname", SqlDbType.NVarChar).Value = surname;
                    int result = await cmd.ExecuteNonQueryAsync();
                    if (result != 0)
                        Console.WriteLine("Added!");
                    else
                        Console.WriteLine("Not added!");
                }
            }
        }*/
        /*public static async void GetAllAuthors()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM Authors",conn))
                {
                    SqlDataReader reader = await cmd.ExecuteReaderAsync();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            //Console.WriteLine($"{reader[0]} {reader[1]} {reader[2]}");
                            Console.WriteLine($"{reader["id"]} {reader["name"]} {reader["surname"]}");
                        }
                    }
                }
            }

        }*/
    }
}