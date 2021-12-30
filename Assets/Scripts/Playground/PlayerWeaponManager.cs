using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponManager : MonoBehaviour {

    [SerializeField] private PlayerWeapon[] weapons;
    private int currentWeaponIndex = 0;
    private int totalWeapons;
    private int maxIndex;

    void Start() {
        foreach (PlayerWeapon weapon in weapons) {
            weapon.gameObject.SetActive(false);
        }
        weapons[0].gameObject.SetActive(true);
        totalWeapons = weapons.Length;
        maxIndex = totalWeapons - 1;
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(KeyCode.E)) {
            if (currentWeaponIndex == maxIndex) {
                updateWeapon(maxIndex, 0);
            } else if (currentWeaponIndex < maxIndex) {
                updateWeapon(currentWeaponIndex, currentWeaponIndex + 1);
            }
        }

        if (Input.GetKeyDown(KeyCode.Q)) {
            if (currentWeaponIndex == 0) {
                updateWeapon(0, maxIndex);
            } else if (currentWeaponIndex > 0) {
                updateWeapon(currentWeaponIndex, currentWeaponIndex - 1);
            }
        }
    }

    private void updateWeapon(int oldIndex, int newIndex) {
        weapons[oldIndex].gameObject.SetActive(false);
        currentWeaponIndex = newIndex;
        weapons[newIndex].gameObject.SetActive(true);
    }
}
