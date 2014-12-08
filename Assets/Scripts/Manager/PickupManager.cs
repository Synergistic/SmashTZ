using UnityEngine;
using System.Collections.Generic;

public class PickupManager : MonoBehaviour {


    public List<GameObject> possiblePickups;
    public float ChanceToSpawn;


    private Vector3 nextLocation;
    private GameObject parentObject;

    void Awake()
    {
        parentObject = GameObject.Find("Pickups");
    }

    public void RollForItem(Vector3 location)
    {
        float roll = Random.value;
        if (roll > ChanceToSpawn)
        {
            return;
        }
        else if (roll < ChanceToSpawn)
        {
            nextLocation = new Vector3(location.x, 1f, location.z);
            Invoke("SpawnItem", 1f);
        }
    }

    public void RemoveItemByName(string name)
    {
        for (int i = 0; i < possiblePickups.Count; i++)
        {
            if (possiblePickups[i].name == name)
            {
                possiblePickups.RemoveAt(i);
            }
        }
    }


    void SpawnItem()
    {
        GameObject pickupObj = Instantiate(possiblePickups[Random.Range(0, possiblePickups.Count)], nextLocation, Quaternion.identity) as GameObject;
        pickupObj.transform.parent = parentObject.transform;
    }


}
