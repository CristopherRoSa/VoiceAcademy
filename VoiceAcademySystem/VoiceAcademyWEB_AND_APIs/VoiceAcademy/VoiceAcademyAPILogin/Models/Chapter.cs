using System;
using System.Collections.Generic;

namespace VoiceAcademyAPILogin.Models;

public partial class Chapter
{
    public int IdChapter { get; set; }

    public string Description { get; set; } = null!;

    public int StateChapter { get; set; }

    public DateTime? PublicationDate { get; set; }

    public string Title { get; set; } = null!;

    public string Topic { get; set; } = null!;

    public byte[]? ImageChapter { get; set; }

    public int PodcastIdPodcast { get; set; }

    public virtual Podcastchannel PodcastIdPodcastNavigation { get; set; } = null!;

    public virtual ICollection<Publicationrequest> Publicationrequests { get; set; } = new List<Publicationrequest>();

    public virtual ICollection<Likeslist> LikesListIdlikeds { get; set; } = new List<Likeslist>();

    public virtual ICollection<Playlist> PlayListIdPlayLists { get; set; } = new List<Playlist>();
}
