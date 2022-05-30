using IdentityModel.Client;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Tlm.Sdk.Api;

namespace Tlm.Fed.Contexts.Common.Services
{
    public class AuthenticationService : IAuthenticationService
	{
		private readonly HttpClient _httpClient;
		private readonly SecurityConfig _securityConfig;

		public AuthenticationService(HttpClient httpClient, SecurityConfig securityConfig)
		{
			_httpClient = httpClient;
			_securityConfig = securityConfig;
		}

		/// <summary>
		///		Accepts a list of scopes, and returns access token.
		/// </summary>
		/// <param name="scopes">Space separated scopes</param>
		/// <returns></returns>
		public async Task<string> GetAccessToken(string scopes)
		{
			var tokenResponse = await _httpClient.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
			{
				Address = $"{_securityConfig.IdentityServerUrl}/connect/token",
				ClientId = _securityConfig.MateoIdentityClientId,
				ClientSecret = _securityConfig.MateoIdentityClientSecret,
				Scope = scopes
			});

			return tokenResponse != null ? tokenResponse.AccessToken : string.Empty;
		}
	}
}
