using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace VoiceAcademyAPIGeneral.Models;

public partial class Likeslist
{
    public int IdlikesList { get; set; }

    public int? TotalChapters { get; set; }

    public int? Followers { get; set; }

    public int UserIdUser { get; set; }
    [JsonIgnore]
    public virtual User UserIdUserNavigation { get; set; } = null!;
    [JsonIgnore]
    public virtual ICollection<Chapter> ChapterIdChapters { get; set; } = new List<Chapter>();
}
