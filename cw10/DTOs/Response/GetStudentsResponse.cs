using cw10.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cw10.DTOs.Response
{
    public class GetStudentsResponse
    {
        public List<Student> Students { get; set; }
    }
}
