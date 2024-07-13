using System;
using UnityEngine;
using DG.Tweening;

namespace CardGuess.View
{
    [RequireComponent(typeof(CanvasGroup))]
    public abstract class Screen : MonoBehaviour
    {
        [SerializeField] protected float fadeTime = 0.75f;
        
        protected CanvasGroup _canvasGroup;

        protected virtual void Awake()
        {
            InitComponents();
        }

        private void OnDestroy()
        {
            _canvasGroup.DOKill();
        }

        protected virtual void ChangeCanvasGroupState(float alpha, float time, bool isActive, Action callback = null)
        {
            if (_canvasGroup.alpha == alpha 
                || gameObject == null)
                return;

            _canvasGroup.DOKill();
            _canvasGroup.DOFade(alpha, time).SetEase(Ease.OutSine).SetUpdate(true).OnComplete(() => callback?.Invoke());
            _canvasGroup.blocksRaycasts = _canvasGroup.interactable = isActive;
        }

        protected virtual void InitComponents()
        {
            _canvasGroup = GetComponent<CanvasGroup>();

            _canvasGroup.alpha = 0f;
            _canvasGroup.blocksRaycasts = _canvasGroup.interactable = false;
        }

    }   
}