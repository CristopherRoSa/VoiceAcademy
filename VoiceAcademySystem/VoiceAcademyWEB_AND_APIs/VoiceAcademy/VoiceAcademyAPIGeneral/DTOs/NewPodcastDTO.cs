using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using VoiceAcademyAPI.Utility;

namespace VoiceAcademyAPI.DTOs
{
    public class NewPodcastDTO
    {
        [Required(ErrorMessage = "La descripcion del podcast no puede estar vacio")]
        [DisplayName("Descripcion del podcast")]
        public string Description { get; set; }
        [Required(ErrorMessage = "El Topic del podcast no puede estar vacio")]
        [DisplayName("Tema del podcast")]
        public string Topic { get; set; }
        [Required(ErrorMessage = "El titulo del podcast no puede estar vacio")]
        [DisplayName("Titulo del podcast")]
        public string Title { get; set; }
        [Required(ErrorMessage ="El id del usuario comunidad uv no puede estar vacio")]
        [DisplayName("Id del Usuario ComunidadUV")]
        public int UvcomunityIdUvcomunity { get; set; }
    }

    
}
