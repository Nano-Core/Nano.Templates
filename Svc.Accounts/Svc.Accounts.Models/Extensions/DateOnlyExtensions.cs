using System;

namespace Svc.Accounts.Models.Extensions;

internal static class DateOnlyExtensions
{
    internal static int GetAge(this DateOnly dateOfBirth)
    {
        var today = DateOnly.FromDateTime(DateTime.Today);
        var age = today.Year - dateOfBirth.Year;

        if (dateOfBirth > today.AddYears(-age))
        {
            age--;
        }

        if (age < 0)
        {
            return 0;
        }

        return age;
    }
}