﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Shopping.Client.Models;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Shopping.Client.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly HttpClient httpClient;

        public HomeController(ILogger<HomeController> logger, IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            httpClient = httpClientFactory.CreateClient("ShoppingAPIClient");
        }

        public async Task<IActionResult> Index()
        {
            var response = await httpClient.GetAsync("/product");
            var content = await response.Content.ReadAsStringAsync();
            var productList = JsonSerializer.Deserialize<IEnumerable<Product>>(content);

            return View(productList);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}