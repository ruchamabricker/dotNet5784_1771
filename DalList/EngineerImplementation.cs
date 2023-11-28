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

        if (Read(item.id) is not null)
            throw new DalAlreadyExistsException($"Student with ID={item.id} already exists");

        DataSource.Engineers.Add(item);
        return item.id;
        // Assuming DataSource.Engineers is a List<T> or any collection that implements IEnumerable<T>
        // DataSource.Engineers.Concat(new[] { item }).ToList();

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
        return DataSource.Engineers.FirstOrDefault(lk => lk.id == id);
    }

    public Engineer? Read(Func<Engineer, bool> filter) // stage 2
    {
        return DataSource.Engineers.FirstOrDefault(filter);
    }

    public IEnumerable<Engineer?> ReadAll(Func<Engineer, bool>? filter = null) // stage 2S
    {

        if (filter == null)
            return DataSource.Engineers.Select(item => item);
        else
            return DataSource.Engineers.Where(filter);
    }

    //public List<Engineer> ReadAll()
    //{
    //    List<Engineer> listE = DataSource.Engineers;
    //    return listE;
    //}

    public void Update(Engineer item)
    {
        Engineer e = DataSource.Engineers.FirstOrDefault(lk => lk.id == item.id);
        if (e != null)
        {
            DataSource.Engineers.Remove(e);
            DataSource.Engineers.Add(item);
        }
        else
            throw new DalDoesNotExistException($"no such item with {item.id} id in Engineers");
    }
}
