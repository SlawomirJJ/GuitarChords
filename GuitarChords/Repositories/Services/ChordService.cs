using AutoMapper;
using Azure.Core;
using GuitarChords.Models.Dtos;
using GuitarChords.Models.Dtos.Requests;
using GuitarChords.Models.Entities;
using GuitarChords.Models.Results;
using GuitarChords.Repositories.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using NuGet.Protocol.Plugins;
using System.Xml.Schema;

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

        public async Task<ChordListResponse> GetAllChords(ChordListRequest request)
        {
            int totalItemsCount = await _dbContext.Chords.CountAsync();
            int numberOfPages = (int)Math.Ceiling((float)totalItemsCount/(float)request.PageSize);

            var foundChords = await _dbContext.Chords
                .Skip(request.PageSize*(request.PageNumber-1))
                .Take(request.PageSize).OrderBy(x => x.ChordName)
                .ToListAsync();

            List<ChordDto>? foundChordsDTOs = null;
            if (foundChords != null)
            {
                foundChordsDTOs = _mapper.Map<List<ChordDto>>(foundChords);
            }

            var chordListResponse = new ChordListResponse()
            {
                FoundChordsDtos = foundChordsDTOs,
                TotalPages = numberOfPages,
                CurrentPage = request.PageNumber
            };

            return chordListResponse;
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

        public async Task<ChordListResponse> SearchChord(ChordListRequest request)
        {
            int? totalItemsCount = await _dbContext.Chords
                .Where(x => x.ChordName.ToUpper() == request.SearchName.ToUpper())
                .CountAsync();

            List<Chord> foundChords = null;
            int numberOfPages;
            if (totalItemsCount != 0)
            {               
                numberOfPages = (int)Math.Ceiling((float)totalItemsCount / (float)request.PageSize);

                foundChords = await _dbContext.Chords
                .Where(x => x.ChordName.ToUpper() == request.SearchName.ToUpper())
                .Skip(request.PageSize * (request.PageNumber - 1))
                .Take(request.PageSize).OrderBy(x => x.ChordName)
                .ToListAsync();
            }
            else
            {
                totalItemsCount = await _dbContext.Chords.Where(x => x.ChordName.Contains(request.SearchName)).CountAsync();
                numberOfPages = (int)Math.Ceiling((float)totalItemsCount / (float)request.PageSize);

                foundChords = await _dbContext.Chords
                .Where(x => x.ChordName.Contains(request.SearchName))
                .Skip(request.PageSize * (request.PageNumber - 1))
                .Take(request.PageSize).OrderBy(x => x.ChordName)
                .ToListAsync();
            }



            

            List<ChordDto>? foundChordsDTOs = null;
            if (foundChords != null)
            {
                foundChordsDTOs = _mapper.Map<List<ChordDto>>(foundChords);
            }

            var chordListResponse = new ChordListResponse()
            {
                FoundChordsDtos = foundChordsDTOs,
                TotalPages = numberOfPages,
                CurrentPage = request.PageNumber
            };

            return chordListResponse;
        }

    }
}
