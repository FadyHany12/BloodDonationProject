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

    public partial class Hospital
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Hospital()
        {
            this.Hospital_Request = new HashSet<Hospital_Request>();
        }

        public int Hospital_id { get; set; }


        [Required(AllowEmptyStrings = false, ErrorMessage = "hospital name is required")]
        public string Hospital_name { get; set; }



        [Required(AllowEmptyStrings = false, ErrorMessage = "address is  required")]
        public string Hospital_address { get; set; }



        [Required(AllowEmptyStrings = false, ErrorMessage = "Phone number is required")]
        public string Hospital_phone { get; set; }



        [Required(AllowEmptyStrings = false, ErrorMessage = " Email is required")]
        [DataType(DataType.EmailAddress)]
        public string Hospital_email { get; set; }


        [Required(AllowEmptyStrings = false, ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Hospital_Request> Hospital_Request { get; set; }
    }
}