using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour
{
    public static GameMaster instance;
    
    Vector3 playerRespawnPosition;
    Vector3 cameraRespawnPosition;

    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
    }


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayerRespawnUpdate(Vector3 playerPosition)
    {
        playerRespawnPosition = playerPosition;
    }

    public void CameraRespawnUpdate(Vector3 cameraPosition)
    {
        cameraRespawnPosition = cameraPosition;
    }

    public Vector3 ReturnPlayerPosition()
    {
        return playerRespawnPosition;
    }

    public Vector3 ReturnCameraPosition()
    {
        return cameraRespawnPosition;
    }
}
