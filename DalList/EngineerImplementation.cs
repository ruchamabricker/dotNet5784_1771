namespace Dal;
using DalApi;
using DO;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
internal class EngineerImplementation : IEngineer
{

    public int Create(Engineer item)
    {
        if (item == null)
            throw new DalNoArgumentPassedException("there is item passed to be added");

        if (Read(item.Id) is not null)
            throw new DalAlreadyExistsException($"Engineer with ID={item.Id} already exists");

        DataSource.Engineers.Add(item);
        return item.Id;
    }

    public void Delete(int id)
    {
        throw new DalDeletionImpossible("can't delete engineer");
        //if (DataSource.Engineers.FirstOrDefault(item => item.id == id) == null)
        //{
        //    throw new DalDoesNotExistException($"there is no enigeer with this id: {id}");
        //}
        //if (DataSource.Tasks.FirstOrDefault(item => item.engineerid == id) != null)
        //{
        //    throw new DalDeletionImpossible($"this enigeer {id} has tasks, he can not be deleted!");
        //}
        ////DataSource.Engineers.Remove(DataSource.Engineers.FirstOrDefault(item => item.id == id));
        ////DataSource.Engineers.FirstOrDefault(item => item.id == id).active = false;
    }

    public Engineer? Read(int id)
    {
        return DataSource.Engineers.FirstOrDefault(lk => lk.Id == id);
    }

    public Engineer? Read(Func<Engineer, bool> filter) // stage 2
    {
        return DataSource.Engineers.FirstOrDefault(filter);
    }

    public IEnumerable<Engineer?> ReadAll(Func<Engineer, bool>? filter = null) // stage 2
    {

        if (filter == null)
            return DataSource.Engineers.Select(item => item);
        else
            return DataSource.Engineers.Where(filter);
    }

    public void Update(Engineer item)
    {
        Engineer e = DataSource.Engineers.FirstOrDefault(lk => lk.Id == item.Id)!;
        if (e != null)
        {
            DataSource.Engineers.Remove(e);
            DataSource.Engineers.Add(item);
        }
        else
            throw new DalDoesNotExistException($"no such item with {item.Id} id in Engineers");
    }
    public void Reset()
    {
        if (!DataSource.Engineers.Any())
            DataSource.Engineers.Clear();
    }
}
