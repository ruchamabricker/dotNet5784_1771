
namespace DO;
/// <summary>
/// Engineer Entity, represents a engineer with all its props
/// </summary>
/// <param name="id"> uniqe id of engineer </param>
/// <param name="name"> name of engineer </param>
/// <param name="email"> email of engineer </param>
/// <param name="level"> experience level of the engineer </param>
/// <param name="cost"> cost per hour </param>
public record Engineer(  
    int id,
    string name,
    string email,
    EngineerExperience level,
    double cost,
    bool active=true
);

