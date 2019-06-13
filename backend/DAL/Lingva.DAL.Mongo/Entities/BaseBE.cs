using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Diagnostics.CodeAnalysis;

namespace Lingva.DAL.Mongo.Entities
{
    [ExcludeFromCodeCoverage]
    [ModelMetadataType(typeof(BaseBEMetadata))]
    public partial class BaseBE
    {
        internal sealed class BaseBEMetadata
        {
            [BsonId]
            public int Id { get; set; }
            [BsonDateTimeOptions]
            public DateTime? CreateDate { get; set; }
            [BsonDateTimeOptions]
            public DateTime? ModifyDate { get; set; }
        }
    }
}
