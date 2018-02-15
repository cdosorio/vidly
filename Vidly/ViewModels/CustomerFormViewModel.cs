using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Vidly.Models;

namespace Vidly.ViewModels
{
    public class CustomerFormViewModel
    {
        public IEnumerable<MembershipType> MembershipTypes { get; set; }
        //Si el Customer fuera a crecer mucho en el tiempo, aca podríamos pasar solo las propiedades necesarias para crearlo,
        //en vez del objeto completo
        public Customer Customer { get; set; }

    }
}