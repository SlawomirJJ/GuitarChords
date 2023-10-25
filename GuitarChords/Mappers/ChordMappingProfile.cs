using AutoMapper;
using GuitarChords.Models.Dtos;
using GuitarChords.Models.Entities;

namespace GuitarChords.Mappers
{
    public class ChordMappingProfile : Profile
    {
        public ChordMappingProfile()
        {
            CreateMap<Chord, ChordDto>()
                .ForMember(m => m.LowestFret, a => a.MapFrom(c => FindLowestFret(new int?[] { c.FirstString, c.SecondString, c.ThirdString, c.FourthString, c.FifthString, c.SixthString })))
                .ForMember(m => m.HighestFret, a => a.MapFrom(c => FindHighestFret(new int?[] { c.FirstString, c.SecondString, c.ThirdString, c.FourthString, c.FifthString, c.SixthString })));
                
        }

        private int FindLowestFret(int?[] strings)
        {
            int lowestFret = 24;
            foreach (var number in strings)
            {
                if (number.HasValue)
                {
                    if (number < lowestFret && number != 0)
                    {
                        lowestFret = (int)number;
                    }
                }
                
            }

            if (lowestFret - 1 > 0)
            {
                lowestFret--;
            }
            return lowestFret;
        }

        private int FindHighestFret(int?[] strings)
        {
            int highestFret = 0;
            foreach (var number in strings)
            {
                if (number.HasValue)
                {
                    if (number > highestFret)
                    {
                        highestFret = (int)number;
                    }
                }
                
            }

            highestFret++;
            return highestFret;
        }
    }
}
