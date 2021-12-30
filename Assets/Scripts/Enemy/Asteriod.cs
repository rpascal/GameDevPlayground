using Assets.Scripts.Layer;
using Assets.Scripts.Projectiles;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteriod : MonoBehaviour {

    public Sprite[] sprites;
    public float size = 1.0f;
    public float minSize = 0.5f;
    public float maxSize = 1.5f;
    public float speed = 50.0f;
    public float maxLifetime = 30.0f;

    private SpriteRenderer _spriteRenderer;
    private Rigidbody2D _rigidBody;

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

    private void OnCollisionEnter2D(Collision2D collision) {
        if (Tags.hasTag(collision, Tags.ProjectileTag, Tags.BlueProjectileTag, Tags.RedProjectileTag, Tags.PurpleProjectileTag)) {
            if (this.size * 0.5f > this.minSize) {
                // This kinda sucks but need to spawn clones outside of this
                FindObjectOfType<AsteriodSpawner>().SpawnSplits(this.transform, size, speed, 2);
            }

            FindObjectOfType<GameManager>().AsteriodDestroyed(this);
            Destroy(this.gameObject);
        }
    }


}
