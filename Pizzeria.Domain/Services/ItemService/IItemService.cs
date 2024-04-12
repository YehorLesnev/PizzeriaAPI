using Microsoft.AspNetCore.Http;
using Pizzeria.Domain.Models;

namespace Pizzeria.Domain.Services.ItemService
{
    public interface IItemService : IBaseService<Item>
    {
        Task<string> SaveItemImageAsync(IFormFile image, string folderName, string filename);
    }
}
