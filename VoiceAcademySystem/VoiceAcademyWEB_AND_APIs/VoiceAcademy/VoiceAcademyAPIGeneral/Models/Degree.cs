using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace VoiceAcademyAPIGeneral.Models;

public partial class Degree
{
    public int IdDegree { get; set; }

    public string Name { get; set; } = null!;

    public int FacultyIdFaculty { get; set; }
    [JsonIgnore]
    public virtual Faculty FacultyIdFacultyNavigation { get; set; } = null!;
    [JsonIgnore]
    public virtual ICollection<Uvcomunity> Uvcomunities { get; set; } = new List<Uvcomunity>();
}
