using Chart.API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chart.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class ChartController : ControllerBase
    {
        private readonly ChartService _service;

        public ChartController(ChartService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> SaveChart(ChartModel chart)
        {
            await _service.SaveChart(chart);

            //IQueryable<ChartModel> chartList = _service.GetList();

            return Ok(_service.GetChartList());
        }

        [HttpGet]
        public IActionResult InitializeChart()
        {
            Random rnd = new Random();

            Enumerable.Range(1, 10).ToList().ForEach(x =>
            {
                foreach (ECity item in Enum.GetValues(typeof(ECity)))
                {
                    var newChart = new ChartModel { City = item, Count = rnd.Next(100, 1000), CDateTime = DateTime.Now.AddDays(x) };

                    _service.SaveChart(newChart).Wait();

                    System.Threading.Thread.Sleep(1000);
                }
            });

            return Ok("Successfull");
        }
    }
}
