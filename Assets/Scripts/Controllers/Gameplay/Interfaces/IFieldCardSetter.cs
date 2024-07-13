using System.Collections.Generic;
using CardGuess.Components;

namespace CardGuess.Controllers
{
    public interface IFieldCardSetter
    {
        public void SetCards(Dictionary<int, CardPool> cardPools);
    }
}