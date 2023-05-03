using System;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace InterviewManager.Services
{
    public class ZoomService
    {
        private readonly IConfiguration _configuration;
        private readonly HttpClient _httpClient;

        public ZoomService(IConfiguration configuration)
        {
            _configuration = configuration;
            _httpClient = new HttpClient();
        }

        private string GenerateZoomToken()
        {
            var clientId = _configuration["ZoomClientId"];
            var clientSecret = _configuration["ZoomClientSecret"];
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(clientSecret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = clientId,
                Expires = DateTime.UtcNow.AddMinutes(15),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public async Task<string> CreateZoomMeeting(string topic, DateTime startTime)
        {
            var token = GenerateZoomToken();
            var requestUri = "https://api.zoom.us/v2/users/me/meetings";
            var requestContent = new
            {
                topic,
                type = 2, // Scheduled meeting
                start_time = startTime.ToString("yyyy-MM-ddTHH:mm:ssZ"),
                duration = 60, // 60 minutes
                timezone = "Europe/Rome",
            };
            var jsonContent = JsonSerializer.Serialize(requestContent);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await _httpClient.PostAsync(requestUri, content);
            var responseContent = await response
.Content.ReadAsStringAsync();


            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = JsonDocument.Parse(responseContent);
                var joinUrl = jsonResponse.RootElement.GetProperty("join_url").GetString();
                return joinUrl;
            }
            else
            {
                throw new InvalidOperationException($"Failed to create Zoom meeting: {responseContent}");
            }
        }
    }
}