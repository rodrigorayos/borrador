using Store.Domain.Models.Common;

namespace Store.Domain.Models.Store;

public class StoreModel : BaseModel
{
    public string Name { get; private set; }
    public string Ubication { get; private set; }
    public int Capacity { get; private set; }

    public StoreModel(string name, string ubication, int capacity)
    {
        Name = name;
        Ubication = ubication;
        Capacity = capacity;
    }
}