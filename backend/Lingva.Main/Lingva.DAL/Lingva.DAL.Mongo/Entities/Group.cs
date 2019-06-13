using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Diagnostics.CodeAnalysis;

namespace Lingva.DAL.Mongo.Entities
{
    [ExcludeFromCodeCoverage]
    [ModelMetadataType(typeof(GroupMetadata))]
    public partial class Group
    {
        internal sealed class GroupMetadata
        {
            [BsonDateTimeOptions]
            public DateTime Date { get; set; }
        }
    }
}
