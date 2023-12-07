
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
        int newDependencyId = Config.NextDependencyId;

        XElement? dependencyElement = new XElement("Dependency",
            new XElement("Id", newDependencyId),
            new XElement("DependentTask", item.dependentTask),
            new XElement("DependsOnTask", item.dependsOnTask));

        dependencysDocument.Root?.Add(dependencyElement);
        dependencysDocument.Save(dependencysFile);

        return newDependencyId;
    }

    public void Delete(int id)
    {
        if (dependencysDocument.Root != null)
        {
            XElement? dependencyElement = dependencysDocument.Root
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
        else
        {
            throw new DalDoesNotExistException("Dependencies document is empty.");
        }
    }


    public Dependency? Read(int id)
    {
        XElement? dependencyElement = dependencysDocument.Root?
            .Elements("Dependency")
            ?.FirstOrDefault(e => (int)e.Element("Id") == id);

        if (dependencyElement != null)
        {
            Dependency? dependency = new Dependency(
                (int)dependencyElement.Element("Id")!,
                (int)dependencyElement.Element("DependentTask")!,
                (int)dependencyElement.Element("DependsOnTask")!
            );

            return dependency;
        }

        throw new DalDoesNotExistException("Dependency with the specified ID does not exist.");
    }



    public Dependency? Read(Func<Dependency, bool> filter)
    {
        Dependency? dependency = dependencysDocument.Root?
            .Elements("Dependency")
            ?.Select(e => new Dependency(
                (int)e.Element("Id")!,
                (int)e.Element("DependentTask")!,
                (int)e.Element("DependsOnTask")!
            ))
            !.FirstOrDefault(filter);

        return dependency;
    }

    public IEnumerable<Dependency> ReadAll(Func<Dependency, bool>? filter = null)
    {
        XElement? dependenciesElement = XMLTools.LoadListFromXMLElement("dependencys");

        IEnumerable<Dependency> dependencies = dependenciesElement
            .Elements("Dependency")
            .Select(e => new Dependency(
                id: (int)e.Element("Id")!,
                dependentTask: (int)e.Element("DependentTask")!,
                dependsOnTask: (int)e.Element("DependsOnTask")!
            ));

        if (filter != null)
        {
            dependencies = dependencies.Where(filter);
        }

        return dependencies;//.ToList(); // Convert to List before returning
    }
    public void Update(Dependency item)
    {
        XElement dependenciesElement = XMLTools.LoadListFromXMLElement("dependencys");

        XElement dependencyElement = dependenciesElement.Descendants("Dependency")
            .FirstOrDefault(e => (int)e.Element("Id")! == Convert.ToInt32(item.id))!;

        if (dependencyElement != null)
        {
            dependencyElement.Element("DependentTask")!.Value = item.dependentTask.ToString();
            dependencyElement.Element("DependsOnTask")!.Value = item.dependsOnTask.ToString();

            XMLTools.SaveListToXMLElement(dependenciesElement, "dependencys");
        }
        else
        {
            throw new InvalidOperationException("Dependency not found.");
        }
    }
}
