using System.Collections.Immutable;
using System.ComponentModel.DataAnnotations;

namespace LibraryManagement.Core.Utilities;

public abstract class ValidatableObjectBase : IValidatableObject
{
    public virtual IEnumerable<ValidationResult> Validate(ValidationContext validationContext) => [];

    public bool TryValidateObject(out IEnumerable<ValidationResult> results)
    {
        var list = new List<ValidationResult>();
        Validator.TryValidateObject(this, new ValidationContext(this), list, true);
        results = list.ToImmutableList();
        return !results.Any();
    }
}