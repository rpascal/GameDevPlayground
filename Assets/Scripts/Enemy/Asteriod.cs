using Assets.Scripts.Damage;
using Assets.Scripts.Layer;
using Assets.Scripts.Projectiles;
using Assets.Scripts.Score;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteriod : MonoBehaviour, IDamageable {

    public Sprite[] sprites;
    public float minSize = 0.5f;
    public float maxSize = 1.5f;
    public float speed = 25.0f;
    public float maxLifetime = 30.0f;
    float _maxHealth = 5;
    public float maxHealth => _maxHealth;

    private float _health = 5;
    public float health => _health;

    public float size = 1.0f;

    private SpriteRenderer _spriteRenderer;
    private Rigidbody2D _rigidBody;


    [SerializeField] private EntityDamageType _type = EntityDamageType.Normal;
    public EntityDamageType type => _type;

    private void Awake() {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _rigidBody = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start() {
        _spriteRenderer.sprite = sprites[Random.Range(0, sprites.Length)];

        this.transform.eulerAngles = new Vector3(0.0f, 0.0f, Random.value * 360.0f);
        this.transform.localScale = Vector3.one * this.size;

        _rigidBody.mass = this.size;
    }

    public void setTrajectory(Vector2 direction) {
        _rigidBody.AddForce(direction * this.speed);
        Destroy(this.gameObject, this.maxLifetime);
    }

    public void Damage(float amount) {
        _health -= amount;
        if (health <= 0) {
            if (this.size * 0.5f > this.minSize) {
                // This kinda sucks but need to spawn clones outside of this
                FindObjectOfType<AsteriodSpawner>().SpawnSplits(this, size, speed, 2);
            }
            int basePoints = calculatePoints();
            int totalPoints = basePoints * EntityDamageTypeScoreMultipliers.scoreMulitplerForType(this.type);
            // TODO find better way to get ScoreManager
            FindObjectOfType<ScoreManager>().addPoints(totalPoints);
            Destroy(this.gameObject);
        }
    }

    private int calculatePoints() {
        if (size < 0.75f) {
            return 10;
        } else if (size < 1.2f) {
            return 5;
        } else {
            return 1;
        }
    }

}
