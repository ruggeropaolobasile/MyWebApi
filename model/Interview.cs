using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyWebApi.model
{
    public class Interview
    {
        public int Id { get; set; }
        public string ApplicantName { get; set; }
        public string Position { get; set; }
        public string InterviewDate { get; set; }
        public string? Status { get; set; } // New, InProgress, Completed, etc.
    }
}
