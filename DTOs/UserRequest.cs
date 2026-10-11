using System.ComponentModel.DataAnnotations;

namespace UserManagementApi.DTOs;

public sealed class UserRequest
{
    [Required(ErrorMessage = "Name is required.")]
    [StringLength(100, MinimumLength = 2)]
    public required string Name { get; init; }

    [Required(ErrorMessage = "Email is required.")]
    [EmailAddress]
    [StringLength(254)]
    public required string Email { get; init; }

    [Range(1, 120)]
    public int Age { get; init; }
}
