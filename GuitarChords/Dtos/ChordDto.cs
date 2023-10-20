namespace GuitarChords.Dtos
{
    public class ChordDto
    {
        public Guid Id { get; set; }
        public string ChordName { get; set; } = string.Empty;
        public int LowestFret { get; set; }
        public int HighestFret { get; set; }
        public int? Bar { get; set; }
        public int? FirstString { get; set; }
        public int? SecondString { get; set; }
        public int? ThirdString { get; set; }
        public int? FourthString { get; set; }
        public int? FifthString { get; set; }
        public int? SixthString { get; set; }
    }
}
