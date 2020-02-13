using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadZoneController : MonoBehaviour
{
    public GameController gameController;

    private void Start()
    {
        gameController = GameObject.Find("GlobalScriptsText").GetComponent<GameController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        gameController.DecrementLife();
        Destroy(other.gameObject);
    }
}
