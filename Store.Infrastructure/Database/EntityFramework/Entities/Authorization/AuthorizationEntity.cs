using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Store.Infrastructure.Database.EntityFramework.Common;

namespace Store.Infrastructure.Database.EntityFramework.Entities.Authorization;

[Table("AuthorizationP", Schema = "CTR")]
public class AuthorizationEntity : BaseEntity, IIdentifiable
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("id")]
    public int Id { get; set; }

    [Required]
    [Column("date")]
    public DateTime Date { get; set; }

    [Required]
    [Column("state")]
    public bool State { get; set; }

    [Required]
    [Column("description")]
    [StringLength(500)]
    public string Description { get; set; }
}