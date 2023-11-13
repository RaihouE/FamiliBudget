using System.ComponentModel.DataAnnotations;

namespace FamiliBudget.App.Infrastructure.Auth;

public class LoginModel
{
	[Required]
	public string? UserName { get; set; }

	[Required]
	public string? Password { get; set; }
}
