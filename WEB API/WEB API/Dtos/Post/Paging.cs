namespace WEB_API.Dtos.Post
{
    public class Paging
    {
       public int? cursor {  get; set; }
       public int? nextCursor { get; set; }
       public int? total { get; set; }
       public int? page { get; set; }
       public int? limit { get; set; }
       public bool? hasNext { get; set; }
    }
}
