using System;

namespace CardGuess.Controllers
{
    public interface IGameEndObserver
    {
        public event Action<bool> GameWon;
    }
}