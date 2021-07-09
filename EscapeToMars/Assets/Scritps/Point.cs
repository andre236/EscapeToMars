using UnityEngine;

public class Point : MonoBehaviour {

    private GameObject _particlesPoints;
    private GameObject _lighGO;

    private Animator _currentAnim;
    private BoxCollider2D _currentBoxCollider;
    private SpriteRenderer _currentSprite;

    private void Awake() {
        _currentAnim = GetComponent<Animator>();
        _currentBoxCollider = GetComponent<BoxCollider2D>();
        _currentSprite = GetComponent<SpriteRenderer>();
        _particlesPoints = transform.Find("ParticleSystem").gameObject;
        _lighGO = transform.Find("PointLight2D").gameObject;
    }

    void Start() {

        _particlesPoints.SetActive(false);
        transform.position = new Vector2(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.y));
    }
    
    void ToDestroy() {
        Destroy(_currentBoxCollider);
        Destroy(_currentSprite);
        Destroy(_lighGO);
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("Player") && gameObject.CompareTag("Point")) {
            ToDestroy();
            _particlesPoints.SetActive(true);
            Destroy(_particlesPoints, 0.5f);
            AudioManager.Instance.PlaySoundEffect(1);
            ScoreManager.instance.IncreasePointsPlayer();
            
            GameManager.instance.TriggerFlag();
            Destroy(gameObject, 1f);
        }

        if (collision.gameObject.CompareTag("Player") && gameObject.CompareTag("PointB")) {
            ToDestroy();
            _particlesPoints.SetActive(true);
            Destroy(_particlesPoints, 0.5f);
            AudioManager.Instance.PlaySoundEffect(1, 1.2f);
            ScoreManager.instance.IncreasePointsPlayer();
            GameManager.instance.SecondStar();
            Destroy(gameObject, 1f);
        }
        if (collision.gameObject.CompareTag("Player") && gameObject.CompareTag("PointC")) {
            ToDestroy();
            _particlesPoints.SetActive(true);
            Destroy(_particlesPoints, 0.5f);
            AudioManager.Instance.PlaySoundEffect(1, 1.5f);
            ScoreManager.instance.IncreasePointsPlayer();
            Destroy(gameObject, 1f);
        }

    }
}
