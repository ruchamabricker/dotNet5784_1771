
namespace DO;
/// <summary>
/// 
/// </summary>
/// <param name="id"></param>
/// <param name="name"></param>
/// <param name="email"></param>
/// <param name="level"></param>
/// <param name="cost"></param>
public record Enigneer(  
    int id,
    string name,
    string email,
    EngineerExperience level,
    double cost
);

