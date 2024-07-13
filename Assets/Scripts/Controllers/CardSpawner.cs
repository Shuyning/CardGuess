using System;
using System.Collections.Generic;
using CardGuess.Components;
using UnityEngine;
using Zenject;

namespace CardGuess.Controllers
{
    public class CardSpawner : IDisposable, ICardSpawner
    {
        private readonly CardPool.Pool _cardPool;

        private readonly List<CardPool> _cardObjects;

        [Inject]
        private CardSpawner(CardPool.Pool cardPool)
        {
            _cardPool = cardPool;

            _cardObjects = new List<CardPool>();
        }
        
        public void Dispose()
        {
            _cardObjects.Clear();
            _cardPool.Clear();
        }
        
        public CardPool Spawn(Transform parent)
        {
            CardPool cardPool = _cardPool.Spawn(parent);
            _cardObjects.Add(cardPool);
            return cardPool;
        }

        public void Despawn(CardPool cardObject)
        {
            if (_cardObjects.Contains(cardObject))
                _cardObjects.Remove(cardObject);
            
            _cardPool.Despawn(cardObject);
        }

        public void DespawnAll()
        {
            foreach (var cardObject in _cardObjects)
                _cardPool.Despawn(cardObject);
            
            _cardObjects.Clear();
        }
    }
}