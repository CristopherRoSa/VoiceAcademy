using System;
using System.Collections.Generic;

namespace VoiceAcademyAPILogin.Models;

public partial class Region
{
    public int IdRegion { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Faculty> Faculties { get; set; } = new List<Faculty>();
}
