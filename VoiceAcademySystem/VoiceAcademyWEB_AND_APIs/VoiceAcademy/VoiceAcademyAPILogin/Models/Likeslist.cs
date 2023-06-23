using System;
using System.Collections.Generic;

namespace VoiceAcademyAPILogin.Models;

public partial class Likeslist
{
    public int IdlikesList { get; set; }

    public int? TotalChapters { get; set; }

    public int? Followers { get; set; }

    public int UserIdUser { get; set; }

    public virtual User UserIdUserNavigation { get; set; } = null!;

    public virtual ICollection<Chapter> ChapterIdChapters { get; set; } = new List<Chapter>();
}
