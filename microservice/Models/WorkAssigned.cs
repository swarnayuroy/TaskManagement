using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace microservice.Models
{
    /* Sets relation for a work assigned to an user i.e. associated with a topic  */
    public class WorkAssigned
    {
        public Guid Id { get; set; }
        public Guid TopicId { get; set; }
        public Guid WorkId { get; set; }
        public Guid UserId { get; set; }
    }
}