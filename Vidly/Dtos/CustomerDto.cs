﻿using System;
using System.ComponentModel.DataAnnotations;

namespace Vidly.Dtos
{
    public class CustomerDto
    {
        public int Id { get; set; }

        [Required()]
        [StringLength(255)]
        public string Name { get; set; }

        public bool IsSubscribedToNewsletter { get; set; }
                        
        public byte MembershipTypeId { get; set; } //tratada como FK por el EF

        public MembershipTypeDto MembershipType { get; set; }

        //[Min18YearsIfaMember]
        public DateTime? Birthday { get; set; }
    }
}