using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Store.Infrastructure.Database.EntityFramework.Common;

namespace Store.Infrastructure.Database.EntityFramework.Entities.Store;

[Table("Store", Schema = "CTR")]
public class StoreEntity : BaseEntity, IIdentifiable
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("id")]
    public int Id { get; set; }

    [Required]
    [Column("name")]
    [StringLength(100)]
    public string Name { get; set; }

    [Required]
    [Column("ubication")]
    [StringLength(200)]
    public string Ubication { get; set; }

    [Required]
    [Column("capacity")]
    public int Capacity { get; set; }
}