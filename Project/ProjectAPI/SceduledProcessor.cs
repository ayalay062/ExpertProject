using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NCrontab;
using Project.BL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ProjectAPI
{
    public class StartupHostedService : IHostedService, IDisposable
    {
        private readonly IServiceProvider _serviceProvider;
        private Timer _timer;
      
        public StartupHostedService(IServiceProvider serviceProvider)
        {

            _serviceProvider = serviceProvider;
        }
       
      
        public void Dispose()
        {
            _timer?.Dispose();
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            TimeSpan scheduledTime = TimeSpan.FromHours(0); // midnight
            TimeSpan minimumIntervalBetweenStarts = TimeSpan.FromHours(12);

            while (true)
            {
                var scheduledDelay = scheduledTime - DateTime.Now.TimeOfDay;

                while (scheduledDelay < TimeSpan.Zero)
                    scheduledDelay += TimeSpan.FromDays(1);

                await Task.Delay(scheduledDelay, cancellationToken);

                var delayBetweenStarts =
                    Task.Delay(minimumIntervalBetweenStarts, cancellationToken);

                using (IServiceScope scope = _serviceProvider.CreateScope())
                {
                    var scopedProcessingService =
                        scope.ServiceProvider.GetRequiredService<IRecommendsBL>();

                     scopedProcessingService.SendInviteAfterMeeting();

                  //  _recommendBL.SendInviteAfterMeeting();
                }


              
                //  await ProcessAsync();

                await delayBetweenStarts;
            }
        }
        public Task StopAsync(CancellationToken cancellationToken)
        {
            _timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }

        private Task Process()
        {
            try
            {
                // perform some database operations
            }
            catch (Exception e)
            {
                //_logger.LogError(e, e.Message);
            }
            return Task.CompletedTask;
        }
    }

}