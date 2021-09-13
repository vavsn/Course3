using MetricsAgent.DAL;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Data.SQLite;


namespace MetricsAgent
{
    public class Startup
    {
        private string[] arrDB = new string[] { "cpumetrics", "dotnetmetrics", "hddmetrics", "networkmetrics", "objmetrics", "rammetrics" };
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            ConfigureSqlLiteConnection(services);
            services.AddScoped<ICpuMetricsRepository, CpuMetricsRepository>();
            services.AddScoped<IDotNetMetricsRepository, DotNetMetricsRepository>();
            services.AddScoped<IHddMetricsRepository, HddMetricsRepository>();
            services.AddScoped<INetworkMetricsRepository, NetworkMetricsRepository>();
            services.AddScoped<IObjMetricsRepository, ObjMetricsRepository>();
            services.AddScoped<IRamMetricsRepository, RamMetricsRepository>();
        }

        private void ConfigureSqlLiteConnection(IServiceCollection services)
        {
            const string connectionString = "Data Source=metrics.db;Version=3;Pooling=true;Max Pool Size=100;";
            var connection = new SQLiteConnection(connectionString);
            connection.Open();
            PrepareSchema(connection);
        }

        private void PrepareSchema(SQLiteConnection connection)
        {
            using (var command = new SQLiteCommand(connection))
            {
                for(int i=0; i< arrDB.Length;i++)
                {
                    // ������ ����� ����� ������� ��� ����������
                    // ������� ������� � ��������� ���� ��� ���������� � ���� ������
                    command.CommandText = $"DROP TABLE IF EXISTS {arrDB[i]}";
                    // ���������� ������ � ���� ������
                    command.ExecuteNonQuery();

                    command.CommandText = $"CREATE TABLE {arrDB[i]}(id INTEGER PRIMARY KEY, value INT, time INT)";
                    command.ExecuteNonQuery();

                    // ����������� � ������� SQL ������ �� ������� ������
                    command.CommandText = $"INSERT INTO {arrDB[i]}(value, time) VALUES(@value, @time)";

                    // ��������� ��������� � ������ �� ������ �������
                    command.Parameters.AddWithValue("@value", 1);

                    // � ������� ����� ������� ����� � ��������, ������ ����������� ����� ������� � �������
                    // ����� ��������
                    command.Parameters.AddWithValue("@time", 100);
                    // ���������� ������� � ����������
                    command.Prepare();

                    // ���������� ������ � ���� ������
                    command.ExecuteNonQuery();
                }
            }
        }
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
