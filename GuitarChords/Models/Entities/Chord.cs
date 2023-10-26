using System.ComponentModel.DataAnnotations;

namespace GuitarChords.Models.Entities
{
    public class Chord
    {
        public Guid Id { get; set; }
        public string ChordName { get; set; }
        public int? FirstString { get; set; }
        public int? SecondString { get; set; }
        public int? ThirdString { get; set; }
        public int? FourthString { get; set; }
        public int? FifthString { get; set; }
        public int? SixthString { get; set; }
    }
}
