using HW_14.Data;
using HW_14.Filters;
using HW_14.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace HW_14.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationContext _db;
        private List<Currency> _currencies = new()
        {
            new Currency {Name = "UA"},
            new Currency {Name = "USD"},
            new Currency {Name = "EUR"},
            new Currency {Name = "USDT"},
        };

        public HomeController(ApplicationContext db)
        {
            _db = db;
        }

        [CurrencyFilter]
        public IActionResult Index()
        {
            return View(_currencies);
        }

        [HttpPost]
        public async Task<IActionResult> SaveCurrency(string selectedCurrency)
        {
            if(!string.IsNullOrEmpty(selectedCurrency))
            {
                var existingCurrency = await _db.Currencies.FirstOrDefaultAsync();
                if(existingCurrency != null)
                {
                    existingCurrency.Name = selectedCurrency;
                }
                else
                {
                   await _db.Currencies.AddAsync(new Currency { Name = selectedCurrency });
                }
                await _db.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

    }
}
