using Entities.Models;

namespace Contracts;

public interface IDataShaper<T>
{
    IEnumerable<Entity> ShapeData(IEnumerable<T> entities, string fieldsString);
    Entity ShapeData(T enitity, string fieldsString);
}

