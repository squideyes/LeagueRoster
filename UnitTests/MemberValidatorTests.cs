using AL.LeagueRoster.Common;
using System;
using System.Collections.Generic;
using Xunit;
using System.Linq;
using FluentValidation;
using FluentValidation.TestHelper;
using System.Linq.Expressions;
using FluentValidation.Validators;

namespace AL.LeagueRoster.UnitTests
{
    public class MemberValidatorTests
    {
        [Theory]
        [InlineData(CountryCode.US, "12345")]
        [InlineData(CountryCode.MX, "12345")]
        [InlineData(CountryCode.CA, "A1A 1B1")]
        [InlineData(CountryCode.GB, "EC4Y 8AP")]
        public void ConstructorWithGoodArgs(
            CountryCode countryCode, string postalCode)
        {
            var member = new Member()
            {
                Address1 = "123 Some Street",
                Address2 = "Apt. 1A",
                ClubId = 12345,
                CountryCode = countryCode,
                EmailAddress = "someone@someco.com",
                FirstName = "Some",
                LastName = "Dude",
                Initial = 'X',
                IsAlcor = true,
                Locality = "Some Town",
                PostalCode = postalCode,
                MemberId = Guid.NewGuid(),
                NoAdvertising = true,
                Reflector = Reflector.PrintAndEmail,
                Region = "PA",
                Roles = new List<Role> { Role.Treasurer, Role.Webmaster },
                UpdatedOn = DateTime.UtcNow
            };

            var validator = new MemberValidator();

            var _ = validator.Validate(member, x => x.ThrowOnFailures());
        }

        [Theory]
        [InlineData((CountryCode)0, "12345")]
        [InlineData((CountryCode)0, "A1A-1B1")]
        [InlineData(CountryCode.US, "1234X")]
        [InlineData(CountryCode.MX, "1234X")]
        [InlineData(CountryCode.CA, "A1A-1B1")]
        public void BadCountryCodeAndOrPostalCode(
            CountryCode countryCode, string postalCode)
        {
            var member = new Member()
            {
                CountryCode = countryCode,
                PostalCode = postalCode
            };

            var validator = new MemberValidator();

            var result = validator.TestValidate(member);

            if (!Enum.IsDefined(countryCode))
            {
                result.ShouldHaveValidationErrorFor(m => m.CountryCode)
                    .WithErrorCode("EnumValidator");
            }

            result.ShouldHaveValidationErrorFor(m => m.PostalCode)
                .WithErrorCode("PredicateValidator");
        }

        [Theory]
        [InlineData(null, "NotEmptyValidator")]
        [InlineData("", "NotEmptyValidator")]
        [InlineData(" ", "NotEmptyValidator")]
        [InlineData(" XXX", "TrimmedValidator")]
        [InlineData("XXX ", "TrimmedValidator")]
        public void BadAddress1(string? value, string errorCode) => Validate(
            new Member() { Address1 = value }, m => m.Address1, errorCode);

        private static void Validate<M, R>(M member,
            Expression<Func<Member, R>> getProperty, string errorCode)
            where M : Member
        {
            var expression = (MemberExpression)getProperty.Body;

            var validator = new MemberValidator();

            var result = validator.TestValidate(member);

            var name = expression.Member.Name;

            result.ShouldHaveValidationErrorFor(name)
                .WithErrorCode(errorCode);
        }
    }
}