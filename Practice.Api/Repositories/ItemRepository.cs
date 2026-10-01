using Practice.Api.Models;

namespace Practice.Api.Repositories
{
    public sealed class ItemRepository
    {
        private readonly List<Item> _items = new();

        public Item Add(string title)
        {
            var item = new Item(Guid.NewGuid(), title, false);
            _items.Add(item);
            return item;
        }

        public Item[] GetAll()
        {
            return _items.ToArray();
        }
    }
}
