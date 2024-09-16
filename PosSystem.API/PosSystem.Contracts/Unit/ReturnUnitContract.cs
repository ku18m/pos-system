using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PosSystem.Contracts.Unit
{
    public class ReturnUnitContract
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string? Note { get; set; }
    }
}
