using AspNetProjMVC.Models;
using MySql.Data.MySqlClient;

namespace AspNetProjMVC.Controllers
{
	public class MySqlController
	{
		public List<Customer> GetCustomers()
		{
			List<Customer> customers = new List<Customer>();
			string? connetionString = null;
			string server = "sql5.freesqldatabase.com";
			string database = "sql5673207";
			string username = "sql5673207";
			string password = "8PH51R8Euv";
			MySqlCommand com = new MySqlCommand();
			MySqlDataReader dr;
			MySqlConnection con = new MySqlConnection();

			connetionString = "Server=" + server + ";Database=" + database + ";Uid=" + username + ";Pwd=" + password + ";";
			con.ConnectionString = connetionString;

			try
			{
				con.Open();
				com.Connection = con;
				com.CommandText = "SELECT id,first_name,last_name,sex,birth_date,status,updated_at FROM customer";
				dr = com.ExecuteReader();
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
			catch (Exception)
			{
				throw;
			}
		}
		public List<Customer> SyncCustomersMySql()
		{
			List<Customer> customers = new List<Customer>();
			string server = "sql5.freesqldatabase.com";
			string database = "sql5673207";
			string username = "sql5673207";
			string password = "8PH51R8Euv";
			MySqlCommand com = new MySqlCommand();
			MySqlDataReader dr;
			MySqlConnection con = new MySqlConnection();

			string? connetionString = "Server=" + server + ";Database=" + database + ";Uid=" + username + ";Pwd=" + password + ";";
			con.ConnectionString = connetionString;

			try
			{
				con.Open();
				com.Connection = con;
				com.CommandText = "SELECT id,first_name,last_name,sex,birth_date,status,updated_at FROM customer where status = 1";
				dr = com.ExecuteReader();

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
			catch (Exception)
			{
				throw;
			}
		}

		public async Task UpdateMySqlCustomers(List<Customer> customers)
		{
			string server = "sql5.freesqldatabase.com";
			string database = "sql5673207";
			string username = "sql5673207";
			string password = "8PH51R8Euv";
			MySqlCommand com = new MySqlCommand();
			MySqlConnection con = new MySqlConnection();

			string? connetionString = "Server=" + server + ";Database=" + database + ";Uid=" + username + ";Pwd=" + password + ";";
			con.ConnectionString = connetionString;
			foreach (var customer in customers)
			{
				try
				{
					con.Open();
					com = con.CreateCommand();
					com.CommandText = "UPDATE customer set status = 2 where id = @id";
					com.Parameters.AddWithValue("@id", customer.id);

					await com.ExecuteNonQueryAsync();

					con.Close();
				}
				catch (Exception)
				{
					throw;
				}
			}
		}

		public async Task InsertCustomerMySql(Customer customer)
		{
			string server = "sql5.freesqldatabase.com";
			string database = "sql5673207";
			string username = "sql5673207";
			string password = "8PH51R8Euv";
			MySqlCommand com = new MySqlCommand();
			MySqlConnection con = new MySqlConnection();

			string? connetionString = "Server=" + server + ";Database=" + database + ";Uid=" + username + ";Pwd=" + password + ";";
			con.ConnectionString = connetionString;

			try
			{
				con.Open();
				com = con.CreateCommand();
				com.CommandText = "INSERT INTO customer (first_name, last_name, sex, birth_date, status, updated_at) VALUES (@first_name, @last_name, @sex, @birth_date, @status, @updated_at)";
				com.Parameters.AddWithValue("@first_name", customer.first_name);
				com.Parameters.AddWithValue("@last_name", customer.last_name);
				com.Parameters.AddWithValue("@sex", customer.sex);
				com.Parameters.AddWithValue("@birth_date", customer.birth_date);
				com.Parameters.AddWithValue("@status", customer.status);
				com.Parameters.AddWithValue("@updated_at", customer.updated_at);

				await com.ExecuteNonQueryAsync();

				con.Close();
			}
			catch (Exception)
			{
				throw;
			}

		}

	}
}

