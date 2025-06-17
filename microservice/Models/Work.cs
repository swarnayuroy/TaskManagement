using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace microservice.Models
{
    public enum WorkStatus
    {
        Discarded = -1,
        Created = 0,
        ToDo = 1,
        InProgress = 2,
        Completed = 3,
        OnHold = 4
    }
    public class Work
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public WorkStatus Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime EstimatedCompletion { get; set; }
        public DateTime StartedAt { get; set; }
        public DateTime EndedAt { get; set; }
    }
}