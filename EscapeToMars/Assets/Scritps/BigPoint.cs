using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigPoint : MonoBehaviour {
    
    private GameObject _pointLight;
    private GameObject _particlesGO;
    private GameObject _particlesGOB;

    private Animator _currentAnim;

    private BoxCollider2D _currentBoxColl2d;
    private SpriteRenderer _currentSprite;

    private Player _player;

    private void Awake() {
        ReferenceGameObjects();
    }

    private void Start() {
        _particlesGOB.SetActive(false);
    }

    void ReferenceGameObjects() {
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        _currentBoxColl2d = GetComponent<BoxCollider2D>();
        _currentAnim = GetComponent<Animator>();
        _currentSprite = GetComponent<SpriteRenderer>();
        
        _pointLight = transform.Find("Point Light 2D").gameObject;
        _particlesGO = transform.Find("Particle System").gameObject;
        _particlesGOB = transform.Find("ParticleSystem").gameObject;

    }

    void ToDestroy() {
        Destroy(_currentBoxColl2d);
        Destroy(_currentSprite);
        Destroy(_pointLight);
        Destroy(_particlesGO);

    }

    private void OnTriggerEnter2D(Collider2D collision) {

        if (collision.gameObject.CompareTag("Player")) {
            ToDestroy();
            _particlesGOB.SetActive(true);
            Destroy(_particlesGOB, 0.5f);
            AudioManager.instance.GetSoundEffect(0);
            _player.GettingStronger(true);
            Destroy(gameObject, 1f);

        }

    }
}
