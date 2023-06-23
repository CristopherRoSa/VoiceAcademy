using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


namespace VoiceAcademyAPI.DTOs
{
    public class NewChapterDTO
    {
        [Required(ErrorMessage = "El Id del Usuario no debe estar vacio")]
        [DisplayName("ID del Usuario")]
        public int idUser { get; set; }
        [Required(ErrorMessage = "El titulo del capitulo no puede estar vacio")]
        [DisplayName("Titulo del capitulo")]
        public string Title { get; set; }
        [Required(ErrorMessage = "La descripcion del capitulo no puede estar vacio")]
        [DisplayName("Descripcion del capitulo")]
        public string Description { get; set; }

        [Required(ErrorMessage = "El nombre del podcast no puede estar vacio")]
        [DisplayName("Nombre del podcast")]
        public string PodcastName { get; set; }
    }
}