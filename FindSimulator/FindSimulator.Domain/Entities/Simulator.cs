using FindSimulator.Share.Abstract.Model;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindSimulator.Domain.Entities
{
    public   sealed  partial class Simulator: BaseEntity<int>
    {

        public string Name { get; set; }
    }
}
