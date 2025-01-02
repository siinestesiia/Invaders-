using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject playerObject;
    new Vector3 initialPosition = new Vector3(0, 0, -10);

    void Start()
    {
        InstantiateObject(playerObject, .5f);
    }

    // Instantiation System ------------------------------------------------------------
    public void InstantiateObject(GameObject gameObject, float delay)
    {
        gameObject.transform.position = initialPosition;
        StartCoroutine(DelayedInstantiation(gameObject, delay));
    }

    IEnumerator DelayedInstantiation(GameObject gameObject, float delay)
    {
        yield return new WaitForSecondsRealtime(delay);
        Instantiate(gameObject);
        Debug.Log($"{gameObject.name} has been instantiated at coordinates {initialPosition}.");
    }

}
