using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace HelperUtility
{
    public static class Utility
    {

        public static bool Validate<T>(T obj, out ICollection<ValidationResult> results)
        {
            results = new List<ValidationResult>();

            return Validator.TryValidateObject(obj, new ValidationContext(obj), results, true);
        }
    }
}
