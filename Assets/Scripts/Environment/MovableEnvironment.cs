using UnityEngine;
using Assets.Scripts.View;

namespace Assets.Scripts {
  public abstract class MovableEnvironment : BaseView {
    [SerializeField] private int _speed;

    protected virtual void HorizontalMove() {
      transform.position = new Vector3(transform.position.x - _speed * Time.fixedDeltaTime, transform.position.y);
    }
  }
}