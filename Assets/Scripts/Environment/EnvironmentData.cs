using UnityEngine;

namespace Assets.Scripts.Environment {
  [CreateAssetMenu(fileName = "EnvironmentData", menuName = "ScriptableObjects/EnvironmentData", order = 1)]
  public class EnvironmentData : ScriptableObject{
    public int _mapDepth;
    public int _wallsCount;
  }
}
