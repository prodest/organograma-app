using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    public class Site
    {
        public string i { get; set; }
        [Display(Name = "URL")]
        public string url { get; set; }                
    }
}