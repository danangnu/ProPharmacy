namespace API.Entities
{
    public class ExpenseSummary
    {
        public int Id { get; set; }
        public int ExpMonth { get; set; }
        public double DirectorSalary { get; set; }
        public double EmployeeSalary { get; set; }
        public double LocumCost { get; set; }
        public double OtherCost { get; set; }
        public double Rent { get; set; }
        public double Rates { get; set; }
        public double Utilities { get; set; }
        public double Telephone { get; set; }
        public double Repair { get; set; }
        public double Communication { get; set; }
        public double Leasing { get; set; }
        public double Insurance { get; set; }
        public double ProIndemnity { get; set; }
        public double ComputerIt { get; set; }
        public double Recruitment { get; set; }
        public double RegistrationFee { get; set; }
        public double Marketing { get; set; }
        public double Travel { get; set; }
        public double Entertainment { get; set; }
        public double Transport { get; set; }
        public double Accountancy { get; set; }
        public double Banking { get; set; }
        public double Interest { get; set; }
        public double OtherExpense { get; set; }
        public double Amortalisation { get; set; }
        public double Depreciation { get; set; }
        public FilesVersion Version { get; set; }
        public int FilesVersionId { get; set; }
    }
}