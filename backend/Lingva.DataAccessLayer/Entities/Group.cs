using System.ComponentModel.DataAnnotations;

namespace Lingva.DataAccessLayer.Entities
{
    public class Group
    {
        [Key]
        public int Id { get; set; }

        public string Title { get; set; }
        public string Description { get; set; }
        public string Picture { get; set; }
    }
}
