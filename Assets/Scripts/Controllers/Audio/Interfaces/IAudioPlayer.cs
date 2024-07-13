using CardGuess.Models;

namespace CardGuess.Controllers
{
    public interface IAudioPlayer
    {
        public void PlayClip(SoundType type);
    }
}