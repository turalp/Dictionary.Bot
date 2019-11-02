using System;
using System.Linq;
using System.Threading.Tasks;
using Dictionary.Domain.Models;
using Dictionary.Domain.Repositories;
using Dictionary.Domain.Repositories.Abstract;
using Dictionary.Services.Services.Abstract;
using Microsoft.EntityFrameworkCore;

namespace Dictionary.Services.Services
{
    public class LogService : ILogService
    {
        private readonly IUnitOfWork _unitOfWork;

        public LogService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task WriteLogAsync(Exception exception)
        {
            if (exception == null)
            {
                throw new ArgumentNullException(nameof(exception));
            }

            var log = new Log
            {
                Message = exception.Message,
                StackTrace = exception.StackTrace,
                ThrownOn = DateTime.Now,
            };

            IRepository<Log> logRepository = new Repository<Log>(_unitOfWork);
            await logRepository.InsertOrUpdateAsync(log);
        }

        public async Task RemoveOldLogsAsync()
        {
            IRepository<Log> logRepository = new Repository<Log>(_unitOfWork);
            var oldLogs = await logRepository
                .GetByCondition(e => e.ThrownOn < DateTime.Now.AddMonths(-3))
                .ToArrayAsync();

            logRepository.DeleteRange(oldLogs);
        }
    }
}
