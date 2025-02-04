namespace Store.Domain.Dtos.Store;

public record AuthorizationDto
(
    int Id,
    DateTime Date,
    bool State,
    string Description
);
public record AuthorizationQueryDto
(
    DateTime Date,
    bool State,
    string Description
);