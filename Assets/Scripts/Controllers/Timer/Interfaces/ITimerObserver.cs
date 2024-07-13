using System;

namespace CardGuess.Controllers
{
    public interface ITimerObserver
    {
        public int CurrentTime { get; }
        
        public event Action<int> TimerUpdated;
        public event Action TimerFinished;
    }
}