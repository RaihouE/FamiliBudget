using FamiliBudget.App.Infrastructure.Auth;
using System.Text;
using System.Text.Json;

namespace FamiliBudget.App.Infrastructure;

public class BudgetApiClient
{
	private readonly HttpClient _httpClient;

	public BudgetApiClient(HttpClient httpClient)
	{
		_httpClient = httpClient;
	}

	public async Task<TokenResponse?> Login(string username, string password)
	{
		var json = JsonSerializer.Serialize(new { username, password });
		var content = new StringContent(json, Encoding.UTF8, "application/json");

		var result = await _httpClient.PostAsync("api/Authenticate/login", content);

		result.EnsureSuccessStatusCode();

		var tokenResult = await result.Content.ReadFromJsonAsync<TokenResponse>();

		return tokenResult;
	}

	public async Task<List<string>> Register(string username, string password, string email)
	{
		var errors = new List<string>();

		var json = JsonSerializer.Serialize(new { username, password, email });
		var content = new StringContent(json, Encoding.UTF8, "application/json");

		var result = await _httpClient.PostAsync("api/Authenticate/register", content);

		if (!result.IsSuccessStatusCode)
		{
			var stringResult = await result.Content.ReadAsStringAsync();
			errors.Add(stringResult);
		}

		return errors;
	}
}
