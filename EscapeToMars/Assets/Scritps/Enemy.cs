using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour {

    private GameObject _pointLight2D;
    private Rigidbody2D _enemyRB;
    private BoxCollider2D _enemyCollider;
    private Animator _currentAnim;
    private Vector2 _movementVector;

    private Player _player;

    private Transform[] _positionsTragectory;
    
    public float MovementSpeed { get; private set; } = 3.8f;
    public int NextPosition { get; set; } = 1;

    private void Awake() {
        StartCoroutine("CooldownToReference");
    }

    void Start() {
        StartingStats();

    }

    void FixedUpdate() {
        if (!GameManager.instance.isLoadingStep && GameManager.instance.GameStarted) {
            ToMove();
        }


    }


    void ReferenceGameObjects() {
        _pointLight2D = transform.Find("Point Light 2D").gameObject;

        _enemyRB = GetComponent<Rigidbody2D>();

        _enemyCollider = GetComponent<BoxCollider2D>();

        _currentAnim = GetComponent<Animator>();

        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

    }

    void StartingStats() {
        _positionsTragectory = transform.Find("Trajectory").GetComponentsInChildren<Transform>();

        for (int i = 0; i < _positionsTragectory.Length; i++) {

            _positionsTragectory[i].parent = null;
        }


    }


    void ToMove() {
        var _positionsTragectorySize = _positionsTragectory.Length - 1;
        

        transform.position = Vector2.MoveTowards(_enemyRB.transform.position, _positionsTragectory[NextPosition].position, MovementSpeed * Time.fixedDeltaTime);

        if (Vector2.Distance(_enemyRB.transform.position, _positionsTragectory[NextPosition].position) == 0) {
            NextPosition++;
            if (NextPosition > _positionsTragectorySize) {
                NextPosition = 0;
            }

        }

        
    }


    void Die() {
        _currentAnim.SetBool("isDying", true);
        AudioManager.instance.GetSoundEffect(4);
        Destroy(_enemyCollider);
        Destroy(_pointLight2D);
        StartCoroutine("CooldownDying");


    }

    private void OnTriggerEnter2D(Collider2D collision) {

        if (collision.gameObject.CompareTag("Player") && _player.Stronger) {
            Die();
            _player.GettingStronger(false);
        } else if (collision.gameObject.CompareTag("Player") && !_player.Stronger) {
            _player.DyingPlayer();
        }
        
    }

    IEnumerator CooldownDying() {
        yield return new WaitForSeconds(1.0f);
        Destroy(this.gameObject);
    }

    IEnumerator CooldownToReference()
    {
        yield return new WaitForSeconds(0.8f);
        ReferenceGameObjects();
    }
}
