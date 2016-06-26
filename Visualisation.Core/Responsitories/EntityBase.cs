using MongoDB.Bson.Serialization.Attributes;
using System;

namespace Visualisation.Core.Responsitories
{
	public abstract class EntityBase : IEntity
	{
		[BsonId]
		public Guid EntityId { get; set; }
	}

	public interface IEntity
	{
		Guid EntityId { get; set; }
	}
}
