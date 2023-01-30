using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Ui
{
    public class MainMenuView : MonoBehaviour
    {
        [SerializeField] private Button _buttonStart;
        [SerializeField] private Button _buttonRewardedAds;
        [SerializeField] private Button _buttonPurchase;

        public void Init(UnityAction startGame, UnityAction startAds,UnityAction purchase)
        {
            _buttonStart.onClick.AddListener(startGame);
            _buttonRewardedAds.onClick.AddListener(startAds);
            _buttonPurchase.onClick.AddListener(purchase);
        }

        public void OnDestroy()
        {
            _buttonStart.onClick.RemoveAllListeners();
            _buttonRewardedAds.onClick.RemoveAllListeners();
            _buttonPurchase.onClick.RemoveAllListeners();
        }

    }
}
