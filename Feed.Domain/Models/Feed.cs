using System;

namespace Template.Domain.Models
{
    public class Feed
    {
        public string Id { get; set; }

        public DateTime CreatedAt { get; set; }
        
        public string Text { get; set; }

        public string CreatedBy { get; set; }
    }
}
