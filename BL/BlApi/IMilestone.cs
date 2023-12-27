namespace BlApi;

public interface IMilestone
{
    /// <summary>
    /// returns milestone by given id
    /// </summary>
    /// <param name="id">id of meilestone that should be given back</param>
    /// <returns>milestone</returns>
    BO.Milestone Read(int id);

    /// <summary>
    /// Updates milestones details
    /// </summary>
    /// <param name="milestone"></param>
    /// <returns>updated milestone</returns>
    BO.Milestone Update(int id); 

}