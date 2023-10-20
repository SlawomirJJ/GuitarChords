using GuitarChords.Dtos;
using GuitarChords.Models;
using GuitarChords.Requests;
using GuitarChords.Results;

namespace GuitarChords.Interfaces
{
    public interface IChordService
    {
        Task CreateChord(CreateChordRequest request);
        Task<List<FoundChordResult>> GetAllChords();
        Task UpdateChord(Chord chord);
        Task DeleteChord(Guid id);
        Task<List<ChordDto>> SearchChord(string chordName);
    }
}
