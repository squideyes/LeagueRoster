using FluentValidation;

namespace AL.LeagueRoster.Common;

public class MemberValidator : AbstractValidator<Member>
{
    private static string GetBadPostalCodeMessage(CountryCode countryCode)
    {
        var prefix = $"'{nameof(Member.PostalCode)}' must be ";

        if (!Enum.IsDefined(countryCode))
            return prefix + "an appropriate postal code for CountryCode";
        else
            return prefix + $"a {countryCode} postal code.";
    }

    public MemberValidator()
    {
        //RuleFor(m => m.ClubId)
        //    .GreaterThan(0);

        //RuleFor(m => m.FirstName)
        //    .Trimmed();

        //RuleFor(m => m.Initial)
        //    .Initial();

        //RuleFor(m => m.LastName)
        //    .Trimmed();

        //RuleFor(m => m.Address1)
        //    .Cascade(CascadeMode.Stop)
        //    .NotEmpty()
        //    .Trimmed();

        //RuleFor(m => m.Address2)
        //    .Trimmed(true);

        //RuleFor(m => m.Locality)
        //    .Trimmed();

        //RuleFor(m => m.Region)
        //    .Trimmed();

        var choices = EnumList.ToChoices<CountryCode>();

        RuleFor(m => m.CountryCode)
            .IsInEnum()
            .WithName(nameof(Member.CountryCode))
            .WithMessage($"'{{PropertyName}}' must be {choices}.");

        RuleFor(m => new { m.CountryCode, m.PostalCode })
            .Must(x => Enum.IsDefined(x.CountryCode) &&
                x.PostalCode.IsPostalCode(x.CountryCode))
            .WithName(m => nameof(Member.PostalCode))
            .WithMessage(m => GetBadPostalCodeMessage(m.CountryCode));




        //RuleFor(m => m.EmailAddress)
        //    .EmailAddress();

        //RuleFor(m => m.Reflector)
        //    .IsInEnum();

        //RuleFor(m => m.UpdatedOn)
        //    .Cascade(CascadeMode.Stop)
        //    .NotEmpty()
        //    .Must(m => m.Kind == DateTimeKind.Utc)
        //    .WithName(nameof(Member.UpdatedOn))
        //    .WithMessage("'{PropertyName}' must be a UTC date-time.");

        //RuleFor(m => m.Roles)
        //    .ListOfRoles();
    }
}
