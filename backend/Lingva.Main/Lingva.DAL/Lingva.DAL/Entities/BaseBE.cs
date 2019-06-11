using System;
using System.Diagnostics.CodeAnalysis;

namespace Lingva.DAL.Entities
{
    [ExcludeFromCodeCoverage]
    public partial class BaseBE
    {
        public int Id { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? ModifyDate { get; set; }
    }
}
