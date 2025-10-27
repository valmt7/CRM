namespace CRM.Services.Admin;

public interface IAdminService
{
    Task<Order> SuccessOrder(int orderId);
}