using System;
using Svc.Emailing.Models.Data.Enums;

namespace Svc.Emailing.Eventing.Extensions;

internal static class EmailTypeExtensions
{
    internal static string GetEmailTemplate(this EmailType emailType)
    {
        return emailType switch
        {
            EmailType.None => "",
            EmailType.Welcome => "welcome",
            EmailType.ForgotPassword => "forgot-password",
            EmailType.ChangeEmail => "change-email",
            EmailType.ConfirmEmail => "confirm-email",
            EmailType.WelcomeAgain => "welcome-again",
            _ => throw new ArgumentOutOfRangeException(nameof(emailType), emailType, null)
        };
    }
}