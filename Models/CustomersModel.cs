namespace AspNetProjMVC.Models
{
	public class Customer
	{
		public int id { get; set; }
		public string first_name { get; set; }
		public string last_name { get; set; }
		public string sex { get; set; }
		public DateTime birth_date { get; set; }
		public int status { get; set; }
		public DateTime updated_at { get; set; }
	}
}
