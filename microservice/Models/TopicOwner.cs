using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace microservice.Models
{
    public enum MemberType
    {
        ADMIN=1,
        MEMBER=2
    }

    /* Sets owner of a topic(individual/group) */
    public class TopicOwner
    {
        public Guid TopicId { get; set; }
        public Guid UserId { get; set; }
    }

    /* Sets member of a group topic */
    public class TopicMember
    {
        public Guid TopicId { get; set; }
        public Guid UserId { get; set; }
        public MemberType Member { get; set; }
    }
}