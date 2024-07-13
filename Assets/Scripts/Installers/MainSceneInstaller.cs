using CardGuess.Components;
using CardGuess.Controllers;
using CardGuess.Models;
using UnityEngine;
using Zenject;

namespace CardGuess.Installers
{
    public class MainSceneInstaller : MonoInstaller
    {
        [Header("Scriptable Objects")] 
        [SerializeField] private CardViewStorage cardViewStorage;
        [SerializeField] private TimerData timerData;
        [SerializeField] private FieldCardConfig fieldCardConfig;
        
        [Header("Card Pool")] 
        [SerializeField] private CardPool cardPrefab;
        [SerializeField] private int defaultCardCount = 20;
        [SerializeField] private Transform cardSpawnObject;
        
        public override void InstallBindings()
        {
            InstallScriptableObjects();
            InstallSignals();
            Container.BindInterfacesTo<CardPositionStorage>().FromComponentInHierarchy().AsSingle();
            InstallPools();
            InstallGameComponents();
        }

        private void InstallSignals()
        {
            SignalBusInstaller.Install(Container);
            Container.DeclareSignal<CardChooseSignal>();
            Container.DeclareSignal<DealCardSignal>();
        }

        private void InstallGameComponents()
        {
            Container.BindInterfacesTo<TimerController>().AsSingle();
            Container.BindInterfacesTo<CardGameField>().AsSingle();
            Container.BindInterfacesTo<CardShuffler>().AsSingle();
            Container.BindInterfacesTo<CardFieldChooseController>().AsSingle();
            Container.BindInterfacesAndSelfTo<GameCardSpawnController>().AsSingle();
            Container.BindInterfacesTo<GameStarter>().FromComponentInHierarchy().AsSingle();
            Container.BindInterfacesTo<GameEndController>().AsSingle();
        }

        private void InstallScriptableObjects()
        {
            Container.Bind<CardViewStorage>().FromInstance(cardViewStorage).AsSingle();
            Container.Bind<TimerData>().FromInstance(timerData).AsSingle();
            Container.Bind<FieldCardConfig>().FromInstance(fieldCardConfig).AsSingle();
        }
        
        private void InstallPools()
        {
            Container.BindMemoryPool<CardPool, CardPool.Pool>().WithInitialSize(defaultCardCount).
                FromComponentInNewPrefab(cardPrefab).UnderTransform(cardSpawnObject);

            Container.BindInterfacesTo<CardSpawner>().AsSingle();
        }

    }   
}