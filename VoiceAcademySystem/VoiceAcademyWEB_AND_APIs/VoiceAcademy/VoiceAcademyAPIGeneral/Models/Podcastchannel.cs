using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace VoiceAcademyAPIGeneral.Models;

public partial class Podcastchannel
{
    public int IdPodcast { get; set; }

    public string Description { get; set; } = null!;

    public int StatePodcast { get; set; }

    public string Topic { get; set; } = null!;

    public string Title { get; set; } = null!;

    public byte[]? ImagePodcast { get; set; }

    public DateTime? CreationDate { get; set; }

    public int UvcomunityIdUvcomunity { get; set; }

    [JsonIgnore]
    public virtual ICollection<Chapter> Chapters { get; set; } = new List<Chapter>();
    [JsonIgnore]
    public virtual Uvcomunity UvcomunityIdUvcomunityNavigation { get; set; } = null!;
    [JsonIgnore]
    public virtual ICollection<User> UserIdUsers { get; set; } = new List<User>();
}
