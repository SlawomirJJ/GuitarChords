namespace GuitarChords.Models
{
    public class Chord
    {
        public Guid Id { get; set; }
        public string ChordName { get; set; } = string.Empty;
        public int? Bar { get; set; }
        public char FirstString { get; set; }
        public char SecondString { get; set; }
        public char ThirdString { get; set; }
        public char FourthString { get; set; }
        public char FifthString { get; set; }
        public char SixthString { get; set; }
    }
}
