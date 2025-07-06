using DAL;
using Entities;

namespace Services
{
    public class ArtistService : IArtistService
    {
        private readonly ArtistDAL _dal;

        public ArtistService()
        {
            _dal = new ArtistDAL();
        }

        public Task SubmitApplication(ArtistApplicationModel model)
        {
            _dal.SubmitApplication(model); // this is synchronous
            return Task.CompletedTask;     // wrap in a completed Task so you can await it
        }


        public async Task<List<int>> GetArtistApplicationsByUserIdAsync(int userId)
        {
            return await _dal.GetApprovedArtistApplicationsByUserIdAsync(userId);
        }

        public async Task UpdateArtistProfileAsync(ArtistDropdownModel artist)
        {
            await _dal.UpdateArtistProfileAsync(artist);
        }
        public List<ArtistApplicationModel> GetPendingApplications()
        {
            return _dal.GetPendingApplications();
        }

        public void ApproveApplication(int appId, int processedBy)
        {
            _dal.ApproveApplication(appId, processedBy);
        }

        public void RejectApplication(int appId, int processedBy, string reason)
        {
            _dal.RejectApplication(appId, processedBy, reason);
        }

        public async Task<List<ArtistDropdownModel>> GetApprovedArtists()
        {
            return await _dal.GetApprovedArtists();
        }
        public async Task RemoveArtistAsync(int applicationId)
        {
            await _dal.RemoveArtistAsync(applicationId);
        }

        public async Task<ArtistDropdownModel?> GetArtistByIdAsync(int id)
        {
            return await _dal.GetArtistByIdAsync(id);
        }

        public async Task UpdateArtistAsync(ArtistDropdownModel artist)
        {
            await _dal.UpdateArtistAsync(artist);
        }
    }
}
