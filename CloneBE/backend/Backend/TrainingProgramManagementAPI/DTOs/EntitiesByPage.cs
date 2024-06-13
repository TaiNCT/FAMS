using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrainingProgramManagementAPI.DTOs
{
    public class EntitiesByPage<T>
    {
        public IEnumerable<T> List { get; set; } = new List<T>();

        public int TotalPage { get; set; }

        public int TotalRecord { get; set; }

    }
}