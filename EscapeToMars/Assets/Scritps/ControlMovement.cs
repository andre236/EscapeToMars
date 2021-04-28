using UnityEngine;

public class ControlMovement : MonoBehaviour {
    
    
    private PlayerMovement _playerMovement;

    private void Awake() {
        _playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
    }

    private void OnMouseDown() {
        if (gameObject.name == "UpButton" && GameManager.instance.GameStarted) {
            _playerMovement.MoveUpPlayer();
        } else if (gameObject.name=="DownButton" && GameManager.instance.GameStarted) {
            _playerMovement.MoveDownPlayer();
        } else if (gameObject.name == "LeftButton" && GameManager.instance.GameStarted) {
            _playerMovement.MoveLeftPlayer();
        }else if (gameObject.name == "RightButton" && GameManager.instance.GameStarted) {
            _playerMovement.MoveRightPlayer();
        }
    }

}
