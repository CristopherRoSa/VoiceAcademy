using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace VoiceAcademyAPIGeneral.Models;

public partial class Uvcomunity
{
    public int IdUvcomunity { get; set; }

    public string InstitutionalEmail { get; set; } = null!;

    public string StudentNumber { get; set; } = null!;

    public int UserIdUser { get; set; }

    public int DegreeIdDegree { get; set; }
    [JsonIgnore]
    public virtual Degree DegreeIdDegreeNavigation { get; set; } = null!;
    [JsonIgnore]
    public virtual ICollection<Playlist> Playlists { get; set; } = new List<Playlist>();
    [JsonIgnore]
    public virtual ICollection<Podcastchannel> Podcastchannels { get; set; } = new List<Podcastchannel>();
    [JsonIgnore]
    public virtual ICollection<Publicationrequest> Publicationrequests { get; set; } = new List<Publicationrequest>();
    [JsonIgnore]
    public virtual User UserIdUserNavigation { get; set; } = null!;
}
