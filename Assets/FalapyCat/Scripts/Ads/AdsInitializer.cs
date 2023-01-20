// using System;
// using UnityEngine;
// using UnityEngine.Advertisements;
// using UnityEngine.SceneManagement;
//
// namespace Ads
// {
//     public class AdsInitializer : MonoBehaviour, IUnityAdsInitializationListener
//     {
//         [SerializeField] string _androidGameId;
//         [SerializeField] string _iOSGameId;
//         [SerializeField] bool _testMode = true;
//
//         [SerializeField] private GameObject ProgressBar;
//
//
//         private string _gameId;
//
//         // void Awake()
//         // {
//         //     InitializeAds();
//         // }
//
//
//         public void InitializeAds()
//         {
//             ProgressBar.SetActive(true);
//
//             _gameId = (Application.platform == RuntimePlatform.IPhonePlayer)
//                 ? _iOSGameId
//                 : _androidGameId;
//             Advertisement.Initialize(_gameId, _testMode, this);
//         }
//
//         public void OnInitializationComplete()
//         {
//             ProgressBar.SetActive(false);
//
//
//             Debug.Log("Unity Ads initialization complete.");
//             gameObject.GetComponent<InterstitialAdExample>().LoadAd();
//         }
//
//         public void OnInitializationFailed(UnityAdsInitializationError error, string message)
//         {
//             Debug.Log($"Unity Ads Initialization Failed: {error.ToString()} - {message}");
//
//             ProgressBar.SetActive(false);
//             SceneManager.LoadScene("SocialLogin/Authentication/Login");
//         }
//     }
// }