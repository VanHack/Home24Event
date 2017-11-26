using System;
using System.Collections.Generic;
using SleekSoftMVCFramework.Data.EntityContract;

namespace SleekSoftMVCFramework.Data.EntityBase
{
    public class Entity<TPrimaryKey> : IEntity<TPrimaryKey>
    {
        public Entity()
        {
            IsActive = true;
            IsDeleted = false;
            DateCreated = DateTime.Now;
        }

        public TPrimaryKey Id { get; set; }
        public DateTime DateCreated { get; set; }
        public bool IsTransient()
        {
            return EqualityComparer<TPrimaryKey>.Default.Equals(Id, default(TPrimaryKey));
        }
        public bool IsDeleted { get; set; }
        public bool IsActive { get; set; }
    }
}