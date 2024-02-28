using System.ComponentModel.DataAnnotations;

namespace App.Core.Exceptions;

public class ModelException : Exception
{
    public List<ValidationResult> Errors { get; set; }
    public ModelException(List<ValidationResult> validationResults)
    {
        Errors = validationResults;
    }
}