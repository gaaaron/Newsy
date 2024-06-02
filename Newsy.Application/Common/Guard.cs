using Newsy.Application.Common.Exceptions;
using Newsy.Domain.Primitives;

namespace Newsy.Application.Common;
public static class Guard
{
    public static void NotFound<T>(T? entity, Guid? id = null) where T : Entity
    {
        if (entity is null)
        {
            if (id is null)
            {
                throw new NotFoundException($"{typeof(T).Name} not found.");
            }

            throw new NotFoundException($"{typeof(T).Name} not found. (Id: {id})");
        }
    }
}
