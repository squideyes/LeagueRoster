using FluentValidation;
using FluentValidation.Validators;

namespace AL.LeagueRoster.Common
{
    public class TrimmedValidator<T, V> : PropertyValidator<T, V>
    {
        private readonly bool isOptional;

        public TrimmedValidator(bool isOptional) =>
            this.isOptional = isOptional;

        public override string Name => nameof(TrimmedValidator<T, V>);

        public override bool IsValid(ValidationContext<T> context, V value)
        {
            if (value == null)
                return isOptional;
            else
                return (value as string).IsTrimmed();
        }

        protected override string GetDefaultMessageTemplate(string errorCode) =>
            "'{PropertyName}' must be trimmed.";
    }
}
