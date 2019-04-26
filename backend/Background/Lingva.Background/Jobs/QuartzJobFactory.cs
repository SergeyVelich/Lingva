using Microsoft.Extensions.DependencyInjection;
using Quartz;
using Quartz.Spi;
using System;

namespace Lingva.Background
{
    public class QuartzJobFactory : IJobFactory
    {
        protected readonly IServiceScope _scope;

        public QuartzJobFactory(IServiceProvider container)
        {
            _scope = container.CreateScope();
        }

        public IJob NewJob(TriggerFiredBundle bundle, IScheduler scheduler)
        {
            try
            {
                var res = _scope.ServiceProvider.GetService(bundle.JobDetail.JobType) as IJob;
                return res;
            }
            catch (Exception ex)
            {
                //ERROR-  Cannot resolve 'Quartz.Jobs.EmailJob' from root provider because it 
                //        requires scoped service 'BLL.Base.UnitOfWork.Interfaces.IUnitOfWork'.
                throw;
            }
        }

        public void ReturnJob(IJob job)
        {
            (job as IDisposable)?.Dispose();
        }
    }
}
