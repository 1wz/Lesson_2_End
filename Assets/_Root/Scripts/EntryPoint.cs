using Profile;
using UnityEngine;
using Services.IAP;
using Services.Analytics;
using Services.Ads.UnityAds;
using Services;
using System;
using UnityEngine.Events;

internal class EntryPoint : MonoBehaviour
{
    private const float SpeedCar = 15f;
    private const GameState InitialState = GameState.Start;

    [SerializeField] private Transform _placeForUi;
    [SerializeField] private UnityAdsSettings _unityAdsSettings;
    [SerializeField] private ProductLibrary _productLibrary;

    private IAPService _iapService;
    private UnityAdsService _adsService;
    private AnalyticsManager _analytics;

    private MainController _mainController;


    private void Start()
    {
        SetServices();

        var profilePlayer = new ProfilePlayer(SpeedCar, InitialState);
        _mainController = new MainController(_placeForUi, profilePlayer,_iapService,_adsService,_analytics);

        _analytics.SendMainMenuOpened();
        if (_adsService.IsInitialized) OnAdsInitialized();
        else _adsService.Initialized.AddListener(OnAdsInitialized);

        if (_iapService.IsInitialized) OnIapInitialized();
        else _iapService.Initialized.AddListener(OnIapInitialized);
    }

    private void SetServices()
    {        
        UnityEvent purchaseSucced = new UnityEvent();
        purchaseSucced.AddListener(() => Debug.Log("PurchaseSucced"));

        _adsService = new UnityAdsService(_unityAdsSettings, null);
        _iapService = new IAPService(_productLibrary, null,purchaseSucced , null);
        _analytics = new AnalyticsManager();
        ServiceLocator.SetService(_adsService);
        ServiceLocator.SetService(_iapService);
        ServiceLocator.SetService(_analytics);
    }

    private void OnDestroy()
    {
        _adsService.Initialized.RemoveListener(OnAdsInitialized);
        _iapService.Initialized.RemoveListener(OnIapInitialized);
        _mainController.Dispose();
    }


    private void OnAdsInitialized() => _adsService.InterstitialPlayer.Play();
    private void OnIapInitialized() => _iapService.Buy("product_1");
}
