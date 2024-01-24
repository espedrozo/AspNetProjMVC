using AspNetProjMVC.Models;
using MySql.Data.MySqlClient;

namespace AspNetProjMVC.Controllers
{
	public class CustomerController
	{
		public Customer BuildCustomer(IFormCollection form)
		{
			Customer customer = new Customer();

			customer.first_name = form["firstName"];
			customer.last_name = form["lastName"];
			customer.sex = form["sex"];
			customer.birth_date = DateTime.Parse(form["birthDate"]);
			customer.status = 1;
			customer.updated_at = DateTime.Now;

			return customer;
		}
	}
}
