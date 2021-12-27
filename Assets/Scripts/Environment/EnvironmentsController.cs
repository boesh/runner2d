using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Utils;

namespace Assets.Scripts.Environment {
  public class EnvironmentsController : MonoBehaviour {
    [SerializeField] private EnvironmentView _groundPrefab;
    [SerializeField] private EnvironmentView _roofPrefab;
    [SerializeField] EnvironmentData _environmentData;
    private Queue<EnvironmentView> _pool;

    public void Initialize() {
      _pool = new Queue<EnvironmentView>();
      SetStartEnvironmentPositionAndRotation();
    }

    private void SetStartEnvironmentPositionAndRotation() {
      for (int i = 0; i < _environmentData._mapDepth; i++) {
        EnvironmentView ground = Instantiate(_groundPrefab);
        EnvironmentView roof = Instantiate(_roofPrefab);
        ground.Initialize(_environmentData._wallsCount);
        roof.Initialize(_environmentData._wallsCount);
        roof.transform.Rotate(new Vector3(180f, 0, 0));
        if (_pool.Count > 0) {
          ground.transform.position = new Vector3(_pool.Peek().transform.position.x + ground.GetSize().x, ground.GetSize().y / 2 - Settings.ScreenWorldSize.y);
          roof.transform.position = new Vector3(_pool.Peek().transform.position.x + roof.GetSize().x, Settings.ScreenWorldSize.y - ground.GetSize().y / 2);
        }
        else {
          roof.transform.position = new Vector3(-Settings.ScreenWorldSize.x, Settings.ScreenWorldSize.y - roof.GetSize().y / 2);
          ground.transform.position = new Vector3(-Settings.ScreenWorldSize.x, ground.GetSize().y / 2 - Settings.ScreenWorldSize.y);
        }
        _pool.Enqueue(ground);
        _pool.Enqueue(roof);
      }
    }

    private void LateUpdate() {
      if (_pool.Peek().transform.position.x < -Settings.ScreenWorldSize.x * 2) {
        UpdateEnviromentPosition();
      }
    }

    private void UpdateEnviromentPosition() {
      EnvironmentView tmp = _pool.Dequeue();
      tmp.transform.position = new Vector3(Settings.ScreenWorldSize.x * 2, tmp.transform.position.y);
      tmp.ResetWallsPositions();
      tmp.EnableWalls();
      _pool.Enqueue(tmp);
    }
  }
}