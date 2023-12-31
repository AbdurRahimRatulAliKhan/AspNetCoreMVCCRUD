﻿namespace AspNetCoreMVCCRUD.Models
{
    public class AddEmployeeViewModel
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public long? Salary { get; set; }
        public DateTime? DateofBirth { get; set; }
        public string? Department { get; set; }
    }
}
