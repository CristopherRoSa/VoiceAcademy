using System;
using System.Collections.Generic;

namespace VoiceAcademyAPILogin.Models;

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

    public virtual ICollection<Chapter> Chapters { get; set; } = new List<Chapter>();

    public virtual Uvcomunity UvcomunityIdUvcomunityNavigation { get; set; } = null!;

    public virtual ICollection<User> UserIdUsers { get; set; } = new List<User>();
}
