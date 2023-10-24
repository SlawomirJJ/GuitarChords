using GuitarChords.Dtos;
using GuitarChords.Dtos.Requests;
using GuitarChords.Models;
using GuitarChords.Results;

namespace GuitarChords.Interfaces
{
    public interface IChordService
    {
        Task CreateChord(CreateChordRequest request);
        Task<List<ChordDto>> GetAllChords();
        Task UpdateChord(Chord chord);
        Task DeleteChord(Guid id);
        Task<List<ChordDto>> SearchChord(string chordName);
    }
}
