using System.ComponentModel.DataAnnotations;

namespace HereditaryDiseaseSystem.Models
{
    public class Phenotype
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Phenotype name is required.")]
        [StringLength(100)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Description is required.")]
        [StringLength(500)]
        public string Description { get; set; }

        public ICollection<GenePhenotype> GenePhenotypes { get; set; } = new HashSet<GenePhenotype>();
    }
}
