using Managers;
using System.Collections;
using Systems;
using UnityEngine;

namespace Entities
{
    public class Enemy : MonoBehaviour
    {

        private GameObject _pointLight2D;
        private Rigidbody2D _enemyRB;
        private BoxCollider2D _enemyCollider;
        private Animator _currentAnim;

        private Player _player;

        private Transform[] _positionsTragectory;

        public float MovementSpeed { get; private set; } = 3.8f;
        public int NextPosition { get; set; } = 1;

        private void Awake()
        {
            ReferenceGameObjects();
            StartCoroutine("CooldownToReference");
        }

        private void OnEnable()
        {
            ReferenceGameObjects();
            StartCoroutine("CooldownToReference");
        }

        void Start()
        {
            StartingStats();
        }

        void FixedUpdate()
        {
            if (!GameManager.Instance.IsLoadingStep && GameManager.Instance.GameStarted)
            {
                ToMove();
            }
        }


        void ReferenceGameObjects()
        {
            _pointLight2D = transform.Find("Point Light 2D").gameObject;

            _enemyRB = GetComponent<Rigidbody2D>();

            _enemyCollider = GetComponent<BoxCollider2D>();

            _currentAnim = GetComponent<Animator>();



        }

        void StartingStats()
        {
            _positionsTragectory = transform.Find("Trajectory").GetComponentsInChildren<Transform>();

            for (int i = 0; i < _positionsTragectory.Length; i++)
            {

                _positionsTragectory[i].parent = null;
            }


        }


        void ToMove()
        {
            var positionsTragectorySize = _positionsTragectory.Length - 1;
            
            bool isMovingUp = _enemyRB.transform.position.x == _positionsTragectory[NextPosition].position.x && _enemyRB.transform.position.y < _positionsTragectory[NextPosition].position.y;
            bool isMovingDown = _enemyRB.transform.position.x == _positionsTragectory[NextPosition].position.x && _enemyRB.transform.position.y > _positionsTragectory[NextPosition].position.y;
            bool isMovingRight = _enemyRB.transform.position.x < _positionsTragectory[NextPosition].position.x && _enemyRB.transform.position.y == _positionsTragectory[NextPosition].position.y;
            bool isMovingLeft = _enemyRB.transform.position.x > _positionsTragectory[NextPosition].position.x && _enemyRB.transform.position.y == _positionsTragectory[NextPosition].position.y;

           
            transform.position = Vector2.MoveTowards(_enemyRB.transform.position, _positionsTragectory[NextPosition].position, MovementSpeed * Time.fixedDeltaTime);

            if (Vector2.Distance(_enemyRB.transform.position, _positionsTragectory[NextPosition].position) == 0)
            {
                NextPosition++;
                if (NextPosition > positionsTragectorySize)
                {
                    NextPosition = 0;
                }
            }
            
            if (isMovingUp)
            {
                _currentAnim.SetBool("IsMovingUp", true);
                _currentAnim.SetBool("IsMovingDown", false);
                _currentAnim.SetBool("IsMovingRight", false);
                _currentAnim.SetBool("IsMovingLeft", false);
            }
            if (isMovingDown)
            {
                _currentAnim.SetBool("IsMovingUp", false);
                _currentAnim.SetBool("IsMovingDown", true);
                _currentAnim.SetBool("IsMovingRight", false);
                _currentAnim.SetBool("IsMovingLeft", false);
            }
            if (isMovingRight)
            {
                _currentAnim.SetBool("IsMovingUp", false);
                _currentAnim.SetBool("IsMovingDown", false);
                _currentAnim.SetBool("IsMovingRight", true);
                _currentAnim.SetBool("IsMovingLeft", false);
            }
            if (isMovingLeft)
            {
                _currentAnim.SetBool("IsMovingUp", false);
                _currentAnim.SetBool("IsMovingDown", false);
                _currentAnim.SetBool("IsMovingRight", false);
                _currentAnim.SetBool("IsMovingLeft", true);
            }
            
        }


        void Die()
        {
            _currentAnim.SetBool("isDying", true);
            AudioManager.Instance.PlaySoundEffect(4);
            _player.CountKill();
            SteamAchievement.Instance.SettingAchievementKill();
            Destroy(_enemyCollider);
            Destroy(_pointLight2D);
            StartCoroutine("CooldownDying");
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("Player") && _player.Stronger)
            {
                Die();
                _player.GettingStronger(false);
            }
            else if (collision.gameObject.CompareTag("Player") && !_player.Stronger)
            {
                _player.DyingPlayer();
            }

        }

        IEnumerator CooldownDying()
        {
            yield return new WaitForSeconds(1.0f);
            Destroy(gameObject);
        }

        IEnumerator CooldownToReference()
        {
            yield return new WaitForSeconds(0.8f);
            _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        }
    }
}