using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

namespace CardGuess.Models
{
    [CreateAssetMenu(fileName = "CardViewStorage", menuName = "ScriptableObject/CardViewStorage")]
    public class CardViewStorage : ScriptableObject
    {
        [SerializeField] private List<CardViewData> cardViewDataList;
        [SerializeField] private Sprite backCardSprite;

        private Dictionary<(CardSuit, CardRank), CardViewData> _cardViewDataDictionary;

        public Sprite BackCardSprite => backCardSprite;

        private void OnValidate()
        {
            CreateCardDictionary();
        }

        public Sprite GetSpriteByRankSuit(CardSuit suit, CardRank rank)
        {
            if (_cardViewDataDictionary == null)
                CreateCardDictionary();

            if (_cardViewDataDictionary.TryGetValue((suit, rank), out CardViewData cardViewData))
                return cardViewData.CardSprite;

            return null;
        }
        
        public IReadOnlyList<CardViewData> GetUniqueRandomElements(int count)
        {
            if (_cardViewDataDictionary == null)
                CreateCardDictionary();
            
            if (count > _cardViewDataDictionary.Count)
                return null;
            
            List<CardViewData> uniqueRandomElements = _cardViewDataDictionary.Values.OrderBy(x => Random.value)
                .Take(count)
                .ToList();

            return uniqueRandomElements;
        }

        private void CreateCardDictionary()
        {
            _cardViewDataDictionary = new Dictionary<(CardSuit, CardRank), CardViewData>();
            
            foreach (var cardData in cardViewDataList)
            {
                if (_cardViewDataDictionary.ContainsKey((cardData.CardSuit, cardData.CardRank)))
                    continue;
                
                _cardViewDataDictionary.Add((cardData.CardSuit, cardData.CardRank), cardData);
            }
        }
    }
}