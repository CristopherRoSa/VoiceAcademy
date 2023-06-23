using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace VoiceAcademyAPIGeneral.Models;

public partial class Publicationrequest
{
    public int IdPublicationRequest { get; set; }

    public DateTime CreationDate { get; set; }

    public int StatusRequest { get; set; }

    public int ChapterIdChapter { get; set; }

    public int UvcomunityIdUvcomunity { get; set; }
    [JsonIgnore]
    public virtual Chapter ChapterIdChapterNavigation { get; set; } = null!;
    [JsonIgnore]
    public virtual Uvcomunity UvcomunityIdUvcomunityNavigation { get; set; } = null!;
}
