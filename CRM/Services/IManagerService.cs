namespace CRM.Services;

public interface IManagerService
{
    Task<IEnumerable<Manager>> GetAllManagers();

    Task<Manager> AddManager(string managerName, string managerEmail, string managerLastName,
        string managerPhone);
}