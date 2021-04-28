using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonLaser : MonoBehaviour
{
    [SerializeField]
    private bool _isPressed;

    private Animator _buttonAnim;
    private GameObject[] _lasers;


    private void Awake()
    {
        _buttonAnim = GetComponent<Animator>();
        _lasers = GameObject.FindGameObjectsWithTag("Laser");
    }

    private void Start()
    {
        _isPressed = false;
        _buttonAnim.SetBool("isPressed", _isPressed);
    }

    public void SwitchLaser()
    {
        if (_isPressed)
        {
            _isPressed = false;
            _buttonAnim.SetBool("isPressed", _isPressed);
        }
        else
        {
            _isPressed = true;
            _buttonAnim.SetBool("isPressed", _isPressed);
        }

        for(int i = 0; i< _lasers.Length; i++)
        {
            _lasers[i].GetComponent<Laser>().ChangeActivation();
        }

        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            SwitchLaser();
        }
    }

}
