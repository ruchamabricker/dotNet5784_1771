
namespace Dal;
using DalApi;
using DO;
using System.Xml.Linq;
using System.Xml.Serialization;

internal class TaskImplementation : ITask
{

    const string tasksFile = @"..\xml\tasks.xml";
    public int Create(DO.Task item)
    {
        // הגדרת אוביקט= מכונה שיודעת להמיר אוביקטים מ ואל מחרוזת
        XmlSerializer serializer = new XmlSerializer(typeof(List<DO.Task>));
        // מצביע לקובץ שיודע לקרוא
        TextReader textReader = new StringReader(tasksFile);
        // 
        List<DO.Task> lst = (List<Task>?)serializer.Deserialize(textReader) ?? throw new Exception();
        // הוספת הפריט החדש
        lst.Add(item);

        using (TextWriter writer = new StreamWriter(tasksFile))
        {
            serializer.Serialize(writer, lst);
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
