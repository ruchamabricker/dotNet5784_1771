using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO;
namespace BlApi;
/// <summary>
/// functions that can be done on tasks
/// </summary>
public interface ITask
{

    /// <summary>
    /// creates new task
    /// </summary>
    /// <param name="task">the new task to be added</param>
    /// <returns></returns>
    public int Create(BO.Task task);

    /// <summary>
    /// returns task by given id
    /// </summary>
    /// <param name="id">id of task that should be returned</param>
    /// <returns>id of the new task</returns>
    public BO.Task? Read(int id);

    /// <summary>
    /// returns all the tasks that pass the condition
    /// </summary>
    /// <param name="filter">the condition the</param>
    /// <returns>all the tasks that pass the condition</returns>
    public IEnumerable<BO.Task?> ReadAll(Func<BO.Task, bool>? filter = null);

    /// <summary>
    /// Updates task details
    /// </summary>
    /// <param name="task">the detailes of the task</param>
    public void Update(BO.Task task);

    /// <summary>
    /// Deletes a task by its Id
    /// </summary>
    /// <param name="id">id of the task that should be deleted</param>
    public void Delete(int id);
}