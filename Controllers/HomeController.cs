using AspNetProjMVC.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace AspNetProjMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
		private MySqlController mySqlController = new MySqlController();
		private MySqlController2 mySqlController2 = new MySqlController2();
		private CustomerController customerController = new CustomerController();

		public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
			var viewModel = new CustomersViewModel
			{
				CustomersMySql = mySqlController.GetCustomers(),
				CustomersMySql2 = mySqlController2.GetClients()
			};
			return View(viewModel);
		}

		public async Task<IActionResult> AddCustomerAsync()
		{
			Customer customer = customerController.BuildCustomer(Request.Form);

			await mySqlController.InsertCustomerMySql(customer);

			_logger.LogInformation("Customer added to MySql");

			return RedirectToAction("Index");
		}


		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}

	}
}
