using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveStation : MonoBehaviour
{
    SpriteRenderer objectSR;
    Vector3 objectPosition;
    bool SaveStationOn; 

    // Start is called before the first frame update
    void Start()
    {
        objectSR = GetComponent<SpriteRenderer>();
        objectPosition = transform.position;    
        SaveStationOn = false;

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && !SaveStationOn)
        {
            objectSR.color = Color.green;
            AudioManager.instance.PlaySFX(1);
            SaveStationOn = true;

            //update player respawn position on GameMaster
            GameMaster.instance.PlayerRespawnUpdate(objectPosition);
            GameMaster.instance.CameraRespawnUpdate(Camera.main.transform.position);
           
        }
    }

    void OnBecameInvisible()
    {
        SaveStationOn = false;
        objectSR.color = Color.white;
    }
}
