using Quorum.Shared.Interfaces;

namespace Quorum.DataAccess.AdoDataProvider.Extensions
{
    internal static class EntityExtensions
    {
        public static string GetTableName<TEntity>(this TEntity entity) where TEntity : class, IEntity, new()
		{
			return entity.GetType().Name + "s";
		}
    }
}