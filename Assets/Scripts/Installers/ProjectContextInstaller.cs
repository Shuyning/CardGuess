using CardGuess.Controllers;
using UnityEngine;
using Zenject;

namespace CardGuess.Installers
{
    public class ProjectContextInstaller : MonoInstaller
    {
        [Header("Audio components")]
        [SerializeField] private AudioController audioController;
        [SerializeField] private SoundStorage soundStorage;
        
        public override void InstallBindings()
        {
            Container.Bind<SoundStorage>().FromInstance(soundStorage).AsSingle();
            Container.BindInterfacesTo<PlayerPrefsDataSaver>().AsSingle();
            Container.BindInterfacesTo<AudioController>().FromInstance(audioController).AsSingle();
            Container.BindInterfacesTo<PauseController>().AsSingle();
        }
    }   
}