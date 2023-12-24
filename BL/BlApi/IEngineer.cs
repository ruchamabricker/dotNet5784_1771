using BO;

namespace BlApi;

public interface IEngineer
{
    /// <summary>
    /// creates an engineer
    /// </summary>
    /// <param name="engineer">receives the engineer that should be added</param>
    /// <returns>new engineer</returns>
    public int Create(BO.Engineer engineer);

    /// <summary>
    /// returns engineer by given id
    /// </summary>
    /// <param name="id">id of the engineer that should be returned</param>
    /// <returns>engineer</returns>
    public BO.Engineer Read(int id);

    /// <summary>
    /// returns all engineers that pass the condition
    /// </summary>
    /// <param name="filter">the condition</param>
    /// <returns>all engineers that pass the condition</returns>
    public IEnumerable<BO.Engineer?> ReadAll(Func<BO.Engineer, bool>? filter = null);

    /// <summary>
    /// Updates engineers details
    /// </summary>
    /// <param name="engineer">engineer with new details</param>
    public void Update(BO.Engineer engineer);

    /// <summary>
    /// Deletes an engineer by its Id
    /// </summary>
    /// <param name="id">id of engineer that should be deleted</param>
    public void Delete(int id);
}
