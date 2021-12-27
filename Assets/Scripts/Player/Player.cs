using UnityEngine;
using Assets.Scripts.Utils;

namespace Assets.Scripts {
  public enum PlayerStates {
    Default,
    OnGround,
    InJump,
    InDoubleJump,
    OnRoof,
    DropFromRoof
  }

  public class Player : MonoBehaviour{
    private Rigidbody2D _rb;
    [SerializeField] private PlayerData _playerData;


    public void Initialize() {
      if (_rb == null) {
        _rb = GetComponent<Rigidbody2D>();
      }
    }

    private void OnCollisionEnter2D(Collision2D collision) {
      if (collision.gameObject.tag == CustomTags.ground.ToString()) {
        PlayerOnGround();
      } else if (collision.gameObject.tag == CustomTags.roof.ToString()) {
        PlayerOnRoof();
      } else if (collision.gameObject.tag == CustomTags.deathBlock.ToString()) {
        PlayerDie();
      }
    }

    private void PlayerDie() {
      EventAggregator.OnGameLose?.Invoke();
    }

    private void PlayerOnRoof() {
      _playerData._state = PlayerStates.OnRoof;
    }

    private void PlayerOnGround() {
      _playerData._state = PlayerStates.OnGround;
    }

    private void Jump() {
      if (_playerData._state == PlayerStates.OnGround) {
        SimpleJump();
      } else if (_playerData._state == PlayerStates.InJump) {
        DoubleJump();
      } else if (_playerData._state == PlayerStates.OnRoof) {
        DropFromRoof();
      }
    }

    private void DoubleJump() {
      _rb.gravityScale *= -1;
      _playerData._state = PlayerStates.InDoubleJump;
    }

    private void DropFromRoof() {
      DoubleJump();
      _playerData._state = PlayerStates.DropFromRoof;
    }

    private void SimpleJump() {
      _playerData._state = PlayerStates.InJump;
      _rb.AddForce(Vector2.up * _playerData._jumpHeight, ForceMode2D.Impulse);
    }

    private void Update() {
      if (Input.GetKeyDown(KeyCode.Space)) {
        Jump();
      }
    }
  }
}