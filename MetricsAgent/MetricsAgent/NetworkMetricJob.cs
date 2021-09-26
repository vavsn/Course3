using System;
using System.Threading.Tasks;
using MetricsAgent.DAL;
using Quartz;
using System.Diagnostics;
using MetricsAgent.DAL.Models;

namespace MetricsAgent
{
    public class NetworkMetricJob : IJob
    {
        private INetworkMetricsRepository _repNetwork;

        // счетчик для метрики Network
        private PerformanceCounter _countNetwork;


        public NetworkMetricJob(INetworkMetricsRepository repository)
        {
            _repNetwork = repository;
            _countNetwork = new PerformanceCounter("Network Interface", "Current Bandwidth", "Atheros AR8151 PCI-E Gigabit Ethernet Controller [NDIS 6.20]");
        }

        public Task Execute(IJobExecutionContext context)
        {
            // получаем значение 
            var Usage = Convert.ToInt32(_countNetwork.NextValue());

            // узнаем когда мы сняли значение метрики.
            var time = DateTime.Now;

            // теперь можно записать что-то при помощи репозитория

            _repNetwork.Create(new NetworkMetric { Time = time, Value = Usage });

            return Task.CompletedTask;
        }
    }
}
