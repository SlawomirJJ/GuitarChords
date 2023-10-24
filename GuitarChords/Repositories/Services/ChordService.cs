using AutoMapper;
using Azure.Core;
using GuitarChords.Dtos;
using GuitarChords.Dtos.Requests;
using GuitarChords.Models;
using GuitarChords.Repositories.Interfaces;
using GuitarChords.Results;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;

namespace GuitarChords.Repositories.Services
{
    public class ChordService : IChordService
    {
        private readonly DataContext _dbContext;
        private readonly IMapper _mapper;

        public ChordService(DataContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task CreateChord(CreateChordRequest request)
        {
            var newChord = new Chord
            {
                ChordName = request.ChordName,
                FirstString = request.FirstString,
                SecondString = request.SecondString,
                ThirdString = request.ThirdString,
                FourthString = request.FourthString,
                FifthString = request.FifthString,
                SixthString = request.SixthString,
            };

            await _dbContext.Chords.AddAsync(newChord);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<ChordDto>> GetAllChords()
        {
            var foundChords = await _dbContext.Chords.ToListAsync();
            List<ChordDto>? foundChordsDTOs = null;
            if (foundChords != null)
            {
                foundChordsDTOs = _mapper.Map<List<ChordDto>>(foundChords);
            }



            return foundChordsDTOs;
        }

        public async Task UpdateChord(Chord chord)
        {
            var foundChord = await _dbContext.Chords.FirstOrDefaultAsync(x => x.Id == chord.Id);
            if (foundChord is null)
            {
                throw new Exception("Chord doesn't exists");
            }

            foundChord.ChordName = chord.ChordName;
            foundChord.FirstString = chord.FirstString;
            foundChord.SecondString = chord.SecondString;
            foundChord.ThirdString = chord.ThirdString;
            foundChord.FourthString = chord.FourthString;
            foundChord.FifthString = chord.FifthString;
            foundChord.SixthString = chord.SixthString;

            _dbContext.Chords.Update(foundChord);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteChord(Guid id)
        {
            var foundChord = await _dbContext.Chords.FirstOrDefaultAsync(x => x.Id == id);
            if (foundChord == null)
            {
                throw new Exception("Chord not found");
            }
            _dbContext.Chords.Remove(foundChord);
            await _dbContext.SaveChangesAsync();

        }

        public async Task<List<ChordDto>>? SearchChord(string chordName)
        {
            var foundChords = _dbContext.Chords.Where(x => x.ChordName.Contains(chordName)).ToList();
            List<ChordDto>? foundChordsDTOs = null;
            if (foundChords != null)
            {
                foundChordsDTOs = _mapper.Map<List<ChordDto>>(foundChords);
            }



            return foundChordsDTOs;
        }

    }
}
