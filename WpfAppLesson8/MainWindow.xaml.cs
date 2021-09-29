using LiveCharts;
using LiveCharts.Wpf;
//using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Windows;

namespace WpfAppLesson8
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static HttpClient _client = new HttpClient();
        private static int numColumns = 15; //количество колонок для вывода

        public MainWindow()
        {
            InitializeComponent();

            _client.BaseAddress = new Uri("http://localhost:51353");
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }

        private AllCpuMetricsResponse GetAll()
        {
            var request = new HttpRequestMessage(HttpMethod.Get,
                "http://localhost:51353/cpumetrics");
            HttpResponseMessage response = _client.SendAsync(request).Result;
            var metricsResponse = new AllCpuMetricsResponse() { };
            if (response.IsSuccessStatusCode)
            {
                var responseStream = response.Content.ReadAsStreamAsync().Result;
                metricsResponse = JsonSerializer.DeserializeAsync
                    <AllCpuMetricsResponse>(responseStream, new JsonSerializerOptions(JsonSerializerDefaults.Web)).Result;
            }
            else
            {
                metricsResponse = null;
            }
            return metricsResponse;
        }

        private CpuMetricDto GetLast()
        {
            var request = new HttpRequestMessage(HttpMethod.Get,
                "http://localhost:51353/cpumetrics/getlast");
            HttpResponseMessage response = _client.SendAsync(request).Result;
            var metricResponse = new CpuMetricDto();
            if (response.IsSuccessStatusCode)
            {
                var responseStream = response.Content.ReadAsStreamAsync().Result;
                metricResponse = JsonSerializer.DeserializeAsync
                    <CpuMetricDto>(responseStream, new JsonSerializerOptions(JsonSerializerDefaults.Web)).Result;
            }
            return metricResponse;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            CpuChart.ColumnSeriesValues[0].Values.Clear();

            var metricsResponse = new AllCpuMetricsResponse() { };
            metricsResponse = GetAll();
            metricsResponse.Metrics.Add(GetLast());
            for (int i=0; i <= metricsResponse.Metrics.Count; i++)
            {
                if (i % numColumns == 0 ) 
                {
                    CpuChart.ColumnSeriesValues[0].Values.Add((double)metricsResponse.Metrics[i].Value);
                }
            }
        }
    }
}