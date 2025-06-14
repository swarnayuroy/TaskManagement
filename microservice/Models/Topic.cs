using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace microservice.Models
{
    public enum ExecutionType
    {
        Individual = 1,
        Group = 2
    }
    public class Topic
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public int ExecutionMode { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}