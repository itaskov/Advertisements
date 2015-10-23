namespace Advertisements.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Town
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Town")]
        public string Name { get; set; }
    }
}
