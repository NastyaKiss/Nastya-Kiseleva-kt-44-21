using System.Text.Json.Serialization;
using AnastasiaKiselevaKt_44_21.Models;

namespace _1_lab.Models
{
    public class Course
    {
        public int CourseId { get; set; }
        public string? Title { get; set; }
        public int GroupId { get; set; }

        [JsonIgnore]
        public Group? Group { get; set; }
        //public Group? Group { get; set; }
    }
}
