namespace ApplicationCore.Entities;

public class User
{
    public int Id { get; set; }
    public DateTime? DateOfBirth { get; set; }
    public string Email { get; set; } = null!;
    public string FirstName { get; set; } = null!;
    public string HashedPassword { get; set; } = null!;
    public bool? IsLocked { get; set; }
    public string LastName { get; set; } = null!;
    public string? PhoneNumber { get; set; }
    public string? ProfilePictureUrl { get; set; }
    public string Salt { get; set; } = null!;
    
    public ICollection<UserRole> RolesOfUser { get; set; } = null!;
    public ICollection<Review> Reviews { get; set; }
}