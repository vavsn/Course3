namespace MetricsManager.Client
{
    //public class MetricsAgentClient : IMetricsAgentClient
    //{
    //    private readonly HttpClient _httpClient;
    //    private readonly ILogger _logger;

    //    public MetricsAgentClient(HttpClient httpClient, ILogger logger)
    //    {
    //        _httpClient = httpClient;
    //        _logger = logger;
    //    }

    //    public AllCpuMetricsApiResponse GetAllCpuMetrics(GetAllCpuMetricsApiRequest request)
    //    {
    //        var fromParameter = request.FromTime.TotalSeconds;
    //        var toParameter = request.ToTime.TotalSeconds;
    //        var httpRequest = new HttpRequestMessage(HttpMethod.Get, $"{request.ClientBaseAddress}/api/cpumetrics/from/{fromParameter}/to/{toParameter}");
    //        try
    //        {
    //            HttpResponseMessage response = httpClient.SendAsync(httprequest).Result;

    //            using var responseStream = response.Content.ReadAsStreamAsync().Result;
    //            return JsonSerializer.DeserializeAsync<AllCpuMetricsApiResponse>(responseStream, new JsonSerializerOptions(JsonSerializerDefault.Web)).Result;
    //        }
    //        catch (Exception ex)
    //        {
    //            logger.LogError(ex.Message);
    //        }
    //    }

    //       return null;
    //}
    // остальные методы реализовать самим
}