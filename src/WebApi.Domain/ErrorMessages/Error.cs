namespace WebApi.Domain.ErrorMessages;

public static class Error
{
    public const string RequiredField = "The {0} field is required.";
    public const string StringLength = "The {0} field must be between {2} and {1} characters.";
    public const string Range = "The {0} field must be between {1} and {2}.";
}

