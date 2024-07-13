using System;
using CardGuess.Components;
using CardGuess.Models;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace CardGuess.View
{
    [RequireComponent(typeof(CardPool), typeof(Button))]
    public class CardFlipButton : MonoBehaviour
    {
        private ICardComponentGetter _cardComponentGetter;
        private SignalBus _signalBus;
        
        private Button _button;

        [Inject]
        private void Construct(SignalBus signalBus)
        {
            _signalBus = signalBus;
        }

        private void Awake()
        {
            _cardComponentGetter = GetComponent<CardPool>();
            _button = GetComponent<Button>();
            
            _signalBus.Subscribe<DealCardSignal>(ChangeButtonInteractable);
        }

        private void OnEnable()
        {
            _button.onClick.AddListener(Press);
            _cardComponentGetter.CardView.Closed += ActiveButton;
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(Press);
            _cardComponentGetter.CardView.Closed -= ActiveButton;
        }

        private void OnDestroy()
        {
            _signalBus.Unsubscribe<DealCardSignal>(ChangeButtonInteractable);
        }

        private void ChangeButtonInteractable(DealCardSignal dealCardSignal)
        {
            _button.interactable = !dealCardSignal.IsDealProgress;
        }

        private void Press()
        {
            _signalBus.Fire(new CardChooseSignal() { CardId = _cardComponentGetter.CardId });
        }

        private void ActiveButton()
        {
            _button.interactable = true;
        }
    }   
}