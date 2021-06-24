using System;
namespace TodoAPIDTO.Domain.Exceptions
{
    public class EntityNotFoundException : Exception
    {
        private const string ERROR_TEMPLATE = "Entity {0} with primary key {1} not found";
        public readonly string EntityName;
        public readonly object EntityId;

        // hide default constructor
        private EntityNotFoundException()
        {
        }

        public EntityNotFoundException(string entityName, object entityId) 
        {
            if (entityName == null)
                throw new ArgumentNullException(nameof(entityName));

            if (entityId == null)
                throw new ArgumentException(nameof(entityId));

            EntityName = entityName;
            EntityId = entityId;
        }

        public override string Message
        {
            get
            {
                return string.Format(ERROR_TEMPLATE, EntityName, EntityId);
            }
        }
    }
}
