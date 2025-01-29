using Store.Domain.Models.Common;

namespace Store.Domain.Models.Store;

public class StoreModel : BaseModel
{
    public string Name { get; set; }
    public string Ubication { get; set; }
    public int Capacity { get; set; }
}