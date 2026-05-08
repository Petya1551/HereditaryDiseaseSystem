namespace HereditaryDiseaseSystem.Models
{
    public class GeneDisease
    {
        public int GeneId { get; set; }
        public Gene Gene { get; set; }

        public int DiseaseId { get; set; }
        public Disease Disease { get; set; }
    }
}
