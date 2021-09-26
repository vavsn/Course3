using FluentMigrator;
using System;

namespace MetricsAgent.DAL.Migrations
{

    [Migration(4)]
    public class FirstMigration : Migration
    {
        private string[] arrDB = new string[] { "cpumetrics", "dotnetmetrics", "hddmetrics", "networkmetrics", "objmetrics", "rammetrics" };

        public override void Up()
        {
            // удалим имеющиеся таблицы
            Down();
            for (int i = 0; i < arrDB.Length; i++)
            {
                if (!Schema.Table(arrDB[i]).Exists())
                {
                    Create.Table(arrDB[i])
                        .WithColumn("Id").AsInt64().PrimaryKey().Identity()
                        .WithColumn("Value").AsInt32()
                        .WithColumn("Time").AsDateTime();
                    for (int j = 1; j <= 6; j++)
                    {
                        Insert.IntoTable(arrDB[i])
                          .Row(new { Id = j, Value = 100 * j, Time = DateTime.Now });
                    }
                }
            }
        }


        public override void Down()
        {
            for (int i = 0; i < arrDB.Length; i++)
            {
                if (Schema.Table(arrDB[i]).Exists())
                    Delete.Table(arrDB[i]);
            }
        }
    }
}
