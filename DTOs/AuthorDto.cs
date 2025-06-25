namespace LibManage.DTOs
{
    public class AuthorDto
    {
        public int Id { get; set; }
        public string FullName => $"{FirstName} {LastName}";
        public string FirstName { get; set; } = "";
        public string LastName { get; set; } = "";
        public string Biography { get; set; } = "";
    }

    public class CreateAuthorDto
    {
        public string FirstName { get; set; } = "";
        public string LastName { get; set; } = "";
        public string Biography { get; set; } = "";
    }
}
