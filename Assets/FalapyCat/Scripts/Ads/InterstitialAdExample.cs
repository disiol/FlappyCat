using System;
using System.Threading.Tasks;
using Unity.Services.Core;
using Unity.Services.Mediation;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Ads
{
    public class InterstitialAdExample : MonoBehaviour, IDisposable
    {
        [SerializeField] bool testMode = true;
        [SerializeField] private string adUnitId = "Interstitial_Android";
        [SerializeField] private string gameId = "4919417";
        [SerializeField] private GameObject ProgressBar;
      
        IInterstitialAd ad;

        private void Start()
        {
            InitServices();
        }

        private async Task InitServices()
        {
            try
            {
                ProgressBar.SetActive(true);
                InitializationOptions initializationOptions = new InitializationOptions();
                initializationOptions.SetGameId(gameId);
                initializationOptions.SetOption("testMode", testMode);
                await UnityServices.InitializeAsync(initializationOptions);

                InitializationComplete();
            }
            catch (Exception e)
            {
                ProgressBar.SetActive(false);

                InitializationFailed(e);
            }
        }

        public void SetupAd()
        {
            //Create
            ad = MediationService.Instance.CreateInterstitialAd(adUnitId);

            //Subscribe to events
            ad.OnClosed += AdClosed;
            ad.OnClicked += AdClicked;
            ad.OnLoaded += AdLoaded;
            ad.OnFailedLoad += AdFailedLoad;

            // Impression Event
            MediationService.Instance.ImpressionEventPublisher.OnImpression += ImpressionEvent;
        }

        public void Dispose() => ad?.Dispose();


        public async void ShowAd()
        {
            if (ProgressBar != null)
            {
                ProgressBar.SetActive(false);

            }

            if (ad.AdState == AdState.Loaded)
            {
                try
                {
                    InterstitialAdShowOptions showOptions = new InterstitialAdShowOptions();
                    showOptions.AutoReload = true;
                    await ad.ShowAsync(showOptions);
                    AdShown();
                }
                catch (ShowFailedException e)
                {
                    AdFailedShow(e);
                }
            }
        }

        void InitializationComplete()
        {
            SetupAd();
            LoadAd();
        }

        async Task LoadAd()
        {
            try
            {
                await ad.LoadAsync();
            }
            catch (LoadFailedException)
            {
                // We will handle the failure in the OnFailedLoad callback
            }
        }

        void InitializationFailed(Exception e)
        {
            Debug.Log("Initialization Failed: " + e.Message);
            LoadNextScene();
        }

        void AdLoaded(object sender, EventArgs e)
        {
            Debug.Log("Ad loaded");
            ShowAd();
        }

        void AdFailedLoad(object sender, LoadErrorEventArgs e)
        {
            Debug.LogError("Failed to load ad");
            Debug.LogError(e.Message);

            LoadAd();
        }

        void AdShown()
        {
            Debug.Log("Ad shown!");
        }

        void AdClosed(object sender, EventArgs e)
        {
            Debug.Log("Ad has closed");
            // Execute logic after an ad has been closed.
            LoadNextScene();
        }

        void AdClicked(object sender, EventArgs e)
        {
            Debug.Log("Ad has been clicked");
            // Execute logic after an ad has been clicked.
        }

        void AdFailedShow(ShowFailedException e)
        {
            Debug.LogError(e.Message);
            LoadNextScene();

        }

        void ImpressionEvent(object sender, ImpressionEventArgs args)
        {
            var impressionData = args.ImpressionData != null ? JsonUtility.ToJson(args.ImpressionData, true) : "null";
            Debug.Log("Impression event from ad unit id " + args.AdUnitId + " " + impressionData);
        }
        
        private static void LoadNextScene()
        {
            SceneManager.LoadScene("Scenes/Game");
        }
    }
}