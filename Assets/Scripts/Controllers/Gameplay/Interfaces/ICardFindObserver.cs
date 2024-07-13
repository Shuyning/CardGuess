using System;

namespace CardGuess.Controllers
{
    public interface ICardFindObserver
    {
        public event Action CardEnded;
    }
}