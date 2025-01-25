using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject playerObject;
    [SerializeField] GameObject triggerObject;

    Vector3 initialPosition = new Vector3(0, 0, -10);

    [SerializeField] bool cursorIsVisible = true;

    void Start()
    {
        InstantiateObject(playerObject, .5f, initialPosition);
        InstantiateObject(triggerObject, .5f, new Vector3(0f, 0f, 19f));
        
        Cursor.visible = cursorIsVisible;
    }

    // Instantiation System ------------------------------------------------------------
    public void InstantiateObject(GameObject gameObject, float delay, Vector3 initPosition)
    {
        if (gameObject != null)
        {
            gameObject.transform.position = initPosition;
            StartCoroutine(DelayedInstantiation(gameObject, delay));
        }
        else
        {
            Debug.Log("Unable to find game object, please check reference.");
        }    
    }

    IEnumerator DelayedInstantiation(GameObject gameObject, float delay)
    {
        yield return new WaitForSecondsRealtime(delay);
        Instantiate(gameObject);
    }

}
