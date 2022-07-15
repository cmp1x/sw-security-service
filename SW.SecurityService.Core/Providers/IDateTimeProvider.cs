using System;

namespace SW.SecurityService.Core.Providers
{
    public interface IDateTimeProvider
    {
        DateTime Now();
    }
}