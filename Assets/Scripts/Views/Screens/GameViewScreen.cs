using System;
using CardGuess.Controllers;
using CardGuess.Models;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace CardGuess.View
{
    public class GameViewScreen : InteractiveScreen
    {
        [SerializeField] private TextMeshProUGUI timerText;
        [Space] 
        [SerializeField] private Button pauseButton;
        [SerializeField] private Button shuffleButton;
        [Space] 
        [SerializeField] private InteractiveScreen pauseScreen;

        private IAudioPlayer _audioPlayer;
        private ICardShuffler _cardShuffler;
        private IPauseController _pauseController;
        private ITimerObserver _timerObserver;
        
        [Inject]
        private void Construct(IAudioPlayer audioPlayer, ICardShuffler cardShuffler,
            IPauseController pauseController, ITimerObserver timerObserver)
        {
            _audioPlayer = audioPlayer;
            _cardShuffler = cardShuffler;
            _pauseController = pauseController;
            _timerObserver = timerObserver;
        }
        
        private void Start()
        {
            UpdateTimerText(_timerObserver.CurrentTime);
            Show();
        }

        private void OnDestroy()
        {
            Hide();
        }

        public override void Show()
        {
            base.Show();
            
            shuffleButton.onClick.AddListener(Shuffle);
            pauseButton.onClick.AddListener(PauseGame);
            _timerObserver.TimerUpdated += UpdateTimerText;
        }

        public override void Hide(bool isFade = true)
        {
            base.Hide(isFade);
            shuffleButton.onClick.RemoveListener(Shuffle);
            pauseButton.onClick.RemoveListener(PauseGame);
            _timerObserver.TimerUpdated -= UpdateTimerText;
        }

        private void Shuffle()
        {
            _cardShuffler.Shuffle();
            _audioPlayer.PlayClip(SoundType.MixCard);
        }

        private void UpdateTimerText(int second)
        {
            int minutes = second / 60;
            int seconds = second % 60;
            
            timerText.text = $"{minutes:D2}:{seconds:D2}";
        }

        private void PauseGame()
        {
            _pauseController.Pause();
            pauseScreen.Show();
        }
    }
}