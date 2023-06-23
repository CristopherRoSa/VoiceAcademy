using System;
using System.Collections.Generic;

namespace VoiceAcademyAPILogin.Models;

public partial class Publicationrequest
{
    public int IdPublicationRequest { get; set; }

    public DateTime CreationDate { get; set; }

    public int StatusRequest { get; set; }

    public int ChapterIdChapter { get; set; }

    public int UvcomunityIdUvcomunity { get; set; }

    public virtual Chapter ChapterIdChapterNavigation { get; set; } = null!;

    public virtual Uvcomunity UvcomunityIdUvcomunityNavigation { get; set; } = null!;
}
