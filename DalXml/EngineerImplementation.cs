namespace Dal;
using DalApi;
using DO;

internal class EngineerImplementation : IEngineer
{
    public int Create(Engineer item)
    {
        //List<Engineer> engineersList = XMLTools.LoadListFromXMLSerializer<Engineer>("engineers");
        //try
        //{
        //    Read(item.id);
        //}   
        //catch (Exception)
        //{
        //    engineersList.Add(item);//add to the file
        //    XMLTools.SaveListToXMLSerializer<Engineer>(engineersList, "engineers");
        //    return item.id;
        //}
        //throw new DalAlreadyExistsException($"An object of type Engineer with ID {item.id} already exists");


        List<Engineer> engineers = XMLTools.LoadListFromXMLSerializer<Engineer>("engineers");
        engineers.Add(item);
        XMLTools.SaveListToXMLSerializer<Engineer>(engineers, "engineers");
        return item.id;
    }

    public void Delete(int id)
    {
        throw new NotImplementedException();
    }

    public Engineer? Read(int id)
    {
        List<Engineer> engineersList = XMLTools.LoadListFromXMLSerializer<Engineer>("engineers");
        Engineer? engineer = engineersList.FirstOrDefault(e => e.id == id);
        if (engineer != null)
        {
            return engineer;
        }
        throw new DalDoesNotExistException($"Cannot read Engineer with ID:{id}.");
    }

    public Engineer? Read(Func<Engineer, bool> filter)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Engineer?> ReadAll(Func<Engineer, bool>? filter = null)
    {
        throw new NotImplementedException();
    }

    public void Update(Engineer item)
    {
        throw new NotImplementedException();
    }
}
