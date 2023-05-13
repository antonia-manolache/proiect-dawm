using DataLayer.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Dtos
{
    public class RecipeDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public RecipeType Type { get; set; }

    }
}
