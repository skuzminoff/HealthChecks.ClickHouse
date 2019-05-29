using System;
using System.Threading;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.Threading.Tasks;
using ClickHouse.Net;

namespace HealthChecks.Clickhouse
{
    public class ClickHouseHealthCheck : IHealthCheck
    {
        private readonly IClickHouseDatabase _database;

        public ClickHouseHealthCheck(IClickHouseDatabase database)
        {
            _database = database ?? throw new ArgumentNullException(nameof(database));
        }

        public Task<HealthCheckResult> CheckHealthAsync(
            HealthCheckContext context, CancellationToken cancellationToken)
        {
            try
            {
                var result = _database.DatabaseExists("default");

                return result 
                    ? Task.FromResult(HealthCheckResult.Healthy()) 
                    : Task.FromResult(HealthCheckResult.Unhealthy());
            }
            catch (AccessViolationException ex)
            {
                var checkResult = new HealthCheckResult(
                    context.Registration.FailureStatus,
                    description: "exception while clickhouse health check",
                    exception: ex,
                    data: null);
                return Task.FromResult(checkResult);
            }
        }
    }
}
