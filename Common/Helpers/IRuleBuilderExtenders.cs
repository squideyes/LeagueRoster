using FluentValidation;
using FluentValidation.Validators;

namespace AL.LeagueRoster.Common;

public static class IRuleBuilderExtenders
{
    //public static IRuleBuilderOptionsConditions<T, List<Role>?> ListOfRoles<T>(
    //    this IRuleBuilder<T, List<Role>?> ruleBuilder)
    //{
    //    return ruleBuilder.Custom((item, context) =>
    //    {
    //        if (item == null)
    //            return;

    //        if (item.Count == 0 || item.Any(i => !Enum.IsDefined(i)))
    //            context.AddFailure($"'{context.PropertyName}' must be a non-empty list of Roles.");
    //    });
    //}

    //public static IRuleBuilderOptionsConditions<T, string?> CountryCode<T>(
    //    this IRuleBuilder<T, string?> ruleBuilder)
    //{
    //    return ruleBuilder.Custom((item, context) =>
    //    {
    //        if (item?.IsCountryCode() == false)
    //            context.AddFailure($"'{context.PropertyName}' must be a valid ISO 3166 Country Code.");
    //    });
    //}

    //public static IRuleBuilderOptionsConditions<T, char?> Initial<T>(
    //    this IRuleBuilder<T, char?> ruleBuilder)
    //{
    //    return ruleBuilder.Custom((item, context) =>
    //    {
    //        if (item != null && !char.IsUpper(item.Value))
    //            context.AddFailure($"'{context.PropertyName}' must be null or an uppercase letter.");
    //    });
    //}

    public static IRuleBuilderOptions<T, P> Trimmed<T, P>(
        this IRuleBuilder<T, P> ruleBuilder, bool isOptional = false)
    {
        return ruleBuilder.SetValidator(
            new TrimmedValidator<T, P>(isOptional));
    }
}
