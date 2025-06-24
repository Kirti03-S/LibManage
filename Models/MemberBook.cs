namespace LibManage.Models
{
    public class MemberBook
    {
        public int MemberId { get; set; }
        public Member Member { get; set; }

        public int BookId { get; set; }
        public Book Book { get; set; }
    }
}
