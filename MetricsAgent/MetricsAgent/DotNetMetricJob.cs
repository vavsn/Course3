using System;
using System.Threading.Tasks;
using MetricsAgent.DAL;
using Quartz;
using System.Diagnostics;
using MetricsAgent.DAL.Models;

namespace MetricsAgent
{
    public class DotNetMetricJob : IJob
    {
        private IDotNetMetricsRepository _repDN;

        // счетчик для метрики DotNet
        private PerformanceCounter _countDN;


        public DotNetMetricJob(IDotNetMetricsRepository repository)
        {
            _repDN = repository;
            _countDN = new PerformanceCounter(".NET CLR Memory", "% Time in GC", "_Global_");
        }

        public Task Execute(IJobExecutionContext context)
        {
            // получаем значение занятости DotNet
            var Usage = Convert.ToInt32(_countDN.NextValue());

            // узнаем когда мы сняли значение метрики.
            var time = DateTime.Now;

            // теперь можно записать что-то при помощи репозитория

            _repDN.Create(new DotNetMetric { Time = time, Value = Usage });

            return Task.CompletedTask;
        }
    }
}
