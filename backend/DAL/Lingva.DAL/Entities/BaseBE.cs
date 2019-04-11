using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace Lingva.DAL.Entities
{
    [ExcludeFromCodeCoverage]
    public class BaseBE
    {
        public int Id { get; set; }
        public DateTime? CreateDate { get; set; }
        [Column("modify")]
        public DateTime? ModifyDate { get; set; }
    }
}
