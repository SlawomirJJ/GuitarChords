namespace GuitarChords.Models.Dtos.Requests
{
    public class ChordListRequest
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public string? SearchName { get; set; }
    }
}
