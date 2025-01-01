using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/* 武器库 */
public class WeaponsLibrary : MonoBehaviour
{
    public List<Weapons> weaponsList;
    public Dictionary<Weapons, int> playerWeaponsLibrary = new Dictionary<Weapons, int>();
    void Awake()
    {
        ListAddToDictionary();
    }
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void ListAddToDictionary()
    {
        int index = 0;
        foreach (Weapons weapon in weaponsList)
        {
            playerWeaponsLibrary.Add(weapon, index++);
        }
    }
}
