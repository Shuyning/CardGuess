using System;
using CardGuess.Models;
using UnityEngine;

namespace CardGuess.Components
{
    public abstract class CardView : MonoBehaviour
    {
        public abstract CardSuit CardSuit { get; }
        public abstract CardRank CardRank { get; }

        public event Action Closed; 

        public abstract void SetCardInfo(CardSuit suit, CardRank rank);
        
        public abstract void Open(bool isSound);

        public virtual void Close(bool isSound)
        {
            Closed?.Invoke();
        }
    }
}