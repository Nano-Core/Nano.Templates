using Svc.Accounts.Models.Data;
using System;

namespace Api.Public.Responses.Accounts;

/// <summary>
/// User Response.
/// </summary>
public class UserResponse
{
    /// <summary>
    /// Id.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// First Name.
    /// </summary>
    public string FirstName { get; set; }

    /// <summary>
    /// Last Name.
    /// </summary>
    public string LastName { get; set; }

    /// <summary>
    /// Email Address.
    /// </summary>
    public string EmailAddress { get; set; }

    /// <summary>
    /// Is Email Address Confirmed.
    /// </summary>
    public bool IsEmailAddressConfirmed { get; set; }

    /// <summary>
    /// Phone Number.
    /// </summary>
    public string? PhoneNumber { get; set; }

    /// <summary>
    /// Is Phone Number Confirmed.
    /// </summary>
    public bool IsPhoneNumberConfirmed { get; set; }

    /// <summary>
    /// Date of Birth.
    /// </summary>
    public DateOnly DateOfBirth { get; set; }

    /// <summary>
    /// Age.
    /// </summary>
    public int Age { get; set; }

    /// <summary>
    /// Created At.
    /// </summary>
    public DateTimeOffset CreatedAt { get; set; }

    /// <summary>
    /// Address.
    /// </summary>
    public AddressResponse Address { get; set; }

    /// <summary>
    /// Gender.
    /// </summary>
    public GenderResponse Gender { get; set; }

    /// <summary>
    /// Language.
    /// </summary>
    public LanguageResponse Language { get; set; }

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="user">The <see cref="User"/>.</param>
    public UserResponse(User user)
    {
        ArgumentNullException.ThrowIfNull(user);

        this.Id = user.Id;
        this.FirstName = user.FirstName;
        this.LastName = user.LastName;
        this.EmailAddress = user.IdentityUser.Email!;
        this.IsEmailAddressConfirmed = user.IdentityUser.EmailConfirmed;
        this.PhoneNumber = user.IdentityUser.PhoneNumber;
        this.IsPhoneNumberConfirmed = user.IdentityUser.PhoneNumberConfirmed;
        this.DateOfBirth = user.DateOfBirth;
        this.Age = user.Age;
        this.CreatedAt = user.CreatedAt;
        this.Address = new AddressResponse(user.Address);
        this.Gender = new GenderResponse(user.Gender);
        this.Language = new LanguageResponse(user.Language);
    }
}