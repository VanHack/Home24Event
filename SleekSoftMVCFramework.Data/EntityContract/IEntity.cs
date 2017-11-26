using System;

namespace SleekSoftMVCFramework.Data.EntityContract
{
    /// <summary>
    /// Defines interface for base entity type. All entities in the system must implement this interface.
    /// </summary>
    
    public interface IEntity<TPrimaryKey>
    {
        /// <summary>
        /// Unique identifier for this entity.
        /// </summary>
        /// Int64
        TPrimaryKey Id { get; set; }

        DateTime DateCreated { get; set; }

        /// <summary>
        /// Checks if this entity is transient (not persisted to database and it has not an <see cref="Id"/>).
        /// </summary>
        /// <returns>True, if this entity is transient</returns>
        bool IsTransient();

        /// <summary>
        /// To allow soft delete
        /// </summary>
        bool IsDeleted { get; set; }
        /// <summary>
        /// to mark entity as active or inactive
        /// </summary>
        bool IsActive { get; set; }

    }
}