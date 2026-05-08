namespace HereditaryDiseaseSystem.Models
{
    public class Gene
    {
        public int Id { get; set; }

        public string Symbol { get; set; }

        public string ChromosomeLocation { get; set; }

        public ICollection<GenePhenotype> GenePhenotypes { get; set; } = new List<GenePhenotype>();

        public ICollection<GeneDisease> GeneDiseases { get; set; } = new List<GeneDisease>();
    }
}
