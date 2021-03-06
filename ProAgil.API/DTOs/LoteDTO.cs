using System.ComponentModel.DataAnnotations;

namespace ProAgil.API.DTOs
{
    public class LoteDTO
    {
        public int Id { get; set; }

        public string Nome { get; set; }

        public decimal Preco { get; set; }

        public string DataInicio { get; set; }

        public string DataFim { get; set; }

        [Range(2, 120000)]
        public int Quantidade { get; set; }
    }
}
