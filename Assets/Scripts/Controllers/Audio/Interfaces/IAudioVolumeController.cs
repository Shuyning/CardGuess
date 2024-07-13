namespace CardGuess.Controllers
{
    public interface IAudioVolumeController
    {
        public float CurrentVolume { get; }
        
        public void SetVolume(float value);
    }
}