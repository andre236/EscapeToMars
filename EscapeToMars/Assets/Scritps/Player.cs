using System.Collections;
using UnityEngine;


public class Player : MonoBehaviour {

    private GameObject _eletricityGO;
    private Animator _playerAnim;
    private ParticleSystem _particleDeathPlayer;

    private SpriteRenderer _playerSprite;

    private Rigidbody2D _playerRB;
    private BoxCollider2D _playerBoxColl;

    private PlayerMovement _playerMovement;
    
    public bool Stronger { get; private set; } = false;
    public bool IsDead { get; private set; } = false;
    
    void Awake() {
        ReferenceGameObjects();
       
    }

    private void Start()
    {
        StartCoroutine(PlayerEmergingSound());
    }

 

    void ReferenceGameObjects() {
        _playerSprite = GetComponent<SpriteRenderer>();
        _playerAnim = GetComponent<Animator>();
        _playerBoxColl = GetComponent<BoxCollider2D>();
        _playerRB = GetComponent<Rigidbody2D>();
        _eletricityGO = transform.Find("Eletricity").gameObject;
        _eletricityGO.SetActive(Stronger);
        _playerMovement = GetComponent<PlayerMovement>();
    }



    public bool GettingStronger(bool state) {
        if (state) {
            _eletricityGO.SetActive(true);
        } else {
            _eletricityGO.SetActive(false);
        }
        return Stronger = state;
    }



    public void DyingPlayer() {
        IsDead = true;
        _playerAnim.SetBool("isDying", true);
        Destroy(GetComponent<BoxCollider2D>());
        AudioManager.instance.GetSoundEffect(2);
        GameManager.instance.GameOver();
    }


    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("Flag")) {
            Destroy(GetComponent<BoxCollider2D>());
            StartCoroutine("CooldownToAnimate");
            AudioManager.instance.GetSoundEffect(3);
        }

        if (collision.gameObject.CompareTag("Cyclop") && !Stronger) {
            //DyingPlayer();
        }

        if (collision.gameObject.CompareTag("Shuriken")) {
            DyingPlayer();
        }
    }



    IEnumerator CooldownToAnimate() {
        yield return new WaitForSeconds(0.15f);
        _playerAnim.SetTrigger("PassingToNextPhase");
    }

    IEnumerator PlayerEmergingSound()
    {
        yield return new WaitForSeconds(0.5f);
        AudioManager.instance.GetSoundEffect(7);
    }

}
