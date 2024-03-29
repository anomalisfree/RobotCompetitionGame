using System;
using Main.Scripts.ApplicationCore.Controllers;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Main.Scripts.ApplicationCore.Clients
{
    public class MainClient : ClientBase
    {
        #region Params

        //Settings


        //Controllers
        [Header("Controllers")] 
        [SerializeField] private VrPlayerController vrPlayerController;
        [SerializeField] private SceneLoaderController sceneLoaderController;
        [SerializeField] private BundleLoaderController bundleLoaderController;
        [SerializeField] private RealtimeMultiplayerController realtimeMultiplayerController;
        [SerializeField] private TimelineController timelineTimerController;

        //Services

        #endregion

        //////////////////////////////////////////////////////////////////////////////////////

        #region Initialize

        protected override void InitializeServices()
        {
        }

        protected override void InitializeControllers()
        {
            Controllers.Add(vrPlayerController);
            Controllers.Add(sceneLoaderController);
            Controllers.Add(bundleLoaderController);
            Controllers.Add(realtimeMultiplayerController);
            Controllers.Add(timelineTimerController);
        }

        protected override void StartScenario()
        {
            InitializeVrPlayerController();
        }

        public override void LoadNewScene(string scene, Action onLoad, bool useBundle = false)
        {
            vrPlayerController.ResetPosition();
            
            sceneLoaderController.SceneIsLoaded = null;
            SceneManager.LoadScene(loadingSceneName);
            sceneLoaderController.SceneIsLoaded += onLoad.Invoke;

            if (useBundle)
                sceneLoaderController.InitFromBundle(scene);
            else
                sceneLoaderController.Init(scene);
        }
        
        public override void LoadNewScene(string scene, bool useBundle = false)
        {
            vrPlayerController.ResetPosition();

            SceneManager.LoadScene(loadingSceneName);
            
            if (useBundle)
                sceneLoaderController.InitFromBundle(scene);
            else
                sceneLoaderController.Init(scene);
        }
        
        public override void LoadNewScene(string scene, string room, bool useBundle = false)
        {
            vrPlayerController.ResetPosition();

            sceneLoaderController.SceneIsLoaded = null;
            SceneManager.LoadScene(loadingSceneName);
            sceneLoaderController.SceneIsLoaded += delegate { SceneIsLoaded(room); };
            
            if (useBundle)
                sceneLoaderController.InitFromBundle(scene);
            else
                sceneLoaderController.Init(scene);
        }

        #endregion

        //////////////////////////////////////////////////////////////////////////////////////
        
        private Transform _playerRoot;
        private (Transform leftHandRoot, Transform rightHandRoot) _handRoots;
        private Transform _bottomRoot;
        private Action _onFirstSceneLoad;

        private void InitializeVrPlayerController()
        {
            vrPlayerController.Ready += VrPlayerControllerReady;
            vrPlayerController.Init();
        }
        
        private void VrPlayerControllerReady(Transform playerRoot,
            (Transform leftHandRoot, Transform rightHandRoot) handRoots, Transform bottomRoot)
        {
            vrPlayerController.Ready -= VrPlayerControllerReady;
            _playerRoot = playerRoot;
            _handRoots = handRoots;
            _bottomRoot = bottomRoot;
            _onFirstSceneLoad += OnFirstSceneLoaded;
            LoadNewScene(scenes[0], _onFirstSceneLoad);
        }

        private void OnFirstSceneLoaded()
        {
            _onFirstSceneLoad -= OnFirstSceneLoaded;
        }

        private void SceneIsLoaded(string room)
        {
            if (string.IsNullOrEmpty(room)) return;

            realtimeMultiplayerController.Ready += RealtimeMultiplayerControllerReady;
            realtimeMultiplayerController.Init(room, _playerRoot, _handRoots, _bottomRoot);
        }

        private void RealtimeMultiplayerControllerReady()
        {
            realtimeMultiplayerController.Ready -= RealtimeMultiplayerControllerReady;
            InitializeTimelineController();
          
        }
        
        private void InitializeTimelineController()
        {
            timelineTimerController.Init();
        }

    }
}