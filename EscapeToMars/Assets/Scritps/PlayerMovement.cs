using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{

    private Button _upButton, _downButton, _rightButton, _leftButton;

    private Rigidbody2D _playerRB;
    private BoxCollider2D _playerBoxColl;
    private Transform _initialPosition;

    [SerializeField]
    private LayerMask _permissionLayersMovement;

    private Vector2 _movementVector;
    private Vector2 _rbPlayerPositionRoundX;
    private Vector2 _rbPlayerPositionRoundY;
    [SerializeField]
    private Vector2 _raycastOffset;

    private Player _player;


    public int MovementSpeed { get; private set; } = 5;

    public bool IsMovingByArrow { get; private set; } = false;
    public bool IsTeleporting { get; private set; } = false;
    public bool RequestToMoveUp { get; private set; }
    public bool RequestToMoveDown { get; private set; }
    public bool RequestToMoveLeft { get; private set; }
    public bool RequestToMoveRight { get; private set; }
    public bool CantMove { get; private set; } = false;


    private void Awake()
    {
        _player = GetComponent<Player>();
        _playerBoxColl = GetComponent<BoxCollider2D>();
        _playerRB = GetComponent<Rigidbody2D>();
        _initialPosition = GameObject.Find("StartPositionPlayer").GetComponent<Transform>();
    }

    void Start()
    {
        transform.position = _initialPosition.transform.position;
        transform.position = new Vector2(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.y));

    }

    void Update()
    {
        bool CanMove = !_player.IsDead && !GameManager.instance.isLoadingStep && !GameManager.instance.Win && GameManager.instance.GameStarted && !IsMovingByArrow && !IsTeleporting && !CantMove;

        if (CanMove)
        {
            ControlToMove();
        }
    }

    void FixedUpdate()
    {
        if (!_player.IsDead && !GameManager.instance.isLoadingStep && !GameManager.instance.Win && GameManager.instance.GameStarted && !IsTeleporting)
        {
            ToMove();
        }

        if (GameManager.instance.Win)
        {
            MoveToPortal();
        }




    }

    void ToMove()
    {
        _rbPlayerPositionRoundX = new Vector3(Mathf.Round(_playerRB.position.x), _playerRB.position.y);
        _rbPlayerPositionRoundY = new Vector3(_playerRB.position.x, Mathf.Round(_playerRB.position.y));

        _playerRB.MovePosition(_playerRB.position + _movementVector * MovementSpeed * Time.fixedDeltaTime);

    }

    public void MoveToPortal()
    {
        GameObject Flag = GameObject.FindGameObjectWithTag("Flag");
        transform.position = Vector2.MoveTowards(gameObject.transform.position, Flag.transform.position, 2 * Time.fixedDeltaTime);
    }

    public void MoveToTelep()
    {

    }

    public void MoveUpPlayer()
    {
        RequestToMoveUp = true;
        RequestToMoveDown = false;
        RequestToMoveRight = false;
        RequestToMoveLeft = false;
    }



    public void MoveDownPlayer()
    {
        RequestToMoveUp = false;
        RequestToMoveDown = true;
        RequestToMoveRight = false;
        RequestToMoveLeft = false;
    }

    public void MoveRightPlayer()
    {
        RequestToMoveUp = false;
        RequestToMoveDown = false;
        RequestToMoveRight = true;
        RequestToMoveLeft = false;
    }

    public void MoveLeftPlayer()
    {
        RequestToMoveUp = false;
        RequestToMoveDown = false;
        RequestToMoveRight = false;
        RequestToMoveLeft = true;
    }

    public void ControlToMove()
    {

        var xOrig = transform.position.x + _raycastOffset.x;
        var yOrig = transform.position.y + _raycastOffset.y;

        var rayCastUpA = Physics2D.Raycast(new Vector2(transform.position.x + -0.25f, transform.position.y), Vector2.up, 0.6f, _permissionLayersMovement);
        var rayCastUpB = Physics2D.Raycast(new Vector2(transform.position.x + 0.25f, transform.position.y), Vector2.up, 0.6f, _permissionLayersMovement);

        var rayCastDownA = Physics2D.Raycast(new Vector2(transform.position.x + -0.25f, transform.position.y), Vector2.down, 0.6f, _permissionLayersMovement);
        var rayCastDownB = Physics2D.Raycast(new Vector2(transform.position.x + 0.25f, transform.position.y), Vector2.down, 0.6f, _permissionLayersMovement);

        var rayCastRightA = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y + 0.16f), Vector2.right, 0.6f, _permissionLayersMovement);
        var rayCastRightB = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y + -0.28f), Vector2.right, 0.6f, _permissionLayersMovement);

        var rayCastLeftA = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y + 0.16f), Vector2.left, 0.6f, _permissionLayersMovement);
        var rayCastLeftB = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y + -0.28f), Vector2.left, 0.6f, _permissionLayersMovement);

        bool ConditionsPlayerToMove = !_player.IsDead && !GameManager.instance.isLoadingStep;


        if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S) && ConditionsPlayerToMove)
        {
            RequestToMoveDown = true;
            RequestToMoveUp = false;
            RequestToMoveRight = false;
            RequestToMoveLeft = false;
        }

        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W) && ConditionsPlayerToMove)
        {
            RequestToMoveUp = true;
            RequestToMoveDown = false;
            RequestToMoveRight = false;
            RequestToMoveLeft = false;
        }

        if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D) && ConditionsPlayerToMove)
        {
            RequestToMoveRight = true;
            RequestToMoveUp = false;
            RequestToMoveDown = false;
            RequestToMoveLeft = false;
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A) && ConditionsPlayerToMove)
        {
            RequestToMoveLeft = true;
            RequestToMoveRight = false;
            RequestToMoveUp = false;
            RequestToMoveDown = false;
        }


        if (RequestToMoveDown && rayCastDownA.collider == null && rayCastDownB.collider == null)
        {
            MoveToDirection("down");

            RequestToMoveDown = false;
        }

        if (RequestToMoveUp && rayCastUpA.collider == null && rayCastUpB.collider == null)
        {
            MoveToDirection("up");

            RequestToMoveUp = false;
        }

        if (RequestToMoveLeft && rayCastLeftA.collider == null && rayCastLeftB.collider == null)
        {
            MoveToDirection("left");

            RequestToMoveLeft = false;
        }

        if (RequestToMoveRight && rayCastRightA.collider == null && rayCastRightB.collider == null)
        {
            MoveToDirection("right");

            RequestToMoveRight = false;
        }


    }

    public void MoveToDirection(string direction)
    {


        switch (direction)
        {
            case "up":
                _movementVector.y = 1;
                _movementVector.x = 0;
                _playerRB.position = _rbPlayerPositionRoundX;
                break;
            case "down":
                _movementVector.y = -1;
                _movementVector.x = 0;
                _playerRB.position = _rbPlayerPositionRoundX;
                break;
            case "right":
                _movementVector.x = 1;
                _movementVector.y = 0;
                _playerRB.position = _rbPlayerPositionRoundY;
                break;
            case "left":
                _movementVector.x = -1;
                _movementVector.y = 0;
                _playerRB.position = _rbPlayerPositionRoundY;
                break;
        }



    }

    public void GettingArrow(bool onArrow)
    {
        IsMovingByArrow = onArrow;
    }

    public void MoveToDestinyWithPortal(Transform destiny)
    {
        _movementVector.y = 0;
        _movementVector.x = 0;
        transform.position = new Vector2(destiny.transform.position.x, destiny.transform.position.y);

    }





    public void GettingPortal(bool onPortal)
    {
        IsTeleporting = onPortal;
        if (IsTeleporting)
        {
            RequestToMoveRight = false;
            RequestToMoveUp = false;
            RequestToMoveDown = false;
            RequestToMoveLeft = false;


        }
    }

    public void TurnOFFcontrol(bool cantMove)
    {
        CantMove = cantMove;
    }
}
