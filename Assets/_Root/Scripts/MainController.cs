using Ui;
using Game;
using Profile;
using UnityEngine;
using Services.IAP;
using Services.Analytics;
using Services.Ads;
internal class MainController : BaseController
{
    private readonly Transform _placeForUi;
    private readonly ProfilePlayer _profilePlayer;

    private MainMenuController _mainMenuController;
    private GameController _gameController;

    private IIAPService _iapService;
    private IAdsService _adsService;
    private AnalyticsManager _analytics;
    public MainController(Transform placeForUi, ProfilePlayer profilePlayer, IIAPService iapService,
        IAdsService adsService, AnalyticsManager analytics)
    {
        _placeForUi = placeForUi;
        _profilePlayer = profilePlayer;

        _iapService = iapService;
        _adsService = adsService;
        _analytics = analytics;

        profilePlayer.CurrentState.SubscribeOnChange(OnChangeGameState);
        OnChangeGameState(_profilePlayer.CurrentState.Value);
    }

    protected override void OnDispose()
    {
        _mainMenuController?.Dispose();
        _gameController?.Dispose();

        _profilePlayer.CurrentState.UnSubscribeOnChange(OnChangeGameState);
    }


    private void OnChangeGameState(GameState state)
    {
        switch (state)
        {
            case GameState.Start:
                _mainMenuController = new MainMenuController(_placeForUi, _profilePlayer,_adsService,_iapService);
                _gameController?.Dispose();
                break;
            case GameState.Game:
                _gameController = new GameController(_profilePlayer,_analytics);
                _mainMenuController?.Dispose();
                break;
            default:
                _mainMenuController?.Dispose();
                _gameController?.Dispose();
                break;
        }
    }
}
