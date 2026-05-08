namespace HereditaryDiseaseSystem.Models
{
    public class Phenotype
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public ICollection<GenePhenotype> GenePhenotypes { get; set; } = new HashSet<GenePhenotype>();
    }
}
