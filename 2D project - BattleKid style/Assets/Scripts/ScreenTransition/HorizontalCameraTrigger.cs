using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalCameraTrigger : MonoBehaviour
{
    public Transform cameraTransform;
    float cameraMovementX = 17.7f;

    float playerPosX;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        var playerObject = GameObject.Find("Player");
        Collider2D playerCollider = playerObject.GetComponent<Collider2D>();
        if (other == playerCollider)
        {
            playerPosX = playerObject.transform.position.x;

            if (playerPosX > transform.position.x)
            {
                cameraTransform.position = new Vector3(cameraTransform.position.x - cameraMovementX, cameraTransform.position.y, cameraTransform.position.z);
                GameMaster.instance.UpdateXRoom(-1);
            }
            else
            {
                cameraTransform.position = new Vector3(cameraTransform.position.x + cameraMovementX, cameraTransform.position.y, cameraTransform.position.z);
                GameMaster.instance.UpdateXRoom(1);
            }
        }
    }
}
