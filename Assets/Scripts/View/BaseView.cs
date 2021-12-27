using UnityEngine;

namespace Assets.Scripts.View {
  public class BaseView : MonoBehaviour, IView {
    [SerializeField] protected SpriteRenderer _spriteRenderer;

    public Vector2 GetSize() {
      return _spriteRenderer.size;
    }
  }
}
