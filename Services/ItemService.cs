using Fullstack.Data;
using Fullstack.Models;
namespace Fullstack.Services.UserService
{
    public class ItemService : IItemService
    {
        private readonly DataContext context;

        public ItemService(DataContext context)
        {
            this.context = context;
        }

        public async Task<ServiceResponse<Item>> AddItem(Item item)
        {
            ServiceResponse<Item> serviceResponse = new ServiceResponse<Item>();
            try
            {
                context.Items.Add(item);
                context.SaveChanges();


                serviceResponse.Data = item;
            }
            catch (Exception ex)
            {

                serviceResponse.Success = false;
                serviceResponse.Error = ex.Message;
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<List<Item>>> DeleteItem(int id)
        {
            ServiceResponse<List<Item>> serviceResponse = new ServiceResponse<List<Item>>();
            try
            {
                var Item = context.Items.First(item => item.itemId == id);
                context.Items.Remove(Item);
                context.SaveChanges();
                serviceResponse.Data = context.Items.ToList();


            }
            catch (Exception ex)
            {

                serviceResponse.Success = false;
                serviceResponse.Error = ex.Message;
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<List<Item>>> GetAll()
        {

            var serviceResponse = new ServiceResponse<List<Item>>();

            try
            {
                serviceResponse.Data = context.Items.ToList();
            }
            catch (Exception ex)
            {

                serviceResponse.Success = false;
                serviceResponse.Error = ex.Message;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<Item>> GetItemById(int id)
        {
            ServiceResponse<Item> serviceResponse = new ServiceResponse<Item>();
            try
            {
                var item = context.Items.First(item => item.itemId == id);
                serviceResponse.Data = item;
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Error = ex.Message;
            }

            return serviceResponse;
        }

        public Task<ServiceResponse<List<Item>>> GetItemByUser(int id)
        {
            throw new NotImplementedException();
        }


        Task<ServiceResponse<Item>> IItemService.GetItemByUser(int id)
        {
            throw new NotImplementedException();
        }

        Task<ServiceResponse<List<Item>>> IItemService.ItemByFlavor(Flavor FlavorType)
        {
            throw new NotImplementedException();
        }

        public async Task<ServiceResponse<Item>> UpdateItem(Item item)
        {
            ServiceResponse<Item> serviceResponse = new ServiceResponse<Item>();
            try
            {
                Item updatedItem = context.Items.First(item => item.itemId == item.itemId);
                updatedItem.itemName = item.itemName;
                updatedItem.Flavor = item.id;
                updatedItem.description = item.description;
                updatedItem.quantity = item.quantity;
                context.Items.Update(updatedItem);
                context.SaveChanges();
                serviceResponse.Data = item;
            }
            catch (Exception ex)
            {

                serviceResponse.Success = false;
                serviceResponse.Error = ex.Message;
            }

            return serviceResponse;
        }
    }
}