using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    [SerializeField] private float thrustSpeed = 3.0f;
    [SerializeField] private float turnSpeed = 0.20f;

    private bool _thrusting;

    public bool Thrusting {
        get {
            return _thrusting;
        }
    }

    private float _turnDirection;
    public float TurnDirection {
        get {
            return _turnDirection;
        }

        set {
            if (value < 0) {
                _turnDirection = 1.0f;
            } else if (value > 0) {
                _turnDirection = -1.0f;
            } else {
                _turnDirection = 0.0f;
            }
        }
    }

    private Rigidbody2D _rigidBody;


    private void Awake() {
        _rigidBody = GetComponent<Rigidbody2D>();
    }

    private void Start() {
    }

    // Update is called once per frame
    private void Update() {
        //_thrusting = Input.GetAxis("Vertical") > 0.0f; // Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow);
        //var horizontalAxis = Input.GetAxis("Horizontal");
        //if (horizontalAxis < 0) {
        //    _turnDirection = 1.0f;
        //} else if (horizontalAxis > 0) {
        //    _turnDirection = -1.0f;
        //} else {
        //    _turnDirection = 0.0f;
        //}
    }

    private void FixedUpdate() {
        if (_thrusting) {
            _rigidBody.AddForce(this.transform.up * this.thrustSpeed);
        }

        if (_turnDirection != 0.0f) {
            _rigidBody.AddTorque(_turnDirection * this.turnSpeed);
        }
    }

    public void setThrusting(float value) {
        _thrusting = value > 0.0f;
    }


    public void doThrust(float value) {
        if (value > 0.0f) {
            _rigidBody.AddForce(this.transform.up * this.thrustSpeed);
        }
    }

    public void doTurn(float value) {
        if (value < 0) {
            _rigidBody.AddTorque(this.turnSpeed);
        } else if (value > 0) {
            _rigidBody.AddTorque(-this.turnSpeed);
        }
    }



    //private void OnCollisionEnter2D(Collision2D collision) {

    //    if (collision.gameObject.tag == "Asteriod") {
    //        _rigidBody.velocity = Vector3.zero;
    //        _rigidBody.angularVelocity = 0.0f;
    //        this.gameObject.SetActive(false);
    //        FindObjectOfType<GameManager>().PlayerDied();
    //    }

    //}
    //Asteriod

}
