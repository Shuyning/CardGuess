using CardGuess.Components;
using UnityEngine;

namespace CardGuess.Controllers
{
    public interface ICardSpawner
    {
        public CardPool Spawn(Transform parent);

        public void Despawn(CardPool cardObject);
        public void DespawnAll();
    }
}