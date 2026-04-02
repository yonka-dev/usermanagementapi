using System.ComponentModel.DataAnnotations;

namespace UserManagementAPI.DTOs
{
    public class UserUpdateDto
    {
        [Required, StringLength(50, MinimumLength = 1)]
        public string FirstName { get; set; } = string.Empty;

        [Required, StringLength(50, MinimumLength = 1)]
        public string LastName { get; set; } = string.Empty;

        [Required, EmailAddress, StringLength(254)]
        public string Email { get; set; } = string.Empty;
    }
}