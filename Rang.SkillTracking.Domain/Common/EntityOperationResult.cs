using System.Collections.Generic;

namespace Rang.SkillTracking.Domain.Common
{
    public class EntityOperationResult<Tentity>
    {
        // fields
        private List<Tentity> _entityList;

        // properties
        public OperationStatusCode OperationStatusCode { get; private set; }
        public ICollection<Tentity> EntityCollection { get => _entityList; }
        public int EntityCollectionCount { get => _entityList.Count; }

        // constructors
        public EntityOperationResult(OperationStatusCode operationStatusCode, ICollection<Tentity> entityCollection = null)
        {
            OperationStatusCode = operationStatusCode;
            _entityList = new List<Tentity>();

            if(entityCollection != null && entityCollection.Count > 0)
                _entityList.AddRange(entityCollection);
        }

        public EntityOperationResult(OperationStatusCode operationStatusCode, Tentity entity)
        {
            OperationStatusCode = operationStatusCode;
            _entityList = new List<Tentity>();

            if (entity != null)
                _entityList.Add(entity);
        }

        // methods
    }
}
