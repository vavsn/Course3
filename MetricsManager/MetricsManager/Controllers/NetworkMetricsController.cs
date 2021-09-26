using Microsoft.AspNetCore.Mvc;
using System;
using Microsoft.Extensions.Logging;
using System.Net.Http;
using System.Text.Json;
using MetricsManager.Responses;

namespace MetricsManager.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class NetworkMetricsController : ControllerBase
    {
        private readonly ILogger<NetworkMetricsController> _logger;
        private readonly IHttpClientFactory _clientFactory;

        public NetworkMetricsController(ILogger<NetworkMetricsController> logger, IHttpClientFactory clientFactory)
        {
            _logger = logger;
            _logger.LogDebug(1, "NLog встроен в NetworkMetricsController");
            _clientFactory = clientFactory;
        }

        [HttpGet("agent/{Id}")]
        public IActionResult GetMetricsFromAgent(
            [FromRoute] int Id)
        {
            var request = new HttpRequestMessage(HttpMethod.Get,
                "http://localhost:51353/networkmetrics/getbyid/245");
            var client = _clientFactory.CreateClient();
            HttpResponseMessage response = client.SendAsync(request).Result;
            if (response.IsSuccessStatusCode)
            {
                using var responseStream = response.Content.ReadAsStreamAsync().Result;
                var metricsResponse = JsonSerializer.DeserializeAsync
                    <NetworkMetricDto>(responseStream, new JsonSerializerOptions(JsonSerializerDefaults.Web)).Result;
                return Ok(metricsResponse);
            }
            else
            {
                // ошибка при получении ответа
                return BadRequest();
            }

        }

        [HttpGet]
        public IActionResult Get()
        {
            var request = new HttpRequestMessage(HttpMethod.Get,
            "http://localhost:51353/NetworkMetrics/");
            var client = _clientFactory.CreateClient();
            HttpResponseMessage response = client.SendAsync(request).Result;
            if (response.IsSuccessStatusCode)
            {
                using var responseStream = response.Content.ReadAsStreamAsync().Result;
                var metricsResponse = JsonSerializer.DeserializeAsync
                    <AllNetworkMetricsResponse>(responseStream, new JsonSerializerOptions(JsonSerializerDefaults.Web)).Result;
                return Ok(metricsResponse);
            }
            else
            {
                // ошибка при получении ответа
            }
            return Ok();
        }

        [HttpGet("cluster/from/{fromTime}/to/{toTime}")]
        public IActionResult GetMetricsFromAllCluster([FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime)
        {
            return Ok();
        }

    }
}
