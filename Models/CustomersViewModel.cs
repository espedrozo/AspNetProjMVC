namespace AspNetProjMVC.Models
{
	public class CustomersViewModel : Customer
	{
		public List<Customer> CustomersMySql { get; set; }
		public List<Customer> CustomersMySql2 { get; set; }
        public CustomersViewModel()
        {
            CustomersMySql  = new List<Customer>();
            CustomersMySql2 = new List<Customer>();
        }
    }

}
