using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API_Service.DTO
{
    public class TaskCommentDTO
    {
        public Guid Id { get; set; }
        public Guid TaskId { get; set; }
        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public string Comment { get; set; }
        public DateTime CommentedAt { get; set; }
        public DateTime EditedAt { get; set; }
        public DateTime DeletedAt { get; set; }
    }
}