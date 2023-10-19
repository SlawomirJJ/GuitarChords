using GuitarChords.Requests;
using GuitarChords.Results;

namespace GuitarChords.Interfaces
{
    public interface IChordService
    {
        Task CreateChord(CreateChordRequest request);
        Task<List<FoundChordResult>> GetAllChords();
        Task UpdateChord(UpdateChordRequest request);
        Task DeleteUser(Guid id);
    }
}
