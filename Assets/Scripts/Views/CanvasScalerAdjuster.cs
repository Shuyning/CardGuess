using UnityEngine;
using UnityEngine.UI;

namespace CardGuess.View
{
    [RequireComponent(typeof(CanvasScaler))]
    public class CanvasScalerAdjuster : MonoBehaviour
    {
        [SerializeField] private float screenAspectRation = 1.6f;
        
        private CanvasScaler _canvasScaler;

        private void Awake()
        {
            _canvasScaler = GetComponent<CanvasScaler>();
            AdjustCanvas();
        }

        private void AdjustCanvas()
        {
            _canvasScaler.matchWidthOrHeight = CheckTablet() ? 1f : 0f;
        }

        private bool CheckTablet()
        {
            float aspectRatio = (float)UnityEngine.Screen.width / (float)UnityEngine.Screen.height;
            return aspectRatio < screenAspectRation;
        }
    }   
}