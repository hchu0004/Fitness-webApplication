//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AssignmentV3.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Reservation
    {
        public int Id { get; set; }
        public int ProgramId { get; set; }
        public System.DateTime DatetimeStart { get; set; }
        public System.DateTime DatetimeEnd { get; set; }
        public string Comment { get; set; }
        public string Status { get; set; }
        public string UserId { get; set; }
        public string memberEmail { get; set; }
    
        public virtual Program Program { get; set; }
    }
}
