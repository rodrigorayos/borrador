using Store.Domain.Models.Common;

namespace Store.Domain.Models.Authorization;

public class AuthorizationModel : BaseModel
{
    public DateTime Date { get; set; }
    public bool State { get; set; }
    public string Description { get; set; }

    public AuthorizationModel(DateTime date, bool state, string description)
    {
        Date = date;
        State = state;
        Description = description;
    }
}