using Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    [SerializeField]
    private bool _isFarTelep;

    private GameObject _cameraMain;
    private GameObject[] _allTeleport;

    [SerializeField]
    private Transform _destiny;
    private Animator _telepAnim;

    private PlayerMovement _playerMovement;

    private void Awake()
    {
        ReferenceGameObjects();
        StartCoroutine("CooldownToReference");

    }
    private void FixedUpdate()
    {
        transform.Rotate(new Vector3(transform.rotation.x, transform.rotation.y, 10f * Time.deltaTime));
    }

    private void OnEnable()
    {
        ReferenceGameObjects();
        StartCoroutine("CooldownToReference");
    }

    void ReferenceGameObjects()
    {
        _destiny = transform.Find("Destiny").GetComponent<Transform>();
        _destiny.transform.parent = null;

        _telepAnim = GetComponent<Animator>();
        _allTeleport = GameObject.FindGameObjectsWithTag("Teleport");
        _cameraMain = GameObject.FindGameObjectWithTag("MainCamera");
    }

  

    public void TeleportingToDestiny()
    {
        _playerMovement.MoveToDestinyWithPortal(_destiny);

        if (_isFarTelep)
        {
            _cameraMain.transform.position = new Vector3(_playerMovement.transform.position.x, _playerMovement.transform.position.y, -2f);
        }
        
        _playerMovement.GettingPortal(false);

    }

    void ActiveDesactiveBoxCollider()
    {
        for (int i = 0; i < _allTeleport.Length; i++)
        {

            _allTeleport[i].GetComponent<Animator>().SetBool("isClosing", true);
            _allTeleport[i].GetComponent<BoxCollider2D>().enabled = false;
        }
        StartCoroutine(CooldownEnableBox());

    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            ActiveDesactiveBoxCollider();
            AudioManager.Instance.PlaySoundEffect(6);
            _playerMovement.GettingPortal(true);
            _telepAnim.SetBool("isClosing", true);
            TeleportingToDestiny();

        }
    }


    IEnumerator CooldownToReference()
    {
        yield return new WaitForSeconds(0.8f);
        _playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();

    }
    IEnumerator CooldownEnableBox()
    {
        yield return new WaitForSeconds(2f);
        for (int i = 0; i < _allTeleport.Length; i++)
        {
            _allTeleport[i].GetComponent<Animator>().SetBool("isClosing", false);
            _allTeleport[i].GetComponent<BoxCollider2D>().enabled = true;
        }

    }
}
