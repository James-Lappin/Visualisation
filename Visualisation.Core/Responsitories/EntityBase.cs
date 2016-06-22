using System;

namespace Visualisation.Core.Responsitories
{
	public abstract class EntityBase : IEntity
	{
		public Guid Id { get; set; }
	}

	public interface IEntity
	{
		Guid Id { get; set; }
	}
}
