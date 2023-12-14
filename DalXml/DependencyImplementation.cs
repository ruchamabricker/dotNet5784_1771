
namespace Dal;
using DalApi;
using DO;
using System.Xml.Linq;

internal class DependencyImplementation : IDependency
{
    const string dependenciesFile = @"..\xml\dependencies.xml";
    XDocument dependenciesDocument = XDocument.Load(dependenciesFile);

    public int Create(Dependency item)
    {
        int newDependencyId = Config.NextDependencyId;

        XElement? dependencyElement = new XElement("Dependency",
            new XElement("Id", newDependencyId),
            new XElement("DependentTask", item.DependentTask),
            new XElement("DependsOnTask", item.DependsOnTask));

        dependenciesDocument.Root?.Add(dependencyElement);
        dependenciesDocument.Save(dependenciesFile);

        return newDependencyId;
    }

    public void Delete(int id)
    {
        if (dependenciesDocument.Root != null)
        {
            XElement? dependencyElement = dependenciesDocument.Root
                .Elements("Dependency")
                .FirstOrDefault(e => (int)e.Element("Id") == id);

            if (dependencyElement != null)
            {
                dependencyElement.Remove();
                dependenciesDocument.Save(dependenciesFile);
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
        XElement? dependencyElement = dependenciesDocument.Root?
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
        Dependency? dependency = dependenciesDocument.Root?
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
        XElement? dependenciesElement = XMLTools.LoadListFromXMLElement("dependencies");

        IEnumerable<Dependency> dependencies = dependenciesElement
            .Elements("Dependency")
            .Select(e => new Dependency(
                Id: (int)e.Element("Id")!,
                DependentTask: (int)e.Element("DependentTask")!,
                DependsOnTask: (int)e.Element("DependsOnTask")!
            ));

        if (filter != null)
        {
            dependencies = dependencies.Where(filter);
        }

        return dependencies;//.ToList(); // Convert to List before returning
    }
    public void Update(Dependency item)
    {
        XElement dependenciesElement = XMLTools.LoadListFromXMLElement("dependencies");

        XElement dependencyElement = dependenciesElement.Descendants("Dependency")
            .FirstOrDefault(e => (int)e.Element("Id")! == Convert.ToInt32(item.Id))!;

        if (dependencyElement != null)
        {
            dependencyElement.Element("DependentTask")!.Value = item.DependentTask.ToString();
            dependencyElement.Element("DependsOnTask")!.Value = item.DependsOnTask.ToString();

            XMLTools.SaveListToXMLElement(dependenciesElement, "dependencies");
        }
        else
        {
            throw new InvalidOperationException("Dependency not found.");
        }
    }
    //public void Reset()
    //{
    //    dependenciesDocument.Root?.RemoveAll();
    //    dependenciesDocument.Save(dependenciesFile);
    //}
    public void Reset()
    {
        XElement arrayOfDependencyElement = dependenciesDocument.Root;
        if (arrayOfDependencyElement != null)
        {
            arrayOfDependencyElement.Elements().Remove();
            dependenciesDocument.Save(dependenciesFile);
        }
    }


}
