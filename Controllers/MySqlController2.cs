using AspNetProjMVC.Models;
using MySql.Data.MySqlClient;

namespace AspNetProjMVC.Controllers
{
	public class MySqlController2
	{
		public List<Customer> GetClients()
		{
			List<Customer> customers = new List<Customer>();
			string? connetionString = null;
			string server = "sql10.freesqldatabase.com";
			string database = "sql10675811";
			string username = "sql10675811";
			string password = "bBLs1iXr6v";
			MySqlCommand com = new MySqlCommand();
			MySqlDataReader dr;
			MySqlConnection con = new MySqlConnection();

			connetionString = "Server=" + server + ";Database=" + database + ";Uid=" + username + ";Pwd=" + password + ";";
			con.ConnectionString = connetionString;

			try
			{
				con.Open();
				com.Connection = con;
				com.CommandText = "SELECT id,first_name,last_name,sex,birth_date FROM client";
				dr = com.ExecuteReader();
				while (dr.Read())
				{					
					customers.Add(new Customer()
					{
						id = (int)dr["id"],
						first_name = dr["first_name"].ToString(),
						last_name = dr["last_name"].ToString(),
						sex = dr["sex"].ToString(),
						birth_date = (DateTime)dr["birth_date"]
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
			string server = "sql10.freesqldatabase.com";
			string database = "sql10675811";
			string username = "sql10675811";
			string password = "bBLs1iXr6v";
			MySqlCommand com = new MySqlCommand();
			MySqlDataReader dr;
			MySqlConnection con = new MySqlConnection();

			string? connetionString = "Server=" + server + ";Database=" + database + ";Uid=" + username + ";Pwd=" + password + ";";
			con.ConnectionString = connetionString;

			try
			{
				con.Open();
				com.Connection = con;
				com.CommandText = "SELECT id,first_name,last_name,sex,birth_date FROM client where status = 1";
				dr = com.ExecuteReader();

				while (dr.Read())
				{
					customers.Add(new Customer()
					{
						id = (int)dr["id"],
						first_name = dr["first_name"].ToString(),
						last_name = dr["last_name"].ToString(),
						sex = dr["sex"].ToString(),
						birth_date = (DateTime)dr["birth_date"]
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
			string server = "sql10.freesqldatabase.com";
			string database = "sql10675811";
			string username = "sql10675811";
			string password = "bBLs1iXr6v";
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
					com.CommandText = "UPDATE client set status = 2 where id = @id";
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
			string server = "sql10.freesqldatabase.com";
			string database = "sql10675811";
			string username = "sql10675811";
			string password = "bBLs1iXr6v";
			MySqlCommand com = new MySqlCommand();
			MySqlConnection con = new MySqlConnection();

			string? connetionString = "Server=" + server + ";Database=" + database + ";Uid=" + username + ";Pwd=" + password + ";";
			con.ConnectionString = connetionString;

			try
			{
				con.Open();
				com = con.CreateCommand();
				com.CommandText = "INSERT INTO client (first_name, last_name, sex, birth_date) VALUES (@first_name, @last_name, @sex, @birth_date)";
				com.Parameters.AddWithValue("@first_name", customer.first_name);
				com.Parameters.AddWithValue("@last_name", customer.last_name);
				com.Parameters.AddWithValue("@sex", customer.sex);
				com.Parameters.AddWithValue("@birth_date", customer.birth_date);

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

