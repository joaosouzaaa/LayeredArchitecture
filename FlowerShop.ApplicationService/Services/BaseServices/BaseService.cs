using FlowerShop.CrossCutting.Interfaces.Settings;
using FluentValidation;

namespace FlowerShop.ApplicationService.Services.BaseServices;
public abstract class BaseService<TEntity>
    where TEntity : class
{
    protected readonly INotificationHandler _notificationHandler;
    private readonly IValidator<TEntity> _validator;

    protected BaseService(INotificationHandler notificationHandler, IValidator<TEntity> validator)
    {
        _notificationHandler = notificationHandler;
        _validator = validator;
    }

    protected async Task<bool> ValidateAsync(TEntity entity)
    {
        var validationResult = await _validator.ValidateAsync(entity);

        if (validationResult.IsValid)
            return true;

        foreach (var error in validationResult.Errors)
            _notificationHandler.AddNotification(error.PropertyName, error.ErrorMessage);

        return false;
    }
}
