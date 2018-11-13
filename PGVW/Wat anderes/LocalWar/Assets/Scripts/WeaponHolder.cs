using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class WeaponHolder : MonoBehaviour {

    public enum Hand
    {
        LEFT,
        RIGHT
    }

    public List<Weapon> weapons = new List<Weapon>();
    public Weapon selectedWeapon;

    public Transform weaponPositionRight;
    public Transform weaponPositionLeft;
    public Hand mainHand = Hand.RIGHT;
    

    private int weaponCount = 0;
    
	// Use this for initialization
	void Start () {
        //foreach(Weapon w in weapons)
        //{
        //    Instantiate(w);
        //    w.gameObject.transform.position = Vector3.zero;
        //    w.gameObject.SetActive(false);
        //}
        if (weapons.Count > 0)
            switchWeapon(0);

    }
	
	// Update is called once per frame
	void Update () {
        if (weapons.Count > 0)
        {
            if (Input.GetAxis("Mouse ScrollWheel") > 0f) // forward
            {
                weaponCount = Math.Min(weaponCount + 1, weapons.Count-1);
                switchWeapon(weaponCount);
            }
            else if (Input.GetAxis("Mouse ScrollWheel") < 0f) // backwards
            {
                weaponCount = Math.Max(weaponCount - 1, 0);
                switchWeapon(weaponCount);
            }
        }
    }

    private void FixedUpdate()
    {
        UpdateWeaponHandles();
    }

    private void switchWeapon(int weaponCount)
    {
        Weapon newWeapon = weapons[weaponCount];
        if (selectedWeapon != newWeapon)
        {
            if (selectedWeapon != null)
            {
                //selectedWeapon.gameObject.SetActive(false);
                Destroy(selectedWeapon.gameObject);
                selectedWeapon = null;
            }

            if (selectedWeapon == null)
            {
                selectedWeapon = newWeapon;
                selectedWeapon = Instantiate<Weapon>(selectedWeapon);
                //selectedWeapon.gameObject.SetActive(true);
                UpdateWeaponHandles();
            }
        }
    }

    private void UpdateWeaponHandles()
    {
        switch (mainHand)
        {
            case Hand.RIGHT:
                selectedWeapon.UpdatePosition(weaponPositionRight.position, weaponPositionLeft.position);
                break;
            case Hand.LEFT:
                selectedWeapon.UpdatePosition(weaponPositionLeft.position, weaponPositionRight.position);
                break;
        }
    }
}
