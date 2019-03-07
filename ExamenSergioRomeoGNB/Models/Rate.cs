using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;

namespace GNBSergioRP.Models
{
    public partial class Rate
    {
        [JsonIgnore]
        public int Id { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        [JsonProperty("rate")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal RateVal { get; set; }
    }
}
