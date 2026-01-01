using WebApi.Entities;

namespace WebApi.Services;

public class InMemoryMatchService : IMatchService
{
    private List<Profile> _profiles;
    private int _nextProfileId = 1;

    public InMemoryMatchService()
    {
        _profiles = new List<Profile>();
        SeedDummyData();
    }

    private void SeedDummyData()
    {
        // Create dummy profiles
        var profile1 = new Profile
        {
            Id = _nextProfileId++,
            Name = "Alice",
            Age = 28,
            Gender = "Female",
            Likes = new List<Like>()
        };

        var profile2 = new Profile
        {
            Id = _nextProfileId++,
            Name = "Bob",
            Age = 30,
            Gender = "Male",
            Likes = new List<Like>()
        };

        var profile3 = new Profile
        {
            Id = _nextProfileId++,
            Name = "Charlie",
            Age = 25,
            Gender = "Male",
            Likes = new List<Like>()
        };

        var profile4 = new Profile
        {
            Id = _nextProfileId++,
            Name = "Diana",
            Age = 27,
            Gender = "Female",
            Likes = new List<Like>()
        };

        // Add profiles to the list
        _profiles.Add(profile1);
        _profiles.Add(profile2);
        _profiles.Add(profile3);
        _profiles.Add(profile4);

        // Seed some likes
        // Alice likes Bob and Charlie
        profile1.Likes.Add(new Like { ProfileId = profile2.Id, Profile = profile2 });
        profile1.Likes.Add(new Like { ProfileId = profile3.Id, Profile = profile3 });

        // Bob likes Alice (mutual match with Alice)
        profile2.Likes.Add(new Like { ProfileId = profile1.Id, Profile = profile1 });

        // Charlie likes Alice and Diana
        profile3.Likes.Add(new Like { ProfileId = profile1.Id, Profile = profile1 });
        profile3.Likes.Add(new Like { ProfileId = profile4.Id, Profile = profile4 });

        // Diana likes Charlie (mutual match with Charlie)
        profile4.Likes.Add(new Like { ProfileId = profile3.Id, Profile = profile3 });
    }

    public Profile CreateProfile(Profile profile)
    {
        // Validate all properties
        if (string.IsNullOrWhiteSpace(profile.Name))
            throw new ArgumentException("Profile name is required.");
        if (profile.Age <= 0)
            throw new ArgumentException("Profile age must be greater than 0.");
        if (string.IsNullOrWhiteSpace(profile.Gender))
            throw new ArgumentException("Profile gender is required.");

        // Set the ID
        profile.Id = _nextProfileId++;
        profile.Likes ??= new List<Like>();

        // Add to the list
        _profiles.Add(profile);

        return profile;
    }

    public void AddLike(int fromProfileId, int toProfileId)
    {
        // Find the profile that is adding the like
        var fromProfile = _profiles.FirstOrDefault(p => p.Id == fromProfileId);
        if (fromProfile == null)
            throw new ArgumentException($"Profile with ID {fromProfileId} not found.");

        // Verify that the profile being liked exists
        var toProfile = _profiles.FirstOrDefault(p => p.Id == toProfileId);
        if (toProfile == null)
            throw new ArgumentException($"Profile with ID {toProfileId} not found.");

        // Check if the like already exists
        if (fromProfile.Likes.Any(l => l.ProfileId == toProfileId))
            throw new InvalidOperationException($"Profile {fromProfileId} has already liked profile {toProfileId}.");

        // Add the like
        fromProfile.Likes.Add(new Like { ProfileId = toProfileId, Profile = toProfile });
    }

    public List<Profile> GetAllProfiles(string? gender = null, int? minAge = null, int? maxAge = null)
    {
        var query = _profiles.AsEnumerable();

        if (!string.IsNullOrWhiteSpace(gender))
            query = query.Where(p => p.Gender.Equals(gender, StringComparison.OrdinalIgnoreCase));

        if (minAge.HasValue)
            query = query.Where(p => p.Age >= minAge.Value);

        if (maxAge.HasValue)
            query = query.Where(p => p.Age <= maxAge.Value);

        return query.ToList();
    }

    public List<Profile> GetMatches(int profileId)
    {
        // Find the profile
        var profile = _profiles.FirstOrDefault(p => p.Id == profileId);
        if (profile == null)
            throw new ArgumentException($"Profile with ID {profileId} not found.");

        // Find mutual likes
        var matches = new List<Profile>();
        if (profile.Likes != null)
        {
            foreach (var like in profile.Likes)
            {
                var likedProfile = _profiles.FirstOrDefault(p => p.Id == like.ProfileId);
                if (likedProfile != null && likedProfile.Likes != null && likedProfile.Likes.Any(l => l.ProfileId == profileId))
                {
                    matches.Add(likedProfile);
                }
            }
        }

        return matches;
    }
}