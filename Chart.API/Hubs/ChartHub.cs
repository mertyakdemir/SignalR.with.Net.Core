using Chart.API.Models;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chart.API.Hubs
{
    public class ChartHub:Hub
    {
        private readonly ChartService _chartService;

        public ChartHub(ChartService chartService)
        {
            _chartService = chartService;
        }

        public async Task GetList()
        {
            await Clients.All.SendAsync("ReceiveList", _chartService.GetChartList());
        }
    }
}
