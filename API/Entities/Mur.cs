namespace API.Entities
{
    public class Mur
    {
        public int Id { get; set; }
        public int MurYear { get; set; }
        public int TotalMur { get; set; }
        public Docs Document { get; set; }
      	public int DocsId { get; set; }
    }
}