using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    [SerializeField]
    private int _direction;
    [SerializeField]
    private int _initialDirection;
    [SerializeField]
    private int _delay;


    private Animator _arrowAnim;

    [SerializeField]
    private PlayerMovement _playerMovement;

    private void Awake()
    {
        _arrowAnim = GetComponent<Animator>();
        StartCoroutine("CooldownToReference");
    }

    private void Start()
    {
        _direction = _initialDirection;
        StartCoroutine("DelayToChangeDirection");
    }

    private void OnEnable()
    {
        _arrowAnim = GetComponent<Animator>();
        StartCoroutine("CooldownToReference");
    }

    public void ChangingDirectionAnim()
    {
        _arrowAnim.SetInteger("Direction", _direction);
    }

    public void TurnDirection()
    {
        switch (_direction)
        {
            case 0:
                StartCoroutine(DelayToMove());

                break;
            case 1:

                StartCoroutine(DelayToMove());
                break;
            case 2:

                StartCoroutine(DelayToMove());
                break;
            case 3:

                StartCoroutine(DelayToMove());
                break;

        }

    }

    private void CheckDirection()
    {

        _direction++;
        if(_direction > 3)
        {
            _direction = 0;
        }


    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            TurnDirection();
            _playerMovement.GettingArrow(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine("DelayToReturnControl");
        }
    }

    IEnumerator DelayToMove()
    {
        yield return new WaitForSeconds(0.1f);
        if (_direction == 0)
        {
            _playerMovement.MoveToDirection("right");
        }
        if (_direction == 1)
        {
            _playerMovement.MoveToDirection("left");
        }
        if (_direction == 2)
        {
            _playerMovement.MoveToDirection("up");
        }
        if (_direction == 3)
        {
            _playerMovement.MoveToDirection("down");
        }
    }

    IEnumerator CooldownToReference()
    {
        yield return new WaitForSeconds(0.8f);
        _playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();

    }

    IEnumerator DelayToReturnControl()
    {
        yield return new WaitForSeconds(0.15f);
        _playerMovement.GettingArrow(false);

    }

    IEnumerator DelayToChangeDirection()
    {

        yield return new WaitForSeconds(_delay);
        CheckDirection();
        ChangingDirectionAnim();
        StartCoroutine("DelayToChangeDirection");
    }

}
