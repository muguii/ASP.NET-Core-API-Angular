using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProAgil.API.DTOs
{
    public class EventoDTO
    {
        //É possivel integrar o DataAnottation com o Angular. ESTUDAR ISSO!

        public int Id { get; set; }

        [Required(ErrorMessage = "Tema é obrigatório.")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Local deve ser entre 3 e 100 caracteres.")]
        public string Local { get; set; }

        public string DataEvento { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório.")] // {0} vai ir sempre o nome do atributo
        public string Tema { get; set; }

        [Range(2, 120000, ErrorMessage = "Quantidade de pessoas deve ser entre 2 e 120000.")]
        public int QtdPessoas { get; set; }

        public string ImagemURL { get; set; }

        [Phone]
        public string Telefone { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        public List<LoteDTO> Lotes { get; set; }

        public List<RedeSocialDTO> RedesSociais { get; set; }

        public List<PalestranteDTO> Palestrantes { get; set; }
    }
}
