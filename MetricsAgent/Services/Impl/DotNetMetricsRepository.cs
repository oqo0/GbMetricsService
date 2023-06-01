using Dapper;
using MetricsAgent.Models;
using Microsoft.Extensions.Options;
using System.Data.SQLite;

namespace MetricsAgent.Services.Impl
{
    public class DotNetMetricsRepository : IDotNetMetricsRepository
    {
        #region Services

        private readonly IOptions<DatabaseOptions> _databaseOptions;

        #endregion

        public DotNetMetricsRepository(IOptions<DatabaseOptions> databaseOptions)
        {
            _databaseOptions = databaseOptions;
        }

        public void Create(DotNetMetric item)
        {
            using var connection = new SQLiteConnection(_databaseOptions.Value.ConnectionString);
            connection.Open();
            connection.Execute("INSERT INTO dotnetmetrics(value, time) VALUES(@value, @time)", new
            {
                value = item.Value,
                time = item.Time
            });
        }

        public void Delete(int id)
        {
            using var connection = new SQLiteConnection(_databaseOptions.Value.ConnectionString);
            connection.Open();
            connection.Execute("DELETE FROM dotnetmetrics WHERE id=@id",
            new
            {
                id = id
            });
        }

        public void Update(DotNetMetric item)
        {
            using var connection = new SQLiteConnection(_databaseOptions.Value.ConnectionString);
            connection.Execute("UPDATE dotnetmetrics SET value = @value, time = @time WHERE id = @id",
            new
            {
                value = item.Value,
                time = item.Time,
                id = item.Id
            });
        }

        public IList<DotNetMetric> GetAll()
        {
            using var connection = new SQLiteConnection(_databaseOptions.Value.ConnectionString);
            return connection.Query<DotNetMetric>("SELECT Id, Time, Value FROM dotnetmetrics").ToList();
        }

        public DotNetMetric GetById(int id)
        {
            using var connection = new SQLiteConnection(_databaseOptions.Value.ConnectionString);
            DotNetMetric metric = connection.QuerySingle<DotNetMetric>("SELECT Id, Time, Value FROM dotnetmetrics WHERE id = @id",
            new { id = id });
            return metric;
        }

        public IList<DotNetMetric> GetByTimePeriod(TimeSpan timeFrom, TimeSpan timeTo)
        {
            using var connection = new SQLiteConnection(_databaseOptions.Value.ConnectionString);
            List<DotNetMetric> metrics = connection.Query<DotNetMetric>("SELECT * FROM dotnetmetrics where time >= @timeFrom and time <= @timeTo",
               new { timeFrom = timeFrom.TotalSeconds, timeTo = timeTo.TotalSeconds }).ToList();
            return metrics;
        }
    }
}
