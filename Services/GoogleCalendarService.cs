using System;
using System.IO;
using System.Threading.Tasks;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Calendar.v3;
using Google.Apis.Calendar.v3.Data;
using Google.Apis.Services;
using Microsoft.Extensions.Configuration;
using MyWebApi.model;

namespace InterviewManager.Services
{
    public class GoogleCalendarService
    {
        private readonly IConfiguration _configuration;
        private readonly CalendarService _calendarService;

        public GoogleCalendarService(IConfiguration configuration)
        {
            _configuration = configuration;
            _calendarService = GetCalendarService();
        }

        private CalendarService GetCalendarService()
        {
            var credentialFilePath = _configuration["GoogleCredentialFile"];
            using (var stream = new FileStream(credentialFilePath, FileMode.Open, FileAccess.Read))
            {
                var credential = GoogleCredential.FromStream(stream)
                    .CreateScoped(CalendarService.Scope.Calendar);
                return new CalendarService(new BaseClientService.Initializer
                {
                    HttpClientInitializer = credential,
                    ApplicationName = "Interview Manager",
                });
            }
        }

        public async Task CreateInterviewEvent(string calendarId, Interview interview)
        {
            var startTime = DateTime.Parse(interview.InterviewDate);
            var endTime = startTime.AddHours(1);
            var interviewEvent = new Event
            {
                Summary = $"Colloquio: {interview.Position}",
                Description = $"Candidato: {interview.ApplicantName}",
                Start = new EventDateTime
                {
                    DateTime = startTime,
                    TimeZone = "Europe/Rome",
                },
                End = new EventDateTime
                {
                    DateTime = endTime,
                    TimeZone = "Europe/Rome",
                },
                Reminders = new Event.RemindersData
                {
                    UseDefault = true,
                },
            };
            await _calendarService.Events.Insert(interviewEvent, calendarId).ExecuteAsync();
        }
    }
}
