

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindSimulator.Share.Abstract.Model
{
    public interface IEntity<TKey> where TKey : IEquatable<TKey>
    {
        TKey ID { get; set; }
        public DateTime InsertDate { get; set; }
        public bool IsActive { get; set; }

    }
    //public interface IEntity : IEntity<Guid>
    //{
    //}

    public abstract class BaseEntity<TKey> : IEntity<TKey> where TKey : IEquatable<TKey>
    {
        public BaseEntity()
        {
            InsertDate = DateTime.Now;
            //Id = Guid.NewGuid();
            IsActive = true;
        }

        public BaseEntity(TKey id, DateTime insertDate, bool isActive)
        {
            this.ID = id;
            this.InsertDate = insertDate;
            IsActive = isActive;
        }

        public TKey ID { get; set; }
        public DateTime InsertDate { get; set; }
        public bool IsActive { get; set; }

   
    }
}
