using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MvcApplication2.Models
{
    public class DatePrompt
    {
        [Required(ErrorMessage = "此為必要欄位")]
        public string strDataDate { get; set; }
        public string strEndDate { get; set; }

    }
}