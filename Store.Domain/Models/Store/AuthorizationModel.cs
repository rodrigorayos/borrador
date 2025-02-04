using Store.Domain.Models.Common;

namespace Store.Domain.Models.Authorization;

public class AuthorizationModel : BaseModel
{
    public DateTime Date { get; private set; }
    public bool State { get; private set; }
    public string Description { get; private set; }

    public AuthorizationModel(DateTime date, bool state, string description)
    {
        Date = date;
        State = state;
        Description = description;
    }
}