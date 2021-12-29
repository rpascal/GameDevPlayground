using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour {
    public Bullet bulletPrefab;
    public Transform firePoint;

    [SerializeField] float bulletSpeed = 500f;
    [SerializeField] float fireRate = 1f;

    private float nextFire = 0f;


    private void Update() {
        bool shouldShoot = Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0);

        if (shouldShoot || Input.GetKey(KeyCode.Space)) {
            Shoot();
        }
    }

    private void Shoot() {
        if (Time.time > nextFire) {
            nextFire = Time.time + fireRate;
            Bullet bullet = Instantiate(this.bulletPrefab, this.firePoint.position, this.firePoint.rotation);
            bullet.Project(this.firePoint.up, bulletSpeed);
        }
    }

}
