namespace Dal;
using DalApi;
using DO;
using System.Collections.Generic;
using System.Linq;

internal class EngineerImplementation : IEngineer
{
    public int Create(Engineer item)
    {
        //List<Engineer> engineersList = XMLTools.LoadListFromXMLSerializer<Engineer>("engineers");
        //try
        //{
        //    Read(item.Id);
        //}
        //catch (Exception)
        //{
        //    engineersList.Add(item);//add to the file
        //    XMLTools.SaveListToXMLSerializer<Engineer>(engineersList, "engineers");
        //    return item.Id;
        //}
        //throw new DalAlreadyExistsException($"An object of type Engineer with ID {item.Id} already exists");

        List<Engineer> engineers = XMLTools.LoadListFromXMLSerializer<Engineer>("engineers");
        engineers.Add(item);
        XMLTools.SaveListToXMLSerializer<Engineer>(engineers, "engineers");
        return item.Id;
    }

    public void Delete(int id)
    {
        List<Engineer> engineers = XMLTools.LoadListFromXMLSerializer<Engineer>("engineers");
        if (Read(id) is null)
            throw new DalDoesNotExistException($"Task with ID {id} does not exists");
        engineers.RemoveAll(t => t.Id == id);
        XMLTools.SaveListToXMLSerializer<Engineer>(engineers, "engineers");
    }

    public Engineer? Read(int id)
    {
        List<Engineer> engineersList = XMLTools.LoadListFromXMLSerializer<Engineer>("engineers");
        Engineer? engineer = engineersList.FirstOrDefault(e => e.Id == id);
        if (engineer != null)
        {
            return engineer;
        }
        throw new DalDoesNotExistException($"Cannot read Engineer with ID:{id}.");
    }

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

    public IEnumerable<Engineer?> ReadAll(Func<Engineer, bool>? filter = null)
    {
        List<Engineer> engineers = XMLTools.LoadListFromXMLSerializer<Engineer>("engineers");

        if (filter != null)
        {
            List<Engineer> engineer = engineers.Where(filter).ToList();
            if (engineer.Count > 0)
                return engineer;
            throw new DalDoesNotExistException($"There is no engineers");
        }
        else
        {
            if (engineers.Count > 0)
                return engineers;
            throw new DalDoesNotExistException($"There is no engineers");
        }
    }

    public void Update(Engineer item)
    {
        List<Engineer> engineers = XMLTools.LoadListFromXMLSerializer<Engineer>("engineers");
        Engineer? existingEngineer = engineers.FirstOrDefault(e => e.Id == item.Id);
        if (existingEngineer == null)
            throw new DalDoesNotExistException($"There is no engineer with ID {item.Id}");
        engineers.Remove(existingEngineer);
        engineers.Add(item);
        XMLTools.SaveListToXMLSerializer<Engineer>(engineers, "engineers");
    }
}
