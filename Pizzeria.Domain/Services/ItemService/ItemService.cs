using Microsoft.AspNetCore.Http;
using Pizzeria.Domain.Models;
using Pizzeria.Domain.Repository.ItemRepository;

namespace Pizzeria.Domain.Services.ItemService
{
    public class ItemService(IItemRepository itemRepository)
        : BaseService<Item>(itemRepository), IItemService
    {
        public async Task<string> SaveItemImageAsync(IFormFile image, string folderName, string filename)
        {
            if(image is null) throw new ArgumentNullException(nameof(image));
            
            var fileExtension = Path.GetExtension(image.FileName);

            if(!fileExtension.Equals(".webp")) throw new ArgumentException($"Bad file extension: {nameof(image)}. Upload .webp file");

            filename = filename.Replace(' ', '_');

            var fileFullName = filename + fileExtension;

            var pathToSave = Path.Combine("static/images/items", folderName);

            if(!Directory.Exists(pathToSave))
            {
                Directory.CreateDirectory(pathToSave);
            }

            var fullPath = Path.Combine(pathToSave, fileFullName);

            await using(var stream = new FileStream(fullPath, FileMode.Create))
            {
                await image.CopyToAsync(stream);
            }

            var relativePath = Path.Combine(folderName, fileFullName).Replace('\\', '/');

            return relativePath;
        }
    }
}
