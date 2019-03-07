using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExamenSergioRomeoGNB.Models
{
    public partial class Transaction
    {
        [JsonIgnore]
        public int Id { get; set; }
        public string Sku { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal Amount { get; set; }
        public string Currency { get; set; }
    }
}
