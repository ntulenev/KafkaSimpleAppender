namespace Logic;

/// <summary>
/// Json Validator.
/// </summary>
public interface IJsonValidator
{
    /// <summary>
    /// Validates the specified string
    /// </summary>
    /// <param name="value">The string to be validated.</param>
    /// <returns>Returns true if the string is valid JSON string, otherwise, false.</returns>
    public bool IsValid(string value);
}
