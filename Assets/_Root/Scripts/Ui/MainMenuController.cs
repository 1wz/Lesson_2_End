using Profile;
using Tool;
using UnityEngine;
using Object = UnityEngine.Object;
using Services.Ads;
using Services.IAP;

namespace Ui
{
    internal class MainMenuController : BaseController
    {
        private readonly ResourcePath _resourcePath = new ResourcePath("Prefabs/MainMenu");
        private readonly ProfilePlayer _profilePlayer;
        private readonly MainMenuView _view;
        const string prodactId = "product_1";


        public MainMenuController(Transform placeForUi, ProfilePlayer profilePlayer,
            IAdsService adsService, IIAPService iAPservice)
        {
            _profilePlayer = profilePlayer;
            _view = LoadView(placeForUi);
            _view.Init(StartGame,adsService.RewardedPlayer.Play,()=>iAPservice.Buy(prodactId));
        }


        private MainMenuView LoadView(Transform placeForUi)
        {
            GameObject prefab = ResourcesLoader.LoadPrefab(_resourcePath);
            GameObject objectView = Object.Instantiate(prefab, placeForUi, false);
            AddGameObject(objectView);

            return objectView.GetComponent<MainMenuView>();
        }

        private void StartGame() =>
            _profilePlayer.CurrentState.Value = GameState.Game;
    }
}
