using System.Collections.Generic;
using System.Linq;
using CardGuess.Components;
using CardGuess.Models;
using CardGuess.Utils;
using Zenject;

namespace CardGuess.Controllers
{
    public class CardShuffler : ICardShuffler
    {
        private readonly IGameCardGetter _gameCardGetter;

        [Inject]
        private CardShuffler(IGameCardGetter gameCardGetter)
        {
            _gameCardGetter = gameCardGetter;
        }
        
        public void Shuffle()
        {
            List<CardViewData> values = RandomListShuffler.ShuffleList(CopyCardList(_gameCardGetter.ActiveActiveCards.Values.ToList()));
            List<int> keys = new List<int>(_gameCardGetter.ActiveActiveCards.Keys);

            for (int i = 0; i < _gameCardGetter.ActiveActiveCards.Count; i++)
                _gameCardGetter.ActiveActiveCards[keys[i]].CardView.SetCardInfo(values[i].CardSuit, values[i].CardRank);
        }

        private List<CardViewData> CopyCardList(List<CardPool> cardList)
        {
            List<CardViewData> newList = new List<CardViewData>();

            foreach (var card in cardList)
                newList.Add(new CardViewData(card.CardView.CardSuit, card.CardView.CardRank));

            return newList;
        }
    }
}