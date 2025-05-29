// Models/CrimeRecord.cs
namespace CrimeAnalysisSystem.Models
{
    public class CrimeRecord
    {
        public int Id { get; set; }
        public string ArrestKey { get; set; }
        public DateTime ArrestDate { get; set; }
        public string PdDesc { get; set; }
        public string OfnsDesc { get; set; }
        public string LawCatCd { get; set; }
        public string AgeGroup { get; set; }
        public string PerpSex { get; set; }
        public string PerpRace { get; set; }
        public int LocationId { get; set; }
        public Location Location { get; set; }
    }
}
