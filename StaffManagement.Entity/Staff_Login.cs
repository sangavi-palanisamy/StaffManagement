﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace StaffManagement.Entity
{
    public partial class Staff_Login
    {
        [Key]
        public int StaffId { get; set; }
        [Required]
        [StringLength(50)]
        public string UserName { get; set; }
        [Required]
        [StringLength(50)]
        public string Password { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime Current_time_stamp { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime Update_time_stamp { get; set; }
        public bool Is_Deleted { get; set; }
    }
}