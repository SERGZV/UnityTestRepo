using UnityEngine;
using UnityEngine.Advertisements;

public class InterstitialAd : MonoBehaviour, IUnityAdsLoadListener, IUnityAdsShowListener
{
    [SerializeField] private AdvertisingService adService;
    public static InterstitialAd singletone;
    
    private string _androidAdUnitId = "Interstitial_Android";
    private string _iosAdUnitId = "Interstitial_iOS";
    private string _adUnitId;
    
    private void Awake()
    {
        singletone = this;
        _adUnitId = (Application.platform == RuntimePlatform.IPhonePlayer) ? _iosAdUnitId : _androidAdUnitId;
    }

    private void OnEnable()
    {
        adService.InitializationComplete += OnInitializationComplete;
    }

    private void OnDisable()
    {
        adService.InitializationComplete -= OnInitializationComplete;
    }
    
    private void OnInitializationComplete()
    {
        LoadAd();
    }
    
    private void LoadAd()
    {
        Advertisement.Load(_adUnitId, this);
    }

    public void ShowAd()
    {
        Advertisement.Show(_adUnitId, this);
    }
    
    public void OnUnityAdsAdLoaded(string placementId)
    {
        // Ad is not ready need to wait
        Debug.Log("Interstitial Ad Ready");
    }

    public void OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message)
    {
        Debug.Log("Interstitial ad failed to load");
    }

    public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
    {
        Debug.Log("Failed to show ads: " + placementId);
    }

    public void OnUnityAdsShowStart(string placementId) {}

    public void OnUnityAdsShowClick(string placementId) {}

    public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
    {
        LoadAd();
    }
}
