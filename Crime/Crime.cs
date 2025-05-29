using CsvHelper.Configuration;

// Crime classes without namespace
public class CrimeRecord
{
    public string ArrestKey { get; set; }
    public string ArrestDate { get; set; }
    public string PdDesc { get; set; }
    public string OfnsDesc { get; set; }
    public string LawCatCd { get; set; }
    public string AgeGroup { get; set; }
    public string PerpSex { get; set; }
    public string PerpRace { get; set; }
    public string LonLat { get; set; }
}

public sealed class CrimeRecordMap : ClassMap<CrimeRecord>
{
    public CrimeRecordMap()
    {
        Map(m => m.ArrestKey).Name("arrest_key");
        Map(m => m.ArrestDate).Name("arrest_date");
        Map(m => m.PdDesc).Name("pd_desc");
        Map(m => m.OfnsDesc).Name("ofns_desc");
        Map(m => m.LawCatCd).Name("law_cat_cd");
        Map(m => m.AgeGroup).Name("age_group");
        Map(m => m.PerpSex).Name("perp_sex");
        Map(m => m.PerpRace).Name("perp_race");
        Map(m => m.LonLat).Name("lon_lat");
    }
}