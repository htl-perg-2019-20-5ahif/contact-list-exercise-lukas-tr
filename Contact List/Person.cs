using System;
using System.ComponentModel.DataAnnotations;


namespace Contact_List
{
    public class Person
    {
        [Required]
        public Int32 id { get; set; }

        public string firstName { get; set; }

        public string lastName { get; set; }

        [Required]
        public string email { get; set; }

    }
}
