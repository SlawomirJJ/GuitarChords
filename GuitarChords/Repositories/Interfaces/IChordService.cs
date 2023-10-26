using GuitarChords.Models.Dtos;
using GuitarChords.Models.Dtos.Requests;
using GuitarChords.Models.Entities;
using GuitarChords.Models.Results;

namespace GuitarChords.Repositories.Interfaces
{
    public interface IChordService
    {
        Task CreateChord(CreateChordRequest request);
        Task<ChordListResponse> GetAllChords(ChordListRequest request);
        Task UpdateChord(UpdateChordRequest chord);
        Task DeleteChord(Guid id);
        Task<ChordListResponse> SearchChord(ChordListRequest request);
    }
}
