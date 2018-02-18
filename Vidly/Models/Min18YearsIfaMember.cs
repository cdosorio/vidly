using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Vidly.Models
{
    public class Min18YearsIfaMember:ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            //no puede ser usado por el customerDTO del webaPI, ya que aca castea a Customer.
            var customer = (Customer) validationContext.ObjectInstance;

            // Alternativa propuesta para poder usarlo desde el WEBAPI:

            //Customer customer = new Customer();
            //if (validationContext.ObjectType == typeof(Customer))
            //    customer = (Customer)validationContext.ObjectInstance;
            //else
            //    customer = Mapper.Map((CustomerDto)validationContext.ObjectInstance, customer);



            if (customer.MembershipTypeId == MembershipType.Unknown || customer.MembershipTypeId == MembershipType.PayAsYouGo)
                return ValidationResult.Success;

            if (customer.Birthday == null)
                return  new ValidationResult("Birthday is required");

            var age = DateTime.Today.Year - customer.Birthday.Value.Year;

            return (age >= 18) ? ValidationResult.Success : new ValidationResult("Age must be at least 18");


        }
    }
}