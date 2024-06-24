using Irrigation.Core.ViewModels.View;

namespace Irrigation.Core.Contracts;

public interface INotificationRepository
{
    Task<OperationResult<dynamic>> GetAllAsync(int page, int pageSize);
    Task<OperationResult<NotificationViewModel>> GetByIdAsync(int id);
    Task<OperationResult<int>> InsertAsync(NotificationViewModel model);
    Task<OperationResult> UpdateAsync(NotificationViewModel model);
    Task<OperationResult> DeleteAsync(int id);
}