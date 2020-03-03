using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    const float _MINX = -8, _MAXX = 8;
    float _speedX = 20f;
    Vector3 deltaPos;
    GameController gameController;

    private void Start()
    {
        gameController = GameObject.Find("GlobalScriptsText").GetComponent<GameController>();
    }

    // Update is called once per frame
    void Update()
    {
        deltaPos = new Vector3(Input.GetAxis("Horizontal"), 0) * _speedX * Time.deltaTime;
        gameObject.transform.Translate(deltaPos);

        gameObject.transform.position = new Vector3(
            Mathf.Clamp(gameObject.transform.position.x, _MINX, _MAXX),
            gameObject.transform.position.y,
            gameObject.transform.position.z);
    }

    private void OnTriggerEnter(Collider other)
    {
        gameController.IncrementScore();
        Destroy(other.gameObject);
        AudioManager.Instance.PlaySoundEffect(AudioManager.SoundEffect.Capture);
    }
}
