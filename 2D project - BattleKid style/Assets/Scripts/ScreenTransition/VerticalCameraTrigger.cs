using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalCameraTrigger : MonoBehaviour
{
    public Transform cameraTransform;
    float cameraMovementY = 10f;

    float playerPosY;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(transform.position.y);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        var playerObject = GameObject.Find("Player");
        Collider2D playerCollider = playerObject.GetComponent<Collider2D>();
        if (other == playerCollider)
        {
            playerPosY = playerObject.transform.position.y;

            if (playerPosY > transform.position.y)
            {
                cameraTransform.position = new Vector3(cameraTransform.position.x, cameraTransform.position.y - cameraMovementY, cameraTransform.position.z);
                GameMaster.instance.UpdateYRoom(1);
            }
            else
            {
                cameraTransform.position = new Vector3(cameraTransform.position.x, cameraTransform.position.y + cameraMovementY, cameraTransform.position.z);
                GameMaster.instance.UpdateYRoom(-1);
            }
        }
    }
}
