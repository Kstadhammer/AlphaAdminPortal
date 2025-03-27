using Business.Models; // This uses your existing form models

namespace Business.Interfaces;

public interface IClientService
{
    Task<bool> AddClientAsync(AddClientForm form);
    Task<bool> EditClientAsync(EditClientForm form);
    Task<EditClientForm?> GetClientForEditAsync(int id);
    Task<IEnumerable<ClientListItem>> GetAllClientsAsync();
    // Add other methods as needed
}
