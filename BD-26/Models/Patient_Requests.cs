//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BD_26.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Web;

    public partial class Patient_Requests
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Patient Ssn is required")]
        public string Patient_Ssn { get; set; }


        [Required(AllowEmptyStrings = false, ErrorMessage = "Patient name is required")]
        public string Patient_name { get; set; }



        [Required(AllowEmptyStrings = false, ErrorMessage = "Patient Blood type is required")]
        public string bloodtype { get; set; }



        public int permission_ID { get; set; }



        [Required(AllowEmptyStrings = false, ErrorMessage = "Permission is required")]
        public string permission_path { get; set; }




        [Required(AllowEmptyStrings = false, ErrorMessage = "Status is required")]
        public string Status { get; set; }




        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter the file is required")]
        public HttpPostedFileBase Imagefile { get; set; }
    }
}