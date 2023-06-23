using System;
using System.Collections.Generic;

namespace VoiceAcademyAPILogin.Models;

public partial class Degree
{
    public int IdDegree { get; set; }

    public string Name { get; set; } = null!;

    public int FacultyIdFaculty { get; set; }

    public virtual Faculty FacultyIdFacultyNavigation { get; set; } = null!;

    public virtual ICollection<Uvcomunity> Uvcomunities { get; set; } = new List<Uvcomunity>();
}
