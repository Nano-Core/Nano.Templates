using DynamicExpression;
using Nano.Basic.Models.Enums;
using Nano.Models.Criterias;

namespace Nano.Basic.Models.Criterias
{
    /// <inheritdoc />
    public class PersonQueryCriteria : DefaultQueryCriteria
    {
        /// <summary>
        /// Name.
        /// </summary>
        public virtual string Name { get; set; }

        /// <summary>
        /// Phone Number.
        /// </summary>
        public virtual string PhoneNumber { get; set; }

        /// <summary>
        /// Email Address.
        /// </summary>
        public virtual string EmailAddress { get; set; }

        /// <summary>
        /// Gender.
        /// </summary>
        public virtual Gender? Gender { get; set; }

        /// <inheritdoc />
        public override CriteriaExpression GetExpression<TEntity>()
        {
            var filter = base.GetExpression<TEntity>();

            if (this.Name != null)
                filter.StartsWith("Name", this.Name);

            if (this.Gender != null)
                filter.Equal("Gender", this.Gender);

            if (this.PhoneNumber != null)
                filter.Equal("PhoneNumber.Number", this.PhoneNumber);

            if (this.EmailAddress != null)
                filter.Equal("EmailAddress.Email", this.EmailAddress);

            return filter;
        }
    }
}