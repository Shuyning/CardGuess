using UnityEngine;

namespace CardGuess.Controllers
{
    public class PauseController : IPauseController
    {
        private bool _isPause = false;

        public void Pause()
        {
            SetPauseState(true);
        }

        public void Unpause()
        {
            SetPauseState(false);
        }

        private void SetPauseState(bool isPause)
        {
            if (_isPause == isPause)
                return;
            
            _isPause = isPause;
            Time.timeScale = _isPause ? 0f : 1f;
        }
    }   
}