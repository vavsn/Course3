using System;
using System.Threading.Tasks;
using MetricsAgent.DAL;
using Quartz;
using System.Diagnostics;
using MetricsAgent.DAL.Models;

namespace MetricsAgent
{
    public class HddMetricJob : IJob
    {
        private IHddMetricsRepository _repHdd;

        // счетчик для метрики HDD
        private PerformanceCounter _countHdd;


        public HddMetricJob(IHddMetricsRepository repository)
        {
            _repHdd = repository;
            _countHdd = new PerformanceCounter("PhysicalDisk", "Disk Bytes/sec", "_Total");
        }

        public Task Execute(IJobExecutionContext context)
        {
            // получаем значение 
            var Usage = Convert.ToInt32(_countHdd.NextValue());

            // узнаем когда мы сняли значение метрики.
            var time = DateTime.Now;

            // теперь можно записать что-то при помощи репозитория

            _repHdd.Create(new HddMetric { Time = time, Value = Usage });

            return Task.CompletedTask;
        }
    }
}
