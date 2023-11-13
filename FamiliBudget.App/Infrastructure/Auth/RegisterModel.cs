using System.ComponentModel.DataAnnotations;

namespace FamiliBudget.App.Infrastructure.Auth;

public class RegisterModel
{
	[Required]
	public string? UserName { get; set; }

	[Required]
	public string? Password { get; set; }

	[Required]
	[EmailAddress]
	public string? Email { get; set; }
}
