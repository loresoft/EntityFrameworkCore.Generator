namespace EntityFrameworkCore.Generator;

/// <summary>
/// 
/// </summary>
public interface IOptionVariable
{
    /// <summary>
    /// Sets variables on the specified VariableDictionary.
    /// </summary>
    /// <param name="variableDictionary">The variable dictionary.</param>
    void Set(VariableDictionary variableDictionary);

    /// <summary>
    /// Removes variables on the specified VariableDictionary.
    /// </summary>
    /// <param name="variableDictionary">The variable dictionary.</param>
    void Remove(VariableDictionary variableDictionary);
}