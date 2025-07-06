using Entities;

namespace Services
{
    public interface IArtistService
    {
        public Task SubmitApplication(ArtistApplicationModel model);
        List<ArtistApplicationModel> GetPendingApplications();
        void ApproveApplication(int appId, int processedBy);
        void RejectApplication(int appId, int processedBy, string reason);

        Task UpdateArtistProfileAsync(ArtistDropdownModel artist);
        Task<List<ArtistDropdownModel>> GetApprovedArtists();

        Task<ArtistDropdownModel?> GetArtistByIdAsync(int id);

        Task<List<int>> GetArtistApplicationsByUserIdAsync(int userId);
        Task RemoveArtistAsync(int applicationId);


        Task UpdateArtistAsync(ArtistDropdownModel artist);
    }
}
