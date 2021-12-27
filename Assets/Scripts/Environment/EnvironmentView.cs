using UnityEngine;
using System.Collections.Generic;
using Assets.Scripts.Utils;
using Assets.Scripts.Wall;

namespace Assets.Scripts.Environment {
  public class EnvironmentView : MovableEnvironment {
    [SerializeField] private WallView _wallPrefab;
    [SerializeField] private int _wallsCount;
    [SerializeField] private BoxCollider2D _boxCollider2D;
    private Queue<WallView> _wallPool;

    public void Initialize(int wallsCount) {
      if (_boxCollider2D == null) {
        _boxCollider2D = transform.GetComponent<BoxCollider2D>();
      }
      if (_spriteRenderer == null) {
        _spriteRenderer = transform.GetComponent<SpriteRenderer>();
      }
      _wallsCount = wallsCount;
      _wallPool = new Queue<WallView>();
      SetSize(new Vector2(Settings.ScreenWorldSize.x * 2, _spriteRenderer.size.y));
      GenerateWalls();
      ResetWallsPositions();
      DisableWalls();
    }

    private void SetSize(Vector2 size) {
      _spriteRenderer.size = size;
      _boxCollider2D.size = size;
    }

    private void GenerateWalls() {
      for (int i = 0; i < _wallsCount; i++) {
        WallView wall = Instantiate(_wallPrefab);
        _wallPool.Enqueue(wall);
        wall.transform.parent = transform;
      }
    }

    public void ResetWallsPositions() {
      int i = 0;
      foreach (WallView wall in _wallPool) {
        float wallSpawnTerritory = _spriteRenderer.size.x / _wallsCount;
        float localStartPosition = Random.Range(wall.GetSize().x / 2f, wallSpawnTerritory - wall.GetSize().x / 2f);
        wall.transform.localPosition = new Vector3(localStartPosition + wallSpawnTerritory * i - _spriteRenderer.size.x / 2, _spriteRenderer.size.y / 2 + wall.GetSize().y / 2);
        i++;
      }
    }

    public void DisableWalls() {
      foreach (WallView wall in _wallPool) {
        wall.gameObject.SetActive(false);
      }
    }

    public void EnableWalls() {
      foreach (WallView wall in _wallPool) {
        wall.gameObject.SetActive(true);
      }
    }

    protected void FixedUpdate() {
      HorizontalMove();
    }
  }
}
