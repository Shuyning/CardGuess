using System;
using System.Threading;
using CardGuess.Components;
using CardGuess.Models;
using CardGuess.Utils;
using Cysharp.Threading.Tasks;
using Zenject;

namespace CardGuess.Controllers
{
    public class CardFieldChooseController : ICardFindObserver, IDisposable, IInitializable
    {
        private readonly ICardSpawner _cardSpawner;
        private readonly IGameCardGetter _gameCardGetter;
        private readonly IGameCardRemover _gameCardRemover;
        
        private readonly FieldCardConfig _fieldCardConfig;
        private readonly SignalBus _signalBus;

        private CardPool _selectCard;
        private CardPool _secondSelectCard;

        private CancellationTokenSource _cancellationTokenSource;
           
        public event Action CardEnded;

        [Inject]
        private CardFieldChooseController(ICardSpawner cardSpawner, FieldCardConfig fieldCardConfig,
            SignalBus signalBus, IGameCardGetter gameCardGetter, IGameCardRemover gameCardRemover)
        {
            _cardSpawner = cardSpawner;
            _fieldCardConfig = fieldCardConfig;
            _signalBus = signalBus;
            _gameCardGetter = gameCardGetter;
            _gameCardRemover = gameCardRemover;
        }
        
        public void Initialize()
        {
            _signalBus.Subscribe<CardChooseSignal>(ChooseCard);
        }
        
        public void Dispose()
        {
            _signalBus.Unsubscribe<CardChooseSignal>(ChooseCard);
            _cancellationTokenSource?.CancelAndDispose();
        }
        
        private void ChooseCard(CardChooseSignal cardChooseSignal)
        {
            if (_secondSelectCard != null 
                || _gameCardGetter.ActiveActiveCards.Count == 0)
                return;
            
            if (_selectCard == null)
            {
                SelectFirstCard(cardChooseSignal.CardId);
                return;
            }
            
            if (_selectCard.CardId == cardChooseSignal.CardId)
                return;
            
            if (!_gameCardGetter.ActiveActiveCards.TryGetValue(cardChooseSignal.CardId, out _secondSelectCard))
            {
                _selectCard.CardView.Close(true);
                _selectCard = null;
                return;
            }
            
            _secondSelectCard.CardView.Open(true);
            MatchCard().Forget();
        }

        private void SelectFirstCard(int id)
        {
            if (_gameCardGetter.ActiveActiveCards.TryGetValue(id, out _selectCard))    
                _selectCard.CardView.Open(true);
        }

        private async UniTaskVoid MatchCard()
        {
            _cancellationTokenSource?.CancelAndDispose();
            _cancellationTokenSource = new CancellationTokenSource();
            
            try
            {
                await UniTask.WaitForSeconds(_fieldCardConfig.OpenShowTime, false, PlayerLoopTiming.Update, _cancellationTokenSource.Token);
            }
            catch (Exception e) { }
            
            _selectCard.CardView.Close(true);
            _secondSelectCard.CardView.Close(true);
            
            if (_selectCard.CardView.CardRank == _secondSelectCard.CardView.CardRank 
                && _selectCard.CardView.CardSuit == _secondSelectCard.CardView.CardSuit)
                DeleteCard();
            
            _selectCard = null;
            _secondSelectCard = null;

            CheckLastCard();
        }

        private void CheckLastCard()
        {
            if (_gameCardGetter.ActiveActiveCards.Count == 0)
                CardEnded?.Invoke();
        }

        private void DeleteCard()
        {
            _gameCardRemover.RemoveCard(_selectCard.CardId);
            _gameCardRemover.RemoveCard(_secondSelectCard.CardId);
            
            _cardSpawner.Despawn(_selectCard);
            _cardSpawner.Despawn(_secondSelectCard);
        }
    }
}