using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameMaster : MonoBehaviour
{
    public static GameMaster instance;
    
    Vector3 playerRespawnPosition;
    Vector3 cameraRespawnPosition;

    int xRoom, yRoom;
    
    int deathcounter;
    public TextMeshProUGUI deathCounterText;
    
    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
    }


    void Start()
    {
        resetDeathCounter();

        
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
}
