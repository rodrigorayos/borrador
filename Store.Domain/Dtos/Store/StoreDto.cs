namespace Store.Domain.Dtos.Store;

public record StoreDto(
    int Id, 
    string Name, 
    string Ubication, 
    int Capacity
);
public record StoreQueryDto(
    string Name,
    string Ubication,
    int Capacity
);