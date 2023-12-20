namespace BlApi;

public interface IEngineer
{
    /// <summary>
    /// creates an engineer
    /// </summary>
    /// <param name="item"></param>
    /// <returns></returns>
    int Create(BO.Engineer item);
    BO.Engineer Read(int id);//returns engineer by given id
    IEnumerable<BO.Engineer?> ReadAll(Func<BO.Engineer, bool>? filter = null);//returns all engineers that pass the condition
    void Update(BO.Engineer item); //Updates engineers details
    void Delete(int id); //Deletes an engineer by its Id
}
