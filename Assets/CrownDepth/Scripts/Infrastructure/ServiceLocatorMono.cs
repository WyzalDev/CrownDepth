using CrownDepth.Dialogue;
using CrownDepth.Limb;
using CrownDepth.Paralax;
using UnityEngine;

namespace CrownDepth.Infrastructure
{
    public class ServiceLocatorMono : MonoBehaviour
    {
        #region ServiceLocator Members

        public static ServiceLocatorMono Instance { get; private set; }

        private ServiceLocator _serviceLocator;

        private void Awake()
        {
            if (Instance != null) return;
            DontDestroyOnLoad(this);
            _serviceLocator = new ServiceLocator();
            RegisterAllDependencies();
            Instance = this;
        }

        public bool TryGetService<T>(out T service)
        {
            service = _serviceLocator.Get<T>();
            return service != null;
        }

        #endregion

        [SerializeField] private DialogueController dialogueController;

        [SerializeField] private ChoiceUIController choiceUIController;
        
        [SerializeField] private ParalaxNextStageChecker paralaxNextStageChecker;
        
        [SerializeField] private LimbsViewController limbsViewController;


        //Register all server services and entities here 
        private void RegisterAllDependencies()
        {
            registerMonobehaviours();
        }


        #region REGISTRATIONS

        private void registerMonobehaviours()
        {
            _serviceLocator.Register<DialogueController>(dialogueController);
            _serviceLocator.Register<ChoiceUIController>(choiceUIController);
            _serviceLocator.Register<ParalaxNextStageChecker>(paralaxNextStageChecker);
            _serviceLocator.Register<LimbsViewController>(limbsViewController);
        }

        #endregion
    }
}