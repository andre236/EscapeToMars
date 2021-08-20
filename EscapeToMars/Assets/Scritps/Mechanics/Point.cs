using Managers;
using UnityEngine;

public class Point : MonoBehaviour {

    private GameObject _particlesPoints;
    private GameObject _lighGO;
    private BoxCollider2D _currentBoxCollider;
    private SpriteRenderer _currentSprite;

    private void Awake() {
        _currentBoxCollider = GetComponent<BoxCollider2D>();
        _currentSprite = GetComponent<SpriteRenderer>();
        _particlesPoints = transform.Find("ParticleSystem").gameObject;
        _lighGO = transform.Find("PointLight2D").gameObject;
    }

    void Start() {
        _particlesPoints.SetActive(false);
        transform.position = new Vector2(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.y));
    }

    private void OnEnable()
    {
        _particlesPoints.SetActive(false);
        transform.position = new Vector2(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.y));
    }

    void DisableComponents() {
        _currentBoxCollider.enabled = false;
        _currentSprite.enabled = false;
        _lighGO.SetActive(false);
    }

    void DeactivatePoint()
    {
        DisableComponents();
        _particlesPoints.SetActive(true);
        ScoreManager.instance.IncreasePointsPlayer();
        Invoke("DeactivateParticles", 0.5f);
        if (gameObject.CompareTag("Point"))
        {
            AudioManager.Instance.PlaySoundEffect(1);
            GameManager.Instance.TriggerSecondStepFlag();
        }
        else if (gameObject.CompareTag("PointB"))
        {
            AudioManager.Instance.PlaySoundEffect(1, 1.2f);
            GameManager.Instance.LastStep();
        }
        else
        {
            AudioManager.Instance.PlaySoundEffect(1, 1.5f);
        }
        Invoke("DeactivateGameObject", 1f);
    }

    void DeactivateGameObject()
    {
        gameObject.SetActive(false);
    }

    void DeactivateParticles()
    {
        _particlesPoints.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("Player")) {
            DeactivatePoint();
        }
    }
}
