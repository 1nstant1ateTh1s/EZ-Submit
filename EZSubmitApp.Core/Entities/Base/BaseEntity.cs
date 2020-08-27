using System;

namespace EZSubmitApp.Core.Entities.Base
{
    public abstract class BaseEntity<TId> : IBaseEntity<TId>
    {
        //public virtual TId Id { get; protected set; }
        public virtual TId Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
