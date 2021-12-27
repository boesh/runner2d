using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI {
  public class GameLosePanel : MonoBehaviour {
    [SerializeField] Button _restartButton;

    private void Awake() {
      AddListeners();
    }

    private void OnDestroy() {
      RemoveListeners();
    }

    private void AddListeners() {
      _restartButton.onClick.AddListener(OnRestartGame);
    }

    private void RemoveListeners() {
      _restartButton.onClick.RemoveListener(OnRestartGame);
    }

    private void OnRestartGame() {
      EventAggregator.OnRestartGame?.Invoke();
    }
  }
}