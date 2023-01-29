using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Ui
{
    public class MainMenuView : MonoBehaviour
    {
        [SerializeField] private Button _buttonStart;
        [SerializeField] private Button _buttonRewardedAds;

        public void Init(UnityAction startGame, UnityAction startAds)
        {
            _buttonStart.onClick.AddListener(startGame);
            _buttonRewardedAds.onClick.AddListener(startAds);
        }

        public void OnDestroy()
        {
            _buttonStart.onClick.RemoveAllListeners();
            _buttonRewardedAds.onClick.RemoveAllListeners();
        }

    }
}
