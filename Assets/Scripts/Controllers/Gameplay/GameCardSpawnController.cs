using System;
using System.Collections.Generic;
using System.Threading;
using CardGuess.Components;
using CardGuess.Models;
using CardGuess.Utils;
using Cysharp.Threading.Tasks;
using Zenject;

namespace CardGuess.Controllers
{
    public class GameCardSpawnController : IDisposable
    {
        private const int DefaultUniqueElementsValue = 10;
        private const int DefaultCardAmount = 20;

        private readonly IAudioPlayer _audioPlayer;
        private readonly ICardPositionStorageGetter _cardPositionGetter;
        private readonly ICardSpawner _cardSpawner;
        private readonly IFieldCardSetter _fieldCardSetter;

        private readonly CardViewStorage _cardViewStorage;
        private readonly FieldCardConfig _fieldCardConfig;
        private readonly SignalBus _signalBus;

        private readonly Dictionary<int, CardPool> _cards;

        private CancellationTokenSource _cancellationTokenSource;
        
        [Inject]
        private GameCardSpawnController(ICardPositionStorageGetter cardPositionGetter, ICardSpawner cardSpawner,
            CardViewStorage cardViewStorage, IAudioPlayer audioPlayer, IFieldCardSetter fieldCardSetter,
            FieldCardConfig fieldCardConfig, SignalBus signalBus)
        {
            _cardPositionGetter = cardPositionGetter;
            _cardSpawner = cardSpawner;
            _audioPlayer = audioPlayer;
            _fieldCardSetter = fieldCardSetter;
            _fieldCardConfig = fieldCardConfig;
            _signalBus = signalBus;

            _cardViewStorage = cardViewStorage;
            _cards = new Dictionary<int, CardPool>(DefaultCardAmount);
        }
        
        public void Dispose()
        {
            _cancellationTokenSource?.CancelAndDispose();
        }

        public void SpawnCards()
        {
            _signalBus.Fire(new DealCardSignal() { IsDealProgress = true });
            DespawnCards();
            
            IReadOnlyList<CardViewData> cardViewDataList = _cardViewStorage.GetUniqueRandomElements(DefaultUniqueElementsValue);
            List<int> idList = GetRandomPositionList();
            int currentId = 0;
            
            foreach (var cardViewData in cardViewDataList)
                for (int i = 0; i < 2; i++)
                {
                    SpawnCard(currentId, idList[currentId], cardViewData);
                    currentId++;
                }
            
            _audioPlayer.PlayClip(SoundType.MixCard);
            WaitSetCard().Forget();
        }

        private async UniTaskVoid WaitSetCard()
        {
            _cancellationTokenSource?.CancelAndDispose();
            _cancellationTokenSource = new CancellationTokenSource();
            
            try
            {
                await UniTask.WaitForSeconds(_fieldCardConfig.FirstShowTime, false, PlayerLoopTiming.Update,
                    _cancellationTokenSource.Token);
            }
            catch (Exception e) { }
            
            foreach (var card in _cards)
                card.Value.CardView.Close(false);
            
            _signalBus.Fire(new DealCardSignal() { IsDealProgress = false });
            _fieldCardSetter.SetCards(_cards);
        }

        private List<int> GetRandomPositionList()
        {
            List<int> idList = new List<int>(DefaultCardAmount);

            for (int i = 0; i < DefaultCardAmount; i++)
                idList.Add(i);

            return RandomListShuffler.ShuffleList(idList);
        }

        private void SpawnCard(int id, int positionId, CardViewData cardViewData)
        {
            CardPool cardPool = _cardSpawner.Spawn(_cardPositionGetter.CardPositions[positionId].Transform);
            cardPool.CardView.SetCardInfo(cardViewData.CardSuit, cardViewData.CardRank);
            cardPool.CardView.Open(false);
            cardPool.SetId(id);
                    
            _cards.Add(id, cardPool);
        }

        private void DespawnCards()
        {
            _cardSpawner.DespawnAll();
            _cards.Clear();
        }
    }   
}