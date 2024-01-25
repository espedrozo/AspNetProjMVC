using AspNetProjMVC.Models;
using MySql.Data.MySqlClient;

namespace AspNetProjMVC.Controllers
{
    public class MySqlController2
    {
        private readonly DatabaseController _databaseController2;

        public MySqlController2()
        {
            _databaseController2 = new DatabaseController();
        }
        public List<Customer> GetClients()
        {
            List<Customer> customers = new List<Customer>();
            using (MySqlConnection con = _databaseController2.GetConnection2())
            {
                using (MySqlCommand com = new MySqlCommand("SELECT id,first_name,last_name,sex,birth_date,status FROM client", con))
                {
                    con.Open();
                    using (MySqlDataReader dr = com.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            customers.Add(new Customer()
                            {
                                id = (int)dr["id"],
                                first_name = dr["first_name"].ToString(),
                                last_name = dr["last_name"].ToString(),
                                sex = dr["sex"].ToString(),
                                birth_date = (DateTime)dr["birth_date"],
                                status = (int)dr["status"]
                            });
                        }
                        con.Close();
                        return customers;
                    }
                }
            }
        }



        public List<Customer> SyncCustomersMySql()
        {
            List<Customer> customers = new List<Customer>();
            using (MySqlConnection con = _databaseController2.GetConnection())
            {
                using (MySqlCommand com = new MySqlCommand("SELECT id,first_name,last_name,sex,birth_date,status FROM client WHERE status = 1", con))
                {
                    con.Open();
                    using (MySqlDataReader dr = com.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            customers.Add(new Customer()
                            {
                                id = (int)dr["id"],
                                first_name = dr["first_name"].ToString(),
                                last_name = dr["last_name"].ToString(),
                                sex = dr["sex"].ToString(),
                                birth_date = (DateTime)dr["birth_date"],
                                status = (int)dr["status"]
                            });
                        }
                    }
                }
            }

            return customers;
        }

        public async Task UpdateMySqlCustomers(List<Customer> customers)
        {
            using (MySqlConnection con = _databaseController2.GetConnection())
            {
                await con.OpenAsync();

                foreach (var customer in customers)
                {
                    using (MySqlCommand com = con.CreateCommand())
                    {
                        com.CommandText = "UPDATE client SET status = 2 WHERE id = @id";
                        com.Parameters.AddWithValue("@id", customer.id);

                        await com.ExecuteNonQueryAsync();
                    }
                }
            }
        }

        public async Task InsertCustomerMySql(Customer customer)
        {
            using (MySqlConnection con = _databaseController2.GetConnection())
            {
                await con.OpenAsync();

                using (MySqlCommand com = con.CreateCommand())
                {
                    com.CommandText = "INSERT INTO client (first_name, last_name, sex, birth_date, status, updated_at) VALUES (@first_name, @last_name, @sex, @birth_date, @status)";
                    com.Parameters.AddWithValue("@first_name", customer.first_name);
                    com.Parameters.AddWithValue("@last_name", customer.last_name);
                    com.Parameters.AddWithValue("@sex", customer.sex);
                    com.Parameters.AddWithValue("@birth_date", customer.birth_date);
                    com.Parameters.AddWithValue("@status", customer.status);

                    await com.ExecuteNonQueryAsync();
                }
            }
        }


        public async Task AddOrUpdateCustomerMySql(List<Customer> customers)
        {
            string server = "sql10.freesqldatabase.com";
            string database = "sql10675811";
            string username = "sql10675811";
            string password = "bBLs1iXr6v";
            MySqlCommand com = new MySqlCommand();

            string connetionString = "Server=" + server + ";Database=" + database + ";Uid=" + username + ";Pwd=" + password + ";";
            using MySqlConnection con = new MySqlConnection(connetionString);

            try
            {
                await con.OpenAsync();

                foreach (var customer in customers)
                {
                    using (MySqlCommand upsertCommand = new MySqlCommand(@"INSERT INTO client (id, first_name, last_name, sex, birth_date, status) 
                                                            VALUES (@id, @first_name, @last_name, @sex, @birth_date, @status)
                                                            ON DUPLICATE KEY UPDATE 
                                                            first_name = VALUES(first_name), 
                                                            last_name = VALUES(last_name), 
                                                            sex = VALUES(sex), 
                                                            birth_date = VALUES(birth_date), 
                                                            status = VALUES(status)", con))
                    {
                        upsertCommand.Parameters.AddWithValue("@id", customer.id);
                        upsertCommand.Parameters.AddWithValue("@first_name", customer.first_name);
                        upsertCommand.Parameters.AddWithValue("@last_name", customer.last_name);
                        upsertCommand.Parameters.AddWithValue("@sex", customer.sex);
                        upsertCommand.Parameters.AddWithValue("@birth_date", customer.birth_date);
                        upsertCommand.Parameters.AddWithValue("@status", 2);

                        await upsertCommand.ExecuteNonQueryAsync();
                    }
                }

                Console.WriteLine($"Successfully inserted/updated {customers.Count} clients in MySQL.");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Something went wrong trying to insert/update the documents in MySQL. Message: {e.Message}");
                Console.WriteLine(e);
            }
        }

    }

}