using MetricsAgent.DAL;
using Quartz;
using System;
using System.Threading.Tasks;
using System.Diagnostics;
using MetricsAgent.DAL.Models;

namespace MetricsAgent.Jobs
{
    public class CpuMetricJob : IJob
    {
        private ICpuMetricsRepository _repository;

        // счетчик для метрики CPU
        private PerformanceCounter _Counter;


        public CpuMetricJob(ICpuMetricsRepository repository)
        {
            _repository = repository;
            _Counter = new PerformanceCounter("Processor", "% Processor Time", "_Total");
        }

        public Task Execute(IJobExecutionContext context)
        {
            // получаем значение занятости CPU
            var Usage = Convert.ToInt32(_Counter.NextValue());

            // узнаем когда мы сняли значение метрики.
            DateTime time = DateTime.Now;

            // теперь можно записать что-то при помощи репозитория

            _repository.Create(new CpuMetric { Time = time, Value = Usage });

            return Task.CompletedTask;
        }
    }

}
