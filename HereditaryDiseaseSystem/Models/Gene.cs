using System.ComponentModel.DataAnnotations;

namespace HereditaryDiseaseSystem.Models
{
    public class Gene
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Gene symbol is required.")]
        [StringLength(20, ErrorMessage = "Maximum length is 20 characters.")]
        public string Symbol { get; set; }

        [Required(ErrorMessage = "Chromosome location is required.")]
        [StringLength(50, ErrorMessage = "Maximum length is 50 characters.")]
        public string ChromosomeLocation { get; set; }

        public ICollection<GenePhenotype> GenePhenotypes { get; set; } = new List<GenePhenotype>();

        public ICollection<GeneDisease> GeneDiseases { get; set; } = new List<GeneDisease>();
    }
}
