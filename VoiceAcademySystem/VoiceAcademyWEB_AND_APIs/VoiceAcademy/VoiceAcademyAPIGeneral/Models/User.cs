using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace VoiceAcademyAPIGeneral.Models;

public partial class User
{
    public int IdUser { get; set; }

    public string Email { get; set; } = null!;

    public int Age { get; set; }

    public string Name { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string Password { get; set; } = null!;

    public int StatusUser { get; set; }

    public byte[]? ImageUser { get; set; }

    public string Rol { get; set; } = null!;
    [JsonIgnore]
    public virtual ICollection<Likeslist> Likeslists { get; set; } = new List<Likeslist>();
    [JsonIgnore]
    public virtual Uvcomunity? Uvcomunity { get; set; }
    [JsonIgnore]
    public virtual ICollection<Podcastchannel> PodcastChannelIdPodcasts { get; set; } = new List<Podcastchannel>();
}
