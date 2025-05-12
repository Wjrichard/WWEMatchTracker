using Microsoft.AspNetCore.Mvc.Diagnostics;
using System.Buffers.Text;

namespace TestAPI.Models
{
    public class Event
    {
        public int EventId { get; set; }
        public string EventName { get; set; }
        public string EventImage { get; set; }
        public string EventDate { get; set; }
    }
}
