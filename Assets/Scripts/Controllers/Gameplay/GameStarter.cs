using UnityEngine;
using Zenject;

namespace CardGuess.Controllers
{
    public class GameStarter : MonoBehaviour, IGameStarter
    {
        private GameCardSpawnController _gameCardSpawnController;
        private ITimerStarter _timerStarter;

        [Inject]
        private void Construct(GameCardSpawnController gameCardSpawnController, ITimerStarter timerStarter)
        {
            _gameCardSpawnController = gameCardSpawnController;
            _timerStarter = timerStarter;
        }

        private void Start()
        {
            StartGame();
        }

        public void StartGame()
        {
            _gameCardSpawnController.SpawnCards();
            _timerStarter.StartTimer();
        }
    }   
}