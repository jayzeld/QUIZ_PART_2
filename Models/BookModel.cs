using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPIDiscussion.Models
{
    public class Bookmodel
    {
        [ForeignKey("Id")]
        public int Id { get; set; }
        public string Author { get; set; }
        public string Title { get; set; }
        public string PublisherName { get; set; }
        public DateTime DatePublished { get; set; }
        public DateTime DateAdded { get; set; }
    }
}

