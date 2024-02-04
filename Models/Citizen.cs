using System.ComponentModel.DataAnnotations;

namespace ActiveCity.Models;

public enum Gender
{
    Male,
    Female
} 

public class Citizen
{
    public int Id { get; set; }
    public required User User { get; set; }

    [EnumDataType(typeof(Gender))]
    public Gender? Gender { get; set; }
    public required int Age { get; set; }
    public required string Education { get; set; }
    public required string Birthdate { get; set; }
    
    [EmailAddress]
    public required string Email { get; set; }
}