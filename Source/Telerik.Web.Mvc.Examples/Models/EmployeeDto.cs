namespace Telerik.Web.Mvc.Examples.Models
{
    using System;
    using System.Runtime.Serialization;
    using System.ComponentModel.DataAnnotations;

    [KnownType(typeof(EmployeeDto))]
    public class EmployeeDto
    {
        public int EmployeeID 
        { 
            get; 
            set; 
        }

        [Required]
        public string FirstName
        {
            get;
            set;
        }

        [Required]
        public string LastName
        {
            get;
            set;
        }

        [Required]
        public string Notes
        {
            get;
            set;
        }
    }
}