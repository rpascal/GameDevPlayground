using UnityEngine;

public class Player : MonoBehaviour {

    public float thrustSpeed = 1.0f;
    public float turnSpeed = 1.0f;

    public float rotationSpeed = 5.0f;

    private bool _thrusting;
    private float _turnDirection;
    private Rigidbody2D _rigidBody;


    private void Awake() {
        _rigidBody = GetComponent<Rigidbody2D>();
    }

    private void Start() {
        _rigidBody.freezeRotation = true;
    }

    // Update is called once per frame
    private void Update() {
        _thrusting = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow);

        bool left = Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow);
        bool right = Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow);

        if (left) {
            _turnDirection = 1.0f;
        } else if (right) {
            _turnDirection = -1.0f;
        } else {
            _turnDirection = 0.0f;
        }

    }

    private void FixedUpdate() {


        if (_thrusting) {
            _rigidBody.AddForce(this.transform.up * this.thrustSpeed);
        }

        //if (_turnDirection != 0.0f) {
        //    _rigidBody.AddTorque(_turnDirection * this.turnSpeed);
        //}

    }
    private void OnCollisionEnter2D(Collision2D collision) {

        if (collision.gameObject.tag == "Asteriod") {
            _rigidBody.velocity = Vector3.zero;
            _rigidBody.angularVelocity = 0.0f;
            this.gameObject.SetActive(false);
            FindObjectOfType<GameManager>().PlayerDied();
        }

    }

}
