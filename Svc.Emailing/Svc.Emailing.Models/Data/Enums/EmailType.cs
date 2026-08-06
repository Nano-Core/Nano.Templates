namespace Svc.Emailing.Models.Data.Enums;

/// <summary>
/// Email Type.
/// </summary>
public enum EmailType
{
    /// <summary>
    /// None.
    /// </summary>
    None = 0,

    /// <summary>
    /// Welcome Email.
    /// </summary>
    Welcome = 1,

    /// <summary>
    /// Forgot Password.
    /// </summary>
    ForgotPassword = 2,

    /// <summary>
    /// Change Email.
    /// </summary>
    ChangeEmail = 3,

    /// <summary>
    /// Confirm Email.
    /// </summary>
    ConfirmEmail = 4,

    /// <summary>
    /// Welcome Again Email.
    /// </summary>
    WelcomeAgain = 5
}