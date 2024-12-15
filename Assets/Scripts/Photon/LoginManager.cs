using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;
public class LoginManager : MonoBehaviourPunCallbacks
{
    public TMP_InputField PlayerName_InputName;
    public GameObject loginUI;
    public GameObject roomUI;

    #region Unity Methods
    // Start is called before the first frame update
    void Start()
    {
        //Connect to server immmeidately upon logging in - testing only 
        // PhotonNetwork.ConnectUsingSettings();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    #endregion

    #region UI Callback Methods
    public void ConnectAnonymously()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    public void ConnectToPhotonServer()
    {
        if (PlayerName_InputName !=null)
        {
            PhotonNetwork.NickName = PlayerName_InputName.text;
            PhotonNetwork.ConnectUsingSettings();
        }
    }

        public void GoBack (){
        GoToLoginScene();
    }

    private void GoToRoomScene () {
        roomUI.SetActive(true);
        loginUI.SetActive(false);
    }

    private void GoToLoginScene () {
        roomUI.SetActive(false);
        loginUI.SetActive(true);
    }
    
    #endregion


    #region Photon Callback Methods
    public override void OnConnected()
    {
        Debug.Log("OnConnected is called. The server is available!");
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to Master Server with player name: "+PhotonNetwork.NickName);

        GoToRoomScene();
        // PhotonNetwork.LoadLevel("HomeScene");
    }


    #endregion
}
