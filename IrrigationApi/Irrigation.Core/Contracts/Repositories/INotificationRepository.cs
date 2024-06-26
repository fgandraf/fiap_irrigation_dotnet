using Irrigation.Core.ViewModels.Create;
using Irrigation.Core.ViewModels.Update;
using Irrigation.Core.ViewModels.View;

namespace Irrigation.Core.Contracts.Repositories;

public interface INotificationRepository
{
    Task<OperationResult<ListView>> GetAllAsync(int page, int pageSize);
    Task<OperationResult<NotificationView>> GetByIdAsync(int id);
    Task<OperationResult<int>> InsertAsync(NotificationCreate model);
    Task<OperationResult> UpdateAsync(NotificationUpdate model);
    Task<OperationResult> DeleteAsync(int id);
}