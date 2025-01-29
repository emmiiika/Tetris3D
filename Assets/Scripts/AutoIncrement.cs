/// <summary>
/// Class <c>AutoIncrement</c> generates a counter for IDs. This class should have only one instance for unique IDs.
/// </summary>
public class AutoIncrement
{
    private int _id = 0;

    /// <summary>
    /// Method <c>GenerateID</c> returns a new ID and increments the counter.
    /// </summary>
    public int GenerateId(){
        return _id++;
    }
}