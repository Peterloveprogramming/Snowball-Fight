using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConnectToPhotonOnLoad : MonoBehaviour
{
    public LoginManager loginManager;
    public RoomManager roomManager;

    // Start is called before the first frame update
    void Start()
    {
        loginManager.ConnectAnonymously();
        roomManager.JoinRandomRoom();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
