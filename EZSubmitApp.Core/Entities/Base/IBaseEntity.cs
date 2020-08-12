using System;

namespace EZSubmitApp.Core.Entities.Base
{
    /// <summary>
    /// Allows type constraints to be applied to all entities specific to this EZ Submit app, regardless
    /// of whether they inherit from the shared BaseEntity, or IdentityUser (in the case of the User entity).
    /// </summary>
    public interface IBaseEntity<TId>
    {
        TId Id { get; }
        DateTime CreatedDate { get; set; }
        DateTime? ModifiedDate { get; set; }
    }
}
