using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace VoiceAcademyAPIGeneral.Models;

public partial class Faculty
{
    public int IdFaculty { get; set; }

    public string Name { get; set; } = null!;

    public int RegionIdRegion { get; set; }
    [JsonIgnore]
    public virtual ICollection<Degree> Degrees { get; set; } = new List<Degree>();
    [JsonIgnore]
    public virtual Region RegionIdRegionNavigation { get; set; } = null!;
}
