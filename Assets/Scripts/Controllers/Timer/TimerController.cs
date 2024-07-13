using System;
using System.Threading;
using CardGuess.Utils;
using CardGuess.Models;
using Cysharp.Threading.Tasks;
using Zenject;

namespace CardGuess.Controllers
{
    public class TimerController : ITimerStarter, ITimerObserver, IDisposable
    {
        private readonly TimerData _timerData;

        private CancellationTokenSource _cancellationTokenSource;

        private int _currentTime;

        public int CurrentTime 
        {
            get => _currentTime;
            private set
            {
                _currentTime = value;
                TimerUpdated?.Invoke(_currentTime);
            }
        }
        
        public event Action<int> TimerUpdated;
        public event Action TimerFinished;

        [Inject]
        private TimerController(TimerData timerData)
        {
            _timerData = timerData;
        }
        
        public void Dispose()
        {
            _cancellationTokenSource?.CancelAndDispose();
        }

        public void StartTimer()
        {
            _cancellationTokenSource?.CancelAndDispose();
            _cancellationTokenSource = new CancellationTokenSource();

            CurrentTime = _timerData.TimerDuration;
            Timer().Forget();
        }

        private async UniTaskVoid Timer()
        {
            try
            {
                while (_currentTime > 0)
                {
                    await UniTask.WaitForSeconds(_timerData.TimeBetween, false, PlayerLoopTiming.Update, _cancellationTokenSource.Token);
                    CurrentTime -= 1;
                }
                
                TimerFinished?.Invoke();
            }
            catch (Exception e) { }
        }
    }   
}