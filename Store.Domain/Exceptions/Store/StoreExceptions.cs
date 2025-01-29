using Store.Domain.Exceptions.Common;

namespace Store.Domain.Exceptions.Store;

public class StoreNotFoundException : DomainException
{
    public StoreNotFoundException(int storeId)
        : base($"El almacen con el ID: {storeId}, no se ha encontrado.")
    {
    }
}

public class StoreAlreadyExistsException : DomainException
{
    public StoreAlreadyExistsException(string storeName)
        : base($"El almacen con el Nombre: {storeName}, ya existe.")
    {
    }
}