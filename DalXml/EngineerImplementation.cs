namespace Dal;
using DalApi;
using DO;
using System.Collections.Generic;
using System.Linq;

internal class EngineerImplementation : IEngineer
{
    /// <summary>
    /// creates a new engineer
    /// </summary>
    /// <param name="engineer">the new engineer</param>
    /// <returns></returns>
    /// <exception cref="DalAlreadyExistsException"></exception>
    public int Create(Engineer engineer)
    {
        List<Engineer> engineersList = XMLTools.LoadListFromXMLSerializer<Engineer>("engineers");
        if (Read(engineer.Id) is not null)
            throw new DalAlreadyExistsException($"Engineer with ID={engineer.Id} already exists");
        engineersList.Add(engineer);
        XMLTools.SaveListToXMLSerializer<Engineer>(engineersList, "engineers");
        return engineer.Id;
    }

    public void Delete(int id)
    {
        List<Engineer> engineers = XMLTools.LoadListFromXMLSerializer<Engineer>("engineers");
        if (Read(id) is null)
            throw new DalDoesNotExistException($"Task with ID {id} does not exists");
        engineers.RemoveAll(engineer => engineer.Id == id);
        XMLTools.SaveListToXMLSerializer<Engineer>(engineers, "engineers");
    }

    /// <summary>
    /// return an engineer with the given id, if there isn't such engineer it return an empty string
    /// </summary>
    /// <param name="id">the id of the engineer that should be given</param>
    /// <returns></returns>
    public Engineer? Read(int id)
    {
        List<Engineer> engineersList = XMLTools.LoadListFromXMLSerializer<Engineer>("engineers");
        return engineersList.FirstOrDefault(engineer => engineer.Id == id);
    }

    /// <summary>
    /// return an engineer that fulfills the given condition
    /// </summary>
    /// <param name="filter">the given condition</param>
    /// <returns></returns>
    /// <exception cref="DalDoesNotExistException"></exception>
    public Engineer? Read(Func<Engineer, bool> filter)
    {
        List<Engineer> engineers = XMLTools.LoadListFromXMLSerializer<Engineer>("engineers");
        Engineer? engineer = engineers.FirstOrDefault(filter);
        if (engineer != null)
        {
            return engineer;
        }
        throw new DalDoesNotExistException($"Cannot find Task with given filter");
    }

    /// <summary>
    /// returns all the engineer that fullfill the given condition
    /// </summary>
    /// <param name="filter">the given condition</param>
    /// <returns>all engineers that fullfill the given condition</returns>
    /// <exception cref="DalDoesNotExistException"></exception>
    public IEnumerable<Engineer?> ReadAll(Func<Engineer, bool>? filter = null)
    {
        List<Engineer> engineers = XMLTools.LoadListFromXMLSerializer<Engineer>("engineers");

        if (filter != null)
        {
            List<Engineer> engineer = engineers.Where(filter).ToList();
        }

        if (engineers.Count > 0)
            return engineers;
        throw new DalDoesNotExistException($"There is no engineers");
    }

    /// <summary>
    /// updates the given engineer
    /// </summary>
    /// <param name="engineer">the id of the engineer that should be updated</param>
    /// <exception cref="DalDoesNotExistException"></exception>
    public void Update(Engineer engineer)
    {
        List<Engineer> engineers = XMLTools.LoadListFromXMLSerializer<Engineer>("engineers");
        Engineer? existingEngineer = engineers.FirstOrDefault(e => e.Id == engineer.Id);
        if (existingEngineer == null)
            throw new DalDoesNotExistException($"There is no engineer with ID {engineer.Id}");
        engineers.Remove(existingEngineer);
        engineers.Add(engineer);
        XMLTools.SaveListToXMLSerializer<Engineer>(engineers, "engineers");
    }
    /// <summary>
    /// resets the array of engineers
    /// </summary>
    public void Reset()
    {
        List<Engineer> engineersList = XMLTools.LoadListFromXMLSerializer<Engineer>("engineers");
        if(engineersList.Count > 0) { 
        engineersList.Clear();
        XMLTools.SaveListToXMLSerializer<Engineer>(engineersList, "engineers");}
    }
}
