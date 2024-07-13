using System.Collections.Generic;
using CardGuess.Components;

namespace CardGuess.Controllers
{
    public class CardGameField : IFieldCardSetter, IGameCardGetter, IGameCardRemover
    {
        private Dictionary<int, CardPool> _activeCard = new();

        public IReadOnlyDictionary<int, CardPool> ActiveActiveCards => _activeCard;
        
        public void SetCards(Dictionary<int, CardPool> cardPools)
        {
            _activeCard.Clear();
            _activeCard = new Dictionary<int, CardPool>(cardPools);
        }

        public void RemoveCard(int id)
        {
            _activeCard.Remove(id);
        }
    }
}