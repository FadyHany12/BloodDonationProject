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

    public partial class Hospital_Request
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Commissary Ssn is required")]
        public string commissary_ssn { get; set; }


        [Required(AllowEmptyStrings = false, ErrorMessage = "Commissary name is required")]
        public string commissary_name { get; set; }



        [Required(AllowEmptyStrings = false, ErrorMessage = "Blood type is required")]
        public string Bloodtype { get; set; }


        [Required(AllowEmptyStrings = false, ErrorMessage = "Hospital id is required")]
        public int Hospital_Id { get; set; }



        [Required(AllowEmptyStrings = false, ErrorMessage = "permission is required")]
        public string permission_path { get; set; }



        public int permission_ID { get; set; }



        [Required(AllowEmptyStrings = false, ErrorMessage = "Status is required")]
        public string Status { get; set; }

        public virtual Hospital Hospital { get; set; }
    }
}
