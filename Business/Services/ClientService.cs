using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Business.Interfaces;
using Business.Models;
using Data.Contexts;
using Data.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

namespace Business.Services;

public class ClientService : IClientService
{
    private readonly DataContext _context;
    private readonly IHostEnvironment _hostEnvironment;

    public ClientService(DataContext context, IHostEnvironment hostEnvironment)
    {
        _context = context;
        _hostEnvironment = hostEnvironment;
    }

    public async Task<bool> AddClientAsync(AddClientForm form)
    {
        try
        {
            var clientEntity = new ClientEntity
            {
                ClientName = form.ClientName,
                Email = form.Email,
                Location = form.Location,
                Phone = form.Phone,
                CreatedAt = DateTime.UtcNow,
                IsActive = true,
            };

            if (form.ClientImage != null)
            {
                var uniqueFileName = $"{Guid.NewGuid()}_{form.ClientImage.FileName}";
                var uploadsFolder = Path.Combine(
                    _hostEnvironment.ContentRootPath,
                    "wwwroot",
                    "uploads",
                    "clients"
                );
                Directory.CreateDirectory(uploadsFolder);
                var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await form.ClientImage.CopyToAsync(fileStream);
                }

                clientEntity.ImageUrl = $"/uploads/clients/{uniqueFileName}";
            }

            _context.Clients.Add(clientEntity);
            await _context.SaveChangesAsync();

            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public async Task<bool> EditClientAsync(EditClientForm form)
    {
        try
        {
            var client = await _context.Clients.FindAsync(form.Id);
            if (client == null)
                return false;

            client.ClientName = form.ClientName;
            client.Email = form.Email;
            client.Location = form.Location;
            client.Phone = form.Phone;
            client.UpdatedAt = DateTime.UtcNow;

            if (form.ClientImage != null)
            {
                // Delete old image if exists
                if (!string.IsNullOrEmpty(client.ImageUrl))
                {
                    var oldImagePath = Path.Combine(
                        _hostEnvironment.ContentRootPath,
                        "wwwroot",
                        client.ImageUrl.TrimStart('/')
                    );
                    if (File.Exists(oldImagePath))
                    {
                        File.Delete(oldImagePath);
                    }
                }

                // Save new image
                var uniqueFileName = $"{Guid.NewGuid()}_{form.ClientImage.FileName}";
                var uploadsFolder = Path.Combine(
                    _hostEnvironment.ContentRootPath,
                    "wwwroot",
                    "uploads",
                    "clients"
                );
                Directory.CreateDirectory(uploadsFolder);
                var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await form.ClientImage.CopyToAsync(fileStream);
                }

                client.ImageUrl = $"/uploads/clients/{uniqueFileName}";
            }

            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public async Task<EditClientForm?> GetClientForEditAsync(int id)
    {
        var client = await _context.Clients.FindAsync(id);
        if (client == null)
            return null;

        return new EditClientForm
        {
            Id = client.Id,
            ClientName = client.ClientName,
            Email = client.Email,
            Location = client.Location,
            Phone = client.Phone,
        };
    }

    public async Task<IEnumerable<ClientListItem>> GetAllClientsAsync()
    {
        return await _context
            .Clients.Select(c => new ClientListItem
            {
                Id = c.Id,
                ClientName = c.ClientName,
                Email = c.Email,
                Location = c.Location,
                Phone = c.Phone,
                ImageUrl = c.ImageUrl,
                IsActive = c.IsActive,
            })
            .ToListAsync();
    }
}
