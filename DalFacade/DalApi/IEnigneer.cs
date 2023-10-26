
namespace DalApi;
using DO;

public interface IEnigneer
{
    int Create(Enigneer item); //Creates new entity object in DAL
    Enigneer? Read(Enigneer id); //Reads entity object by its ID 
    List<Enigneer> ReadAll(); //stage 1 only, Reads all entity objects
    void Update(Enigneer item); //Updates entity object
    void Delete(int id); //Deletes an object by its Id
}
