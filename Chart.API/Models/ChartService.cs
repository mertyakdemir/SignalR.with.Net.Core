using Chart.API.Hubs;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chart.API.Models
{
    public class ChartService
    {
        private readonly AppDbContext _context;
        private readonly IHubContext<ChartHub> _hubContext;

        public ChartService(AppDbContext context, IHubContext<ChartHub> hubContext)
        {
            _context = context;
            _hubContext = hubContext;
        }

        public IQueryable<ChartModel> GetList()
        {
            return _context.Charts.AsQueryable();
        }

        public async Task SaveChart(ChartModel chart)
        {
            await _context.Charts.AddAsync(chart);
            await _context.SaveChangesAsync();
            await _hubContext.Clients.All.SendAsync("ReceiveList", GetChartList());

        }

        public List<ChartPivotModel> GetChartList()
        {
            List<ChartPivotModel> chart = new List<ChartPivotModel>();

            using (var command = _context.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "select tarih,[1],[2],[3],[4],[5]  FROM(select[City],[Count], Cast([CDateTime] as date) as tarih FROM Charts) as charTbl PIVOT (SUM(Count) For City IN([1],[2],[3],[4],[5]) ) as ptable order by tarih asc";

                command.CommandType = System.Data.CommandType.Text;

                _context.Database.OpenConnection();

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        ChartPivotModel cc = new ChartPivotModel();

                        cc.ChartDate = reader.GetDateTime(0).ToShortDateString();

                        Enumerable.Range(1, 5).ToList().ForEach(x =>
                        {
                            if (System.DBNull.Value.Equals(reader[x]))
                            {
                                cc.Counts.Add(0);
                            }
                            else
                            {
                                cc.Counts.Add(reader.GetInt32(x));
                            }
                        });

                        chart.Add(cc);
                    }
                }

                _context.Database.CloseConnection();

                return chart;
            }
        }
    }
}
