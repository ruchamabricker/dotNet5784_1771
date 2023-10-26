
namespace DO;
/// <summary>
/// Enigneer Entity, represents a enigneer with all its props
/// </summary>
/// <param name="id"> uniqe id of enigneer </param>
/// <param name="name"> name of enigneer </param>
/// <param name="email"> email of enigneer </param>
/// <param name="level"> experience level of the enigneer </param>
/// <param name="cost"> cost per hour </param>
public record Enigneer(  
    int id,
    string name,
    string email,
    EngineerExperience level,
    double cost
);

