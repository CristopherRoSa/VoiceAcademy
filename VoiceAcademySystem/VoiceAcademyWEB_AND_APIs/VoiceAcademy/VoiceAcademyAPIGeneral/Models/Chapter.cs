using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace VoiceAcademyAPIGeneral.Models;

public partial class Chapter
{
    public int IdChapter { get; set; }

    public string Description { get; set; } = null!;

    public int StateChapter { get; set; }

    public DateTime? PublicationDate { get; set; }

    public string Title { get; set; } = null!;

    public string Topic { get; set; } = null!;

    public byte[]? ImageChapter { get; set; }

    public string? LinkChapter { get; set; }

    public int PodcastIdPodcast { get; set; }
    [JsonIgnore]
    public virtual Podcastchannel PodcastIdPodcastNavigation { get; set; } = null!;
    [JsonIgnore]
    public virtual ICollection<Publicationrequest> Publicationrequests { get; set; } = new List<Publicationrequest>();
    [JsonIgnore]
    public virtual ICollection<Likeslist> LikesListIdlikeds { get; set; } = new List<Likeslist>();
    [JsonIgnore]
    public virtual ICollection<Playlist> PlayListIdPlayLists { get; set; } = new List<Playlist>();
}
