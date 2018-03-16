﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.ModelBinding;

namespace Vidly.Models.DataTables
{
    [ModelBinder(typeof(DataTableModelBinder))]
    public class DataTableRequest
    {
        //C# naming conventions : the first letter of the property name capitalize
        public int Draw { get; set; }
        public int Start { get; set; }
        public int Length { get; set; }

        public DataTableOrder[] Order { get; set; }
        public DataTableColumn[] Columns { get; set; }
        public DataTableSearch Search { get; set; }
    }
}