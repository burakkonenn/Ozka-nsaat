using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Cities: BaseEntity
    {
        public string Name { get; set; }      
        public string CountryName { get; set; }      
    }
}
