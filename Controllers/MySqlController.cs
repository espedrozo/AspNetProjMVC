using AspNetProjMVC.Models;
using MySql.Data.MySqlClient;

namespace AspNetProjMVC.Controllers
{
    public class MySqlController
    {

        private readonly DatabaseController _databaseController;

        public MySqlController()
        {
            _databaseController = new DatabaseController();
        }
        public List<Customer> GetCustomers()
        {
            List<Customer> customers = new List<Customer>();
            using (MySqlConnection con = _databaseController.GetConnection())
            {
                using (MySqlCommand com = new MySqlCommand("SELECT id,first_name,last_name,sex,birth_date,status,updated_at FROM customer", con))
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
                                status = (int)dr["status"],
                                updated_at = (DateTime)dr["updated_at"]
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
            using (MySqlConnection con = _databaseController.GetConnection())
            {
                using (MySqlCommand com = new MySqlCommand("SELECT id,first_name,last_name,sex,birth_date,status,updated_at FROM customer WHERE status = 1", con))
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
                                status = (int)dr["status"],
                                updated_at = (DateTime)dr["updated_at"]
                            });
                        }
                    }
                }
            }

            return customers;
        }

        public async Task UpdateMySqlCustomers(List<Customer> customers)
        {
            using (MySqlConnection con = _databaseController.GetConnection())
            {
                await con.OpenAsync();

                foreach (var customer in customers)
                {
                    using (MySqlCommand com = con.CreateCommand())
                    {
                        com.CommandText = "UPDATE customer SET status = 2 WHERE id = @id";
                        com.Parameters.AddWithValue("@id", customer.id);

                        await com.ExecuteNonQueryAsync();
                    }
                }
            }
        }

        public async Task InsertCustomerMySql(Customer customer)
        {
            using (MySqlConnection con = _databaseController.GetConnection())
            {
                await con.OpenAsync();

                using (MySqlCommand com = con.CreateCommand())
                {
                    com.CommandText = "INSERT INTO customer (first_name, last_name, sex, birth_date, status, updated_at) VALUES (@first_name, @last_name, @sex, @birth_date, @status, @updated_at)";
                    com.Parameters.AddWithValue("@first_name", customer.first_name);
                    com.Parameters.AddWithValue("@last_name", customer.last_name);
                    com.Parameters.AddWithValue("@sex", customer.sex);
                    com.Parameters.AddWithValue("@birth_date", customer.birth_date);
                    com.Parameters.AddWithValue("@status", customer.status);
                    com.Parameters.AddWithValue("@updated_at", customer.updated_at);

                    await com.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task DeleteCustomerMySql(int customerId)
        {
            using (MySqlConnection con = _databaseController.GetConnection())
            {
                await con.OpenAsync();

                using (MySqlCommand com = con.CreateCommand())
                {
                    com.CommandText = "DELETE FROM customer WHERE id = @id";
                    com.Parameters.AddWithValue("@id", customerId);

                    await com.ExecuteNonQueryAsync();
                }
            }
        }
    }
}

