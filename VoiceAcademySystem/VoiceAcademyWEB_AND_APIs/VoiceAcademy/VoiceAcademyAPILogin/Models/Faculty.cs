using System;
using System.Collections.Generic;

namespace VoiceAcademyAPILogin.Models;

public partial class Faculty
{
    public int IdFaculty { get; set; }

    public string Name { get; set; } = null!;

    public int RegionIdRegion { get; set; }

    public virtual ICollection<Degree> Degrees { get; set; } = new List<Degree>();

    public virtual Region RegionIdRegionNavigation { get; set; } = null!;
}
