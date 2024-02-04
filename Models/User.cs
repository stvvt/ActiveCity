using System.ComponentModel.DataAnnotations;

namespace ActiveCity.Models;
public enum Role
{
    Admin,
    Employee,
    Citizen,
} 

public class User
{
    public int Id { get; set; }
    public required string Username { get; set; }
    
    [DataType(DataType.Password)]
    public required string Password { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }

    [EnumDataType(typeof(Role))]
    public required Role Role { get; set; }
}