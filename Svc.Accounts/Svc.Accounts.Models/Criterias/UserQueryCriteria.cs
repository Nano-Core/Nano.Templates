using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using DynamicExpression;
using DynamicExpression.Enums;
using Nano.App.Api.Controllers.Criteria;
using Nano.Data.Abstractions.Models.Identity;
using Svc.Accounts.Models.Data;

namespace Svc.Accounts.Models.Criterias;

/// <inheritdoc />
public class UserQueryCriteria : BaseQueryCriteria
{
    /// <summary>
    /// Full Name.
    /// </summary>
    public virtual string? FullName { get; set; }

    /// <summary>
    /// Email Address.
    /// </summary>
    [EmailAddress]
    public virtual string? EmailAddress { get; set; }

    /// <summary>
    /// Phone Number.
    /// </summary>
    [Phone]
    public virtual string? PhoneNumber { get; set; }

    /// <summary>
    /// Is Active.
    /// </summary>
    public virtual bool? IsActive { get; set; }

    /// <summary>
    /// Keyword.
    /// </summary>
    public virtual string? Keyword { get; set; }

    /// <inheritdoc />
    public override IList<CriteriaExpression> GetExpressions()
    {
        var expressions = base.GetExpressions();

        var expression = expressions.FirstOrDefault() ?? new CriteriaExpression();

        if (!string.IsNullOrEmpty(this.FullName))
        {
            expression
                .StartsWith(nameof(User.FullNameNormalized), this.FullName.ToUpper());
        }

        if (!string.IsNullOrEmpty(this.EmailAddress))
        {
            expression
                .Equal($"{nameof(User.IdentityUser)}.{nameof(User.IdentityUser.Email)}", this.EmailAddress);
        }

        if (!string.IsNullOrEmpty(this.PhoneNumber))
        {
            expression
                .Equal($"{nameof(User.IdentityUser)}.{nameof(User.IdentityUser.PhoneNumber)}", this.PhoneNumber);
        }

        if (this.IsActive.HasValue)
        {
            expression
                .Equal($"{nameof(User.IdentityUser)}.{nameof(IdentityUserEx<>.IsActive)}", this.IsActive);
        }

        if (!string.IsNullOrEmpty(this.Keyword))
        {
            var keywordExpression = new CriteriaExpression();

            keywordExpression
                .Contains(nameof(User.FullNameNormalized), this.Keyword.ToUpper(), LogicalType.Or);

            keywordExpression
                .Equal($"{nameof(User.IdentityUser)}.{nameof(User.IdentityUser.NormalizedEmail)}", this.Keyword.ToUpper());

            expressions
                .Add(keywordExpression);
        }

        expressions
            .Add(expression);

        return expressions;
    }
}