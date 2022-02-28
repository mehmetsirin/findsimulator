using FindSimulator.Share.Abstract.Model;


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace FindSimulator.Share.Concrete.Model
{
    public class Document : IEntity<int>
    {
        
        public Document()
        {
            //Id = Guid.NewGuid();
            AddedAtUtc = DateTime.UtcNow;
        }

       
        public int ID { get; set; }

        /// <summary>
        /// The datetime in UTC at which the document was added.
        /// </summary>
        public DateTime AddedAtUtc { get; set; }

        /// <summary>
        /// The version of the schema of the document
        /// </summary>
        
        public string Path { get; set; }
        public string Type { get; set; }
        public int Version { get; set; }
        public DateTime InsertDate { get; set; }
        public bool IsActive { get; set; }
    }
}
