using System;
using UnityEngine;

namespace CardGuess.Models
{
    [Serializable]
    public class CardViewData
    {
        [SerializeField] private CardSuit cardSuit;
        [SerializeField] private CardRank cardRank;
        [Space] 
        [SerializeField] private Sprite cardSprite;

        public CardSuit CardSuit => cardSuit;
        public CardRank CardRank => cardRank;

        public Sprite CardSprite => cardSprite;

        public CardViewData(CardSuit suit, CardRank rank)
        {
            cardSuit = suit;
            cardRank = rank;
        }
    }
}