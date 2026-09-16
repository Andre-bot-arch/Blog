using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BLog.ViewModels
{
    public class EditorCategoryViewModel
    {
        [Required]
        public string Name { get; set; }
        public string Slug { get; set; }
    }
}