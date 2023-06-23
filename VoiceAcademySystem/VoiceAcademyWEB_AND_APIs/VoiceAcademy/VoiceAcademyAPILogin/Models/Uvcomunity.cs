using System;
using System.Collections.Generic;

namespace VoiceAcademyAPILogin.Models;

public partial class Uvcomunity
{
    public int IdUvcomunity { get; set; }

    public string InstitutionalEmail { get; set; } = null!;

    public string StudentNumber { get; set; } = null!;

    public int UserIdUser { get; set; }

    public int DegreeIdDegree { get; set; }

    public virtual Degree DegreeIdDegreeNavigation { get; set; } = null!;

    public virtual ICollection<Playlist> Playlists { get; set; } = new List<Playlist>();

    public virtual ICollection<Podcastchannel> Podcastchannels { get; set; } = new List<Podcastchannel>();

    public virtual ICollection<Publicationrequest> Publicationrequests { get; set; } = new List<Publicationrequest>();

    public virtual User UserIdUserNavigation { get; set; } = null!;
}
