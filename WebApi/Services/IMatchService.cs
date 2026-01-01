using WebApi.Entities;

namespace WebApi.Services;

public interface IMatchService
{
    Profile CreateProfile(Profile profile);
    void AddLike(int fromProfileId, int toProfileId);
    List<Profile> GetAllProfiles(string? gender = null, int? minAge = null, int? maxAge = null);
    List<Profile> GetMatches(int profileId);
}