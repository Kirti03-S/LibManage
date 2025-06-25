namespace LibManage.DTOs
{
    public class GenreDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
    }

    public class CreateGenreDto
    {
        public string Name { get; set; } = "";
    }
}
