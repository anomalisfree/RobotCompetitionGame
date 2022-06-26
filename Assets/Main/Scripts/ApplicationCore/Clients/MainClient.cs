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
        private Action _onFirstSceneLoad;

        private void InitializeVrPlayerController()
        {
            vrPlayerController.Ready += VrPlayerControllerReady;
            vrPlayerController.Init();
        }
        
        private void VrPlayerControllerReady(Transform playerRoot,
            (Transform leftHandRoot, Transform rightHandRoot) handRoots)
        {
            Debug.Log("VR Ready");
            vrPlayerController.Ready -= VrPlayerControllerReady;
            _playerRoot = playerRoot;
            _handRoots = handRoots;
            _onFirstSceneLoad += OnFirstSceneLoaded;
            LoadNewScene(scenes[0], _onFirstSceneLoad);
        }

        private void OnFirstSceneLoaded()
        {
            _onFirstSceneLoad -= OnFirstSceneLoaded;
            InitializeMultiplayerController();
        }

        private void SceneIsLoaded(string room)
        {
            if (string.IsNullOrEmpty(room)) return;

            // realtimeMultiplayerController.Ready += RealtimeMultiplayerControllerReady;
            // realtimeMultiplayerController.Init(room, _playerRoot, _handRoots, _loginResponseData);
        }
        
        private void InitializeMultiplayerController()
        {
           
            realtimeMultiplayerController.Ready += RealtimeMultiplayerControllerReady;
            realtimeMultiplayerController.Init("TestRoom", _playerRoot, _handRoots);
        }
        
        private void RealtimeMultiplayerControllerReady()
        {
            realtimeMultiplayerController.Ready -= RealtimeMultiplayerControllerReady;
          
        }
    }
}