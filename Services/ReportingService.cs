using System;
using System.Collections.Generic;
using System.Linq;
using MyWebApi;

namespace InterviewManager.Services
{
    public class ReportingService
    {
        private readonly ApplicationDbContext _context;

        public ReportingService(ApplicationDbContext context)
        {
            _context = context;
        }

        public Dictionary<string, int> GetInterviewsByStatus()
        {
            return _context.Interviews
                .GroupBy(i => i.Status)
                .ToDictionary(g => g.Key, g => g.Count());
        }

        public Dictionary<string, int> GetInterviewsByPosition()
        {
            return _context.Interviews
                .GroupBy(i => i.Position)
                .ToDictionary(g => g.Key, g => g.Count());
        }

        public Dictionary<string, int> GetInterviewsByMonth()
        {
            return _context.Interviews
                .GroupBy(i => i.InterviewDate.Substring(0, 7))
                .ToDictionary(g => g.Key, g => g.Count());
        }
    }
}
