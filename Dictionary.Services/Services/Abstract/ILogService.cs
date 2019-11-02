using System;
using System.Threading.Tasks;

namespace Dictionary.Services.Services.Abstract
{
    public interface ILogService
    {
        Task WriteLogAsync(Exception exception);

        Task RemoveOldLogsAsync();
    }
}
