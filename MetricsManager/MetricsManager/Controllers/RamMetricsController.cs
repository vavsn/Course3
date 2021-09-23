using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using MetricsManager.Responses;

namespace MetricsManager.Controllers
{
    [ApiController]
    [Route("[controller]")]

    public class RamMetricsController : ControllerBase
    {
        private readonly IHttpClientFactory _clientFactory;

        public RamMetricsController(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        //[HttpGet("agent/{agentId}/from/{fromTime}/to/{toTime}")]
        //public IActionResult GetMetricsFromAgent([FromRoute] int agentId, [FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime)
        [HttpGet]
        public IActionResult Get()
        {
            //var client = _clientFactory.CreateClient("RamClient");
            //var request = new HttpRequestMessage(HttpMethod.Get, "http://localhost:51353/api/rammetrics/from/1/to/999999/");
            //request.Headers.Add("Accept", "application/vnd.github.v3+json");
            //HttpResponseMessage response = client.SendAsync(request).Result;

            //if (response.IsSuccessStatusCode)
            //{
            //    using var responseStream = response.Content.ReadAsStringAsync().Result;

            //        // response.Content.ReadAsStringAsync().Result;
            //    var metricsResponse = JsonSerializer.DeserializeAsync
            //        <RamMetricsDto>(responseStream, new JsonSerializerOptions(JsonSerializerDefaults.Web)).Result;
            //    return Ok(metricsResponse);
            //}
            //return BadRequest();
            return Ok();
        }
    }


    //[HttpGet("cluster/from/{fromTime}/to/{toTime}")]
    //    public IActionResult GetMetricsFromAllCluster([FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime)
    //    {
    //        return Ok();
    //    }

}
