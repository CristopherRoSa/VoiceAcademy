using System;
using System.Collections.Generic;

namespace VoiceAcademyAPILogin.Models;

public partial class Playlist
{
    public int IdPlayList { get; set; }

    public string? Description { get; set; }

    public DateTime CreationDate { get; set; }

    public string Name { get; set; } = null!;

    public int Followers { get; set; }

    public byte[]? ImagePlayList { get; set; }

    public int? StateList { get; set; }

    public int UvcomunityIdUvcomunity { get; set; }

    public virtual Uvcomunity UvcomunityIdUvcomunityNavigation { get; set; } = null!;

    public virtual ICollection<Chapter> ChapterIdChapters { get; set; } = new List<Chapter>();
}
