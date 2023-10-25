namespace GuitarChords.Models.Dtos.Requests
{
    public class CreateChordRequest
    {
        public string ChordName { get; set; } = string.Empty;
        public int? Bar { get; set; }
        public int? FirstString { get; set; }
        public int? SecondString { get; set; }
        public int? ThirdString { get; set; }
        public int? FourthString { get; set; }
        public int? FifthString { get; set; }
        public int? SixthString { get; set; }
    }
}
