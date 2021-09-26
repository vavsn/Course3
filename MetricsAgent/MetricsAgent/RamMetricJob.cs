using MetricsAgent.DAL;
using Quartz;
using System;
using System.Threading.Tasks;
using System.Diagnostics;
using MetricsAgent.DAL.Models;

namespace MetricsAgent
{
    public class RamMetricJob : IJob
    {
        private IRamMetricsRepository _repRam;

        // счетчик для метрики RAM
        private PerformanceCounter _countRam;


        public RamMetricJob(IRamMetricsRepository repository)
        {
            _repRam = repository;
            _countRam = new PerformanceCounter("Memory", "Available MBytes");
        }

        public Task Execute(IJobExecutionContext context)
        {
            // получаем значение занятости Ram
            var Usage = Convert.ToInt32(_countRam.NextValue());

            // узнаем когда мы сняли значение метрики.
            var time = DateTime.Now;

            // теперь можно записать что-то при помощи репозитория

            _repRam.Create(new RamMetric { Time = time, Value = Usage });

            return Task.CompletedTask;
        }
    }
}
