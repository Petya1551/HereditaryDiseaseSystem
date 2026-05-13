namespace HereditaryDiseaseSystem.Models
{
    public class DashboardViewModel
    {
        public int DiseaseCount { get; set; }
        public int GeneCount { get; set; }
        public int PhenotypeCount { get; set; }
        public List<Disease> LatestDiseases { get; set; }
    }
}
