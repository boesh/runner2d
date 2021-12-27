using UnityEngine;

namespace Assets.Scripts {
  [CreateAssetMenu(fileName = "PlayerData", menuName = "ScriptableObjects/PlayerData", order = 1)]
  public class PlayerData : ScriptableObject {
    public PlayerStates _state;
    public int _jumpHeight;
  }
}
