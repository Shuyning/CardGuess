using System;
using Zenject;

namespace CardGuess.Controllers
{
    public class GameEndController : IInitializable, IDisposable, IGameEndObserver
    {
        private readonly ITimerObserver _timerObserver;
        private readonly ICardFindObserver _cardFindObserver;

        public event Action<bool> GameWon;

        [Inject]
        private GameEndController(ITimerObserver timerObserver, ICardFindObserver cardFindObserver)
        {
            _timerObserver = timerObserver;
            _cardFindObserver = cardFindObserver;
        }
        
        public void Initialize()
        {
            _timerObserver.TimerFinished += LoseGame;
            _cardFindObserver.CardEnded += WinGame;
        }
        
        public void Dispose()
        {
            _timerObserver.TimerFinished -= LoseGame;
            _cardFindObserver.CardEnded -= WinGame;
        }

        private void LoseGame()
        {
            GameWon?.Invoke(false);
        }

        private void WinGame()
        {
            GameWon?.Invoke(true);
        }
    }   
}