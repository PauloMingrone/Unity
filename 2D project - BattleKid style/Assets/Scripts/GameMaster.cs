using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameMaster : MonoBehaviour
{
    public static GameMaster instance;
    
    Vector3 playerRespawnPosition;
    Vector3 cameraRespawnPosition;

    int xRoom, yRoom, xRespawn, yRespawn;
    
    int deathcounter;
    public TextMeshProUGUI deathCounterText;
    public TextMeshProUGUI mapText;
    
    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
    }


    void Start()
    {
        resetDeathCounter();
        ResetMap();

        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Respawn functions
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


    //DeathCounter functions
    public void resetDeathCounter()
    {
        deathcounter = 0;
        deathCounterText.text = deathcounter.ToString();
    }
    public void increaseDeath()
    {
        deathcounter++;
        deathCounterText.text = deathcounter.ToString();
    }


    //Map functions
    public void ResetMap()
    {
        xRoom = 1;
        yRoom = 1;
        UpdateMap();
            

    }

    public void UpdateXRoom(int value)
    {
        xRoom += value;
        UpdateMap();
    }

    public void UpdateYRoom(int value)
    {
        yRoom += value;
        UpdateMap();
    }

    void UpdateMap()
    {
        mapText.text = string.Concat(xRoom.ToString() + " / " + yRoom.ToString());
    }
    
    public void MapRespawnUpdate() 
    {
        xRespawn = xRoom; 
        yRespawn = yRoom;
    }

    public void MapRespawn()
    {
        xRoom = xRespawn;
        yRoom = yRespawn;
        UpdateMap();

    }
}
