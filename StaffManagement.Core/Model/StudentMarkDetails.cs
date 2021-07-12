using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StaffManagement.Core.Model
{
    public class StudentMarkDetails
    {
        public int MarkId { get; set; }
        public string Roll_No { get; set; }
        public string Name { get; set; }
        public int Tamil { get; set; }
        public int English { get; set; }
        public int Maths { get; set; }
        public int Science { get; set; }
        public int Social { get; set; }
        public int Total { get; set; }
        public double Average { get; set; }

    }
}
