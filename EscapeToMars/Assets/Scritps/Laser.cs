using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField]
    private bool _isActivated;

    private Animator _laserAnim;
    private Player _player;

    private void Awake()
    {
        _laserAnim = GetComponent<Animator>();
        StartCoroutine("CooldownToReference");
    }

    private void Start()
    {
        _laserAnim.SetBool("isActivated", _isActivated);
    }

    private void OnEnable()
    {
        _laserAnim = GetComponent<Animator>();
        StartCoroutine("CooldownToReference");
    }

    public void ActivateDeactivate(bool active)
    {
        _isActivated = active;
        _laserAnim.SetBool("isActivated", active);
    }

    public void ChangeActivation()
    {
        if (_isActivated)
        {
            _isActivated = false;
            _laserAnim.SetBool("isActivated", _isActivated);
        }
        else
        {
            _isActivated = true;
            _laserAnim.SetBool("isActivated", _isActivated);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && _isActivated)
        {
            _player.DyingPlayer();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !_isActivated)
        {
            ActivateDeactivate(true);
        }
    }

    IEnumerator CooldownToReference()
    {
        yield return new WaitForSeconds(0.8f);
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }
}
