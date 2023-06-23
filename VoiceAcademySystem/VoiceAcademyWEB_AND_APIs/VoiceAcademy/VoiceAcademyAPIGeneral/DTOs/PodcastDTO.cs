using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace VoiceAcademyAPIGeneral.DTOs
{
    public class PodcastDTO
    {
        public int IdPodcast { get; set; }

        [Required(ErrorMessage = "La descripcion del podcast no puede estar vacio")]
        [DisplayName("Descripcion del podcast")]
        public string Description { get; set; } = null!;

        public int StatePodcast { get; set; }

        [Required(ErrorMessage = "El Topic del podcast no puede estar vacio")]
        [DisplayName("Topic del podcast")]
        public string Topic { get; set; } = null!;

        [Required(ErrorMessage = "El Titulo del podcast no puede estar vacio")]
        [DisplayName("Titulo del podcast")]
        public string Title { get; set; } = null!;

        public byte[]? ImagePodcast { get; set; }

        public DateTime? CreationDate { get; set; }

        [Required(ErrorMessage = "El UserId no puede estar vacio")]
        [DisplayName("UserId")]
        public int UvcomunityIdUvcomunity { get; set; }

    }
}
