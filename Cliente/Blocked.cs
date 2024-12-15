using Cliente.ServiceReference;

namespace Cliente
{
    public class Blocked
    {
        public int BlockId { get; set; }
        public int BlockedId { get; set; }
        public string BlockerUsername { get; set; }
        public string BlockedProfilePicturePath { get; set; }

        public Blocked(int blockId, int blockedId, string blockerUsername, string blockedProfilePicturePath)
        {
            BlockId = blockId;
            BlockedId = blockedId;
            BlockerUsername = blockerUsername;
            BlockedProfilePicturePath = blockedProfilePicturePath;
        }

        public Blocked(BlockedDTO blockedDto)
        {
            BlockId = blockedDto.BlockId;
            BlockedId = blockedDto.BlockedId;
            BlockerUsername = blockedDto.BlockerUsername;
            BlockedProfilePicturePath = "pack://application:,,,/Images/pfp" + blockedDto.ProfilePictureId + ".jpg";
        }
    }
}