﻿using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameSetupScript : NetworkBehaviour {

	public GameObject eventSystem;
	public Color pink;
	public Color lightPink;
	public Color blue;
	public Color lightBlue;

	//public GameObject doneButton;
	//public GameObject deleteButton;
	public GameObject turnBar;
//	public Image colorBarOne;
//	public Image colorBarTwo;
	public Image colorBarWheelOne;
	public Image colorBarWheelTwo;
	public Image colorWheel;


	public bool bluePlayerHere = false;
	public TurnManagerScript turnMan;

	[SyncVar]
	public int sendData;

	[SyncVar]
	public int rpcData;

	public override void OnStartLocalPlayer(){
	
		int networkNum = (int)netId.Value;
		//Debug.Log (networkNum);

		eventSystem.SetActive (true);

		if (networkNum == 1) {
			
			gameObject.name = "Pink Game Manager";
			gameObject.tag = "Game Manager Local";
			//pink = new Color (1.0f, .8f, .808f, 1.0f);
			ButtonsToPink ();
			GameObject.FindGameObjectWithTag ("Camera One").SetActive (false);
			//GameObject.FindGameObjectWithTag ("Signal Circle Message").GetComponent<WheelFromBuddyScript> ().networkNum = 1;
			turnMan.myBackground = GameObject.FindGameObjectWithTag ("Color Spiral 1");


		}

		if (networkNum == 2) {

			gameObject.name = "Blue Game Manager";
			gameObject.tag = "Game Manager Local";
			//blue = new Color (.74f, .945f, 1.0f, 1.0f);
			ButtonsToBlue ();
			GameObject.FindGameObjectWithTag ("Camera Two").SetActive (false);
			//GameObject.FindGameObjectWithTag ("Signal Circle Message").GetComponent<WheelFromBuddyScript> ().networkNum = 2;
			turnMan.myBackground = GameObject.FindGameObjectWithTag ("Color Spiral 2");
			CmdBluePlayerHere ();

		}

		InvokeRepeating ("CmdSendData", 1.0f, 5.0f);

		if (!isServer) {
			return;
		}

		InvokeRepeating ("RpcSendData", .5f, 5.0f);
	
	
	}

	// Use this for initialization
	void Start () {


		if (!isLocalPlayer)
		{
			GameObject canvas = this.gameObject.transform.GetChild(0).gameObject;
			Destroy (canvas);

		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void ButtonsToPink () {
	
		GameObject[] buttons = GameObject.FindGameObjectsWithTag ("Program Button");
		int buttonCount = buttons.Length;

		for(int i = 0; i < buttonCount; i++)
		{
			buttons[i].GetComponent<Image>().color = pink;

		}

//		doneButton.GetComponent<Image>().color = pink;
//		deleteButton.GetComponent<Image>().color = pink;
		turnBar.GetComponent<Image>().color = pink;
//		colorBarOne.color = blue;
//		colorBarTwo.color = blue;
		colorWheel.color = lightBlue;
		colorBarWheelOne.color = pink;
		colorBarWheelTwo.color = pink;

	}

	void ButtonsToBlue () {

		GameObject[] buttons = GameObject.FindGameObjectsWithTag ("Program Button");
		int buttonCount = buttons.Length;

		for(int i = 0; i < buttonCount; i++)
		{
			buttons[i].GetComponent<Image>().color = blue;

		}

//		doneButton.GetComponent<Image>().color = blue;
//		deleteButton.GetComponent<Image>().color = blue;
		turnBar.GetComponent<Image>().color = blue;
//		colorBarOne.color = pink;
//		colorBarTwo.color = pink;
		colorWheel.color = lightPink;
		colorBarWheelOne.color = blue;
		colorBarWheelTwo.color = blue;

	}

	[Command]
	public void CmdBluePlayerHere () {

		GameObject.FindGameObjectWithTag ("Game Manager Local").GetComponent<TurnManagerScript> ().BluePlayerThere ();

	}

	[Command]
	public void CmdSendData (){
	
		sendData++;

	}

	[ClientRpc]
	public void RpcSendData (){

		rpcData++;

	}

}
