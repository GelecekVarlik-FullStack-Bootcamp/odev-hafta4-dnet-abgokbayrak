namespace BMS.Entity.Dto
{
    public class LoginUserDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public int AuthorityId { get; set; }
        public int DepartmentId { get; set; }
    }

}
