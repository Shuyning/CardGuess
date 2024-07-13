using CardGuess.Controllers;
using CardGuess.Models;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace CardGuess.Components
{
    [RequireComponent(typeof(Image))]
    public class CardViewComponent : CardView
    {
        [SerializeField] private CardSuit cardSuit;
        [SerializeField] private CardRank cardRank;

        private IAudioPlayer _audioPlayer;
        private CardViewStorage _cardViewStorage;
        
        private Image _image;

        public override CardSuit CardSuit => cardSuit;
        public override CardRank CardRank => cardRank;

        [Inject]
        private void Construct(CardViewStorage cardViewStorage, IAudioPlayer audioPlayer)
        {
            _cardViewStorage = cardViewStorage;
            _audioPlayer = audioPlayer;
        }
        
        private void Awake()
        {
            _image = GetComponent<Image>();
        }

        public override void SetCardInfo(CardSuit suit, CardRank rank)
        {
            cardSuit = suit;
            cardRank = rank;
        }

        public override void Open(bool isSound)
        {
            _image.sprite = _cardViewStorage.GetSpriteByRankSuit(cardSuit, cardRank);
            
            if (isSound)
                _audioPlayer.PlayClip(SoundType.CardFlip);
        }

        public override void Close(bool isSound)
        {
            _image.sprite = _cardViewStorage.BackCardSprite;
            
            if (isSound)
                _audioPlayer.PlayClip(SoundType.CardFlip);
            
            base.Close(isSound);
        }
    }   
}