
namespace Dal;
using DalApi;
using DO;
using System.Xml.Linq;
using System.Xml.Serialization;

internal class TaskImplementation : ITask
{

    const string tasksFile = @"..\xml\tasks.xml";
    //XDocument tasksDocument = XDocument.Load(tasksFile);
    //public int Create(DO.Task item)
    //{
    //    XmlSerializer ser = new XmlSerializer(typeof(Task));
    //    // מצביע לקובץ 
    //    StreamWriter w = new StreamWriter(dependencysFile);
    //    // הפעולה עצמה
    //    ser.Serialize(w, item);
    //    // שחרור המצביע
    //    w.Close();

    //    return item.id;
    //    //throw new NotImplementedException();
    //}

    public int Create(DO.Task item)
    {
        XmlSerializer serializer = new XmlSerializer(typeof(DO.Task));
        using (TextWriter writer = new StreamWriter(tasksFile))
        {
            serializer.Serialize(writer, item);
        }

        return item.id;
    }



    public void Delete(int id)
    {
        throw new NotImplementedException();
    }

    public DO.Task? Read(int id)
    {
        throw new NotImplementedException();
    }

    public DO.Task? Read(Func<DO.Task, bool> filter)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<DO.Task?> ReadAll(Func<DO.Task, bool>? filter = null)
    {
        throw new NotImplementedException();
    }

    public void Update(DO.Task item)
    {
        throw new NotImplementedException();
    }
}
