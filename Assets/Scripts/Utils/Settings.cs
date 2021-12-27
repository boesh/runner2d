using UnityEngine;

namespace Assets.Scripts.Utils {

  public enum CustomTags {
    deathBlock,
    roof,
    ground
  }

  public class Settings : MonoBehaviour {
    static private Vector3 _screenWorldSize;

    public void Initialize() {
      _screenWorldSize = new Vector3(Screen.width, Screen.height);
      _screenWorldSize = Camera.main.ScreenToWorldPoint(_screenWorldSize);
    }

    public static Vector2 ScreenWorldSize { get => _screenWorldSize; }
  }
}