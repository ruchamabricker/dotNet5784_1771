
namespace Dal;
using DalApi;
using DO;
using System.Xml.Linq;

internal class DependencyImplementation : IDependency
{

    const string dependencysFile = @"..\xml\dependencys.xml";
    XDocument dependencysDocument = XDocument.Load(dependencysFile);


    public int Create(Dependency item)
    {

        XElement dependencyElement = new XElement("Dependency", new XAttribute("id", item.id),
            new XAttribute("DependentTask", item.dependentTask),
            new XAttribute("DependsOnTask", item.dependsOnTask));

        dependencysDocument.Root?.Add(dependencyElement);
        dependencysDocument.Save(dependencysFile);

        return item.id;
        //throw new NotImplementedException();
    }

    public void Delete(int id)
    {
        XElement dependencyElement = dependencysDocument.Root!
            .Elements("Dependency")
            .FirstOrDefault(e => (int)e.Element("Id") == id);

        if (dependencyElement != null)
        {
            dependencyElement.Remove();
            dependencysDocument.Save(dependencysFile);
        }
        else
        {
            throw new DalDoesNotExistException("Dependency with the specified ID does not exist.");
        }
    }

    public Dependency? Read(int id)
    {
        XElement dependencyElement = dependencysDocument.Root!
            .Elements("Dependency")
            .FirstOrDefault(e => (int)e.Element("Id") == id);
        if( dependencyElement!=null)
        {
            Dependency dependency = new Dependency();
        }
        throw new NotImplementedException();
    }

    public Dependency? Read(Func<Dependency, bool> filter)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Dependency?> ReadAll(Func<Dependency, bool>? filter = null)
    {
        throw new NotImplementedException();
    }

    public void Update(Dependency item)
    {
        throw new NotImplementedException();
    }
}
