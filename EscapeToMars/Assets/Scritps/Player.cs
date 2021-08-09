using System.Collections;
using UnityEngine;


public class Player : MonoBehaviour
{
    private GameObject _eletricityGO;
    private Animator _playerAnim;

    public int TotalKills { get; private set; }
    public bool Stronger { get; private set; } = false;
    public bool IsDead { get; private set; } = false;

    void Awake()
    {
        ReferenceGameObjects();
    }

    private void Start()
    {
        StartCoroutine(PlayerEmergingSound());
    }


    void ReferenceGameObjects()
    {
        _playerAnim = GetComponent<Animator>();
        _eletricityGO = transform.Find("Eletricity").gameObject;
        _eletricityGO.SetActive(Stronger);
    }

    public void CountKill()
    {
        PlayerPrefs.SetInt("Kills", PlayerPrefs.GetInt("Kills") + 1);
    }

    public bool GettingStronger(bool state)
    {
        if (state)
        {
            _eletricityGO.SetActive(true);
        }
        else
        {
            _eletricityGO.SetActive(false);
        }

        return Stronger = state;
    }

    public void DyingPlayer()
    {
        IsDead = true;
        _playerAnim.SetBool("isDying", true);
        Destroy(GetComponent<BoxCollider2D>());
        AudioManager.Instance.PlaySoundEffect(2);
        GameManager.Instance.GameOver();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Flag"))
        {
            Destroy(GetComponent<BoxCollider2D>());
            StartCoroutine("CooldownToAnimate");
            AudioManager.Instance.PlaySoundEffect(3);
        }

        if (collision.gameObject.CompareTag("Cyclop") && !Stronger)
        {
            //DyingPlayer();
        }

        if (collision.gameObject.CompareTag("Shuriken"))
        {
            DyingPlayer();
        }
    }



    IEnumerator CooldownToAnimate()
    {
        yield return new WaitForSeconds(0.15f);
        _playerAnim.SetTrigger("PassingToNextPhase");
    }

    IEnumerator PlayerEmergingSound()
    {
        yield return new WaitForSeconds(0.5f);
        AudioManager.Instance.PlaySoundEffect(7);
    }

}
