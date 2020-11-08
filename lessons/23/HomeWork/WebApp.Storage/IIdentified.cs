using System;

namespace WebApp.Storage
{
    public interface IIdentified
    {
        Guid Id { get; }
    }
}
