using GuitarChords.Models.Dtos;

namespace GuitarChords.Models.Results
{
    public class ChordListResponse
    {
        public List<ChordDto> FoundChordsDtos { get; set; }
        public int TotalPages { get; set; }
        public int CurrentPage { get; set; }
    }
}
