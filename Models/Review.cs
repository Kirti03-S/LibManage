﻿namespace LibManage.Models;

public class Review
{
    public int Id { get; set; }

    public int BookId { get; set; }
    public Book? Book { get; set; }

    public int MemberId { get; set; }
    public Member? Member { get; set; }

    public int Rating { get; set; }  // 1 to 5
    public string Comment { get; set; } = "";
    public DateTime CreatedDate { get; set; }
}
