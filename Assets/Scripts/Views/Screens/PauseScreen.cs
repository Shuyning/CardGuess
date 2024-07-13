using CardGuess.Controllers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace CardGuess.View
{
    public class PauseScreen : InteractiveScreen
    {
        private const string WinText = "Win!";
        private const string LoseText = "Lose!";
        
        [SerializeField] private TextMeshProUGUI endText;
        [Space] 
        [SerializeField] private Button unpauseButton;
        [SerializeField] private Button restartButton;
        [SerializeField] private Button exitButton;

        private IPauseController _pauseController;
        private IGameStarter _gameStarter;
        private IGameEndObserver _gameEndObserver;

        private bool _isEndGame = false;

        [Inject]
        private void Construct(IPauseController pauseController, IGameStarter gameStarter,
            IGameEndObserver gameEndObserver)
        {
            _pauseController = pauseController;
            _gameEndObserver = gameEndObserver;
            _gameStarter = gameStarter;
        }

        protected override void Awake()
        {
            base.Awake();
            _gameEndObserver.GameWon += GameFinish;
        }

        private void OnDestroy()
        {
            _gameEndObserver.GameWon -= GameFinish;
        }

        public override void Show()
        {
            base.Show();
            exitButton.onClick.AddListener(Exit);
            restartButton.onClick.AddListener(Restart);
            unpauseButton.onClick.AddListener(UnpauseGame);
        }

        public override void Hide(bool isFade = true)
        {
            _isEndGame = false;
            base.Hide(isFade);
            exitButton.onClick.RemoveListener(Exit);
            restartButton.onClick.RemoveListener(Restart);
            unpauseButton.onClick.RemoveListener(UnpauseGame);
            endText.text = string.Empty;
        }

        private void GameFinish(bool isWin)
        {
            _isEndGame = true;
            endText.text = isWin ? WinText : LoseText;
            
            Show();
        }

        private void UnpauseGame()
        {
            if (_isEndGame)
            {
                Restart();
                return;
            }
            
            _pauseController.Unpause();
            Hide();
        }

        private void Restart()
        {
            Hide();
            _pauseController.Unpause();
            _gameStarter.StartGame();
        }

        private void Exit()
        {
            Hide();
            Application.Quit();
        }
    }
}