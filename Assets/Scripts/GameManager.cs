using UnityEngine;
using UnityEngine.SceneManagement;
using Assets.Scripts.Utils;
using Assets.Scripts.Environment;
using Assets.Scripts.UI;

namespace Assets.Scripts {
  public class GameManager : MonoBehaviour {
    [SerializeField] private GameLosePanel _gameLosePanel;
    [SerializeField] private EnvironmentsController _environmentsController;
    [SerializeField] private Player _player;
    [SerializeField] private Settings _settings;

    private void Awake() {
      EventAggregator.OnGameLose += OnGameLose;
      EventAggregator.OnRestartGame += OnResstartGame;
      _settings.Initialize();
      Instantiate(_player).Initialize();
      Instantiate(_environmentsController).Initialize();
    }

    private void OnResstartGame() {
      Time.timeScale = 1f;
      SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void OnDestroy() {
      EventAggregator.OnGameLose -= OnGameLose;
      EventAggregator.OnRestartGame -= OnResstartGame;
    }

    private void OnGameLose() {
      _gameLosePanel.gameObject.SetActive(true);
      Time.timeScale = 0f;
    }
  }
}