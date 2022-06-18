using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    public Transform TheSpell;
    public GameObject TheSpellPrefab;
    //float timer = 0;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
                Shoot();

        }
    }
    void Shoot()
    {
        Instantiate(TheSpellPrefab, TheSpell.position, TheSpell.rotation);    
    }
}
