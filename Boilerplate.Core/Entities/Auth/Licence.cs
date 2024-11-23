#nullable disable
using System.ComponentModel.DataAnnotations.Schema;

namespace Boilerplate.Core.Entities.Auth
{
    [Table("licences")]
    public class Licence : BaseEntityUpdate
    {
        [Column("public_key")]
        public string PublicKey { get; set; }
        [Column("licence_key")]
        public string LicenceKey { get; set; }
    }
}
