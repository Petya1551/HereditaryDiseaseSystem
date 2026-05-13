using System.ComponentModel.DataAnnotations;

namespace HereditaryDiseaseSystem.Models
{
    public class Disease
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Disease name is required.")]
        [StringLength(100, ErrorMessage = "Maximum length is 100 characters.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Description is required.")]
        [StringLength(500)]
        public string Description { get; set; }

        public ICollection<GeneDisease> GeneDiseases { get; set; } = new List<GeneDisease>();
    }
}
