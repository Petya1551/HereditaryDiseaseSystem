namespace HereditaryDiseaseSystem.Models
{
    public class GenePhenotype
    {
        public int GeneId { get; set; }
        public Gene Gene { get; set; }

        public int PhenotypeId { get; set; }
        public Phenotype Phenotype { get; set; }
    }
}
