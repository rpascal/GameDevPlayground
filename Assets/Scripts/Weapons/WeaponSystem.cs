using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Weapons {
    public class WeaponSystem : MonoBehaviour {
        [SerializeField] WeaponBase _startingWeaponPrefab = null;
        [SerializeField] WeaponBase _slot01WeaponPrefab = null;
        [SerializeField] WeaponBase _slot02WeaponPrefab = null;
        // weapon socket helps us position our weapon and graphics
        [SerializeField] Transform _weaponSocket = null;

        // our weapon will use the STRATEGY PATTERN
        // each new weapon will have its own behavior!
        public WeaponBase EquippedWeapon { get; private set; }

        private void Awake() {
            if (_startingWeaponPrefab != null)
                EquipWeapon(_startingWeaponPrefab);
        }

        private void Update() {
            // press 1
            if (Input.GetKeyDown(KeyCode.Alpha1)) {
                EquipWeapon(_slot01WeaponPrefab);
            }

            // press 2
            if (Input.GetKeyDown(KeyCode.Alpha2)) {
                EquipWeapon(_slot02WeaponPrefab);
            }

            // press Space
            if (Input.GetKey(KeyCode.Mouse0) || Input.GetKey(KeyCode.Space)) {
                ShootWeapon();
            }
        }

        public void EquipWeapon(WeaponBase newWeapon) {
            if (EquippedWeapon != null) {
                Destroy(EquippedWeapon.gameObject);
            }

            // spawn weapon in the world and hold a reference
            EquippedWeapon = Instantiate
                (newWeapon, _weaponSocket.position, _weaponSocket.rotation);
            // make sure to include it in the player GameObject so it follows
            EquippedWeapon.transform.SetParent(_weaponSocket);
        }

        public void ShootWeapon() {
            // no matter what weapon we have, do its own Shoot()
            EquippedWeapon.Shoot();
        }
    }
}