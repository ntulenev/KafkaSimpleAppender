namespace Logic;

/// <summary>
/// Json Validator.
/// </summary>
public interface IJsonValidator
{
    /// <summary>
    /// Checks if string is json.
    /// </summary>
    /// <param name="value">String for checking.</param>
    /// <returns>true if string is json.</returns>
    public bool IsValid(string value);
}
