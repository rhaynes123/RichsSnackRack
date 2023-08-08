using System;
using System.Data.Common;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace RichsSnackRack.Persistence
{
	public sealed class PerformanceInterceptor: DbCommandInterceptor
	{
        private readonly ILogger<PerformanceInterceptor> _logger;
		public PerformanceInterceptor(ILogger<PerformanceInterceptor> logger)
		{
            _logger = logger;
		}

        public override ValueTask<DbDataReader> ReaderExecutedAsync(DbCommand command
            , CommandExecutedEventData eventData
            , DbDataReader result
            , CancellationToken cancellationToken = default)
        {
            if(eventData.Duration.TotalMilliseconds > 100)
            {
                _logger.LogWarning("Query too slow");
            }
            _logger.LogInformation(message: "Query Time {TotalMilliseconds}", eventData.Duration.TotalMilliseconds);
            return base.ReaderExecutedAsync(command, eventData, result, cancellationToken);
        }
    }
}

