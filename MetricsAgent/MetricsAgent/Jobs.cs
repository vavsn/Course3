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
            var time = TimeSpan.FromSeconds(DateTimeOffset.UtcNow.ToUnixTimeSeconds());

            // теперь можно записать что-то при помощи репозитория

            _repository.Create(new CpuMetric { Time = time, Value = Usage });

            return Task.CompletedTask;
        }
    }

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
            var time = TimeSpan.FromSeconds(DateTimeOffset.UtcNow.ToUnixTimeSeconds());

            // теперь можно записать что-то при помощи репозитория

            _repRam.Create(new RamMetric { Time = time, Value = Usage });

            return Task.CompletedTask;
        }
    }
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
            var time = TimeSpan.FromSeconds(DateTimeOffset.UtcNow.ToUnixTimeSeconds());

            // теперь можно записать что-то при помощи репозитория

            _repHdd.Create(new HddMetric { Time = time, Value = Usage });

            return Task.CompletedTask;
        }
    }
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
            var time = TimeSpan.FromSeconds(DateTimeOffset.UtcNow.ToUnixTimeSeconds());

            // теперь можно записать что-то при помощи репозитория

            _repNetwork.Create(new NetworkMetric { Time = time, Value = Usage });

            return Task.CompletedTask;
        }
    }

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
            var time = TimeSpan.FromSeconds(DateTimeOffset.UtcNow.ToUnixTimeSeconds());

            // теперь можно записать что-то при помощи репозитория

            _repDN.Create(new DotNetMetric { Time = time, Value = Usage });

            return Task.CompletedTask;
        }
    }

    

}
