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
            _logger.LogInformation("Customer added to Customer's Database");

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> SyncAsync()
        {
            List<Customer> customersMySql = new List<Customer>();
            customersMySql = mySqlController.GetCustomers();
            await mySqlController.UpdateMySqlCustomers(customersMySql);
            await mySqlController2.AddOrUpdateCustomerMySql(customersMySql);
            _logger.LogInformation("Customers synchronized to Clients");

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> DeleteCustomerAsync(int customerId)
        {            
            await mySqlController.DeleteCustomerMySql(customerId);
            _logger.LogInformation($"Customer with ID {customerId} deleted from Customer's Database");

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> DeleteClientAsync(int customerId)
        {
            await mySqlController2.DeleteClientMySql(customerId);
            _logger.LogInformation($"Client with ID {customerId} deleted from Client's Database");

            return RedirectToAction("Index");
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}
