using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;


// Used to gather touch position to pass into Entropy
public class TouchEntropy : MonoBehaviour {

	//Objects
	public GameObject startBtn;
	public GameObject entropyButton;
	public GameObject reGenBtn;

	//UI
	public Text entropyText;

	//Variables
	private float entropyValue;

	//Bools
	private bool isDown = false;
	private bool isStarted = false;


	//References
	public BtcAddress.Forms.AddressGen addGen;


	void OnEnable () {
		Begin ();

	}

	void Begin(){
		addGen.status.text = " ";
		addGen.keyOutput.text = " ";
		startBtn.SetActive (true);
		entropyButton.SetActive (false);
		reGenBtn.SetActive (false);

	}


	#region Buttons
	public void StartGenerator(){

		if (!isStarted) {
		
			isStarted = true;
			startBtn.SetActive (false);
			entropyButton.SetActive (true);
			reGenBtn.SetActive (false);
		}

	}


	public void TouchDown(){

		if (!isDown) {
			isDown = true;
		}
	}


	public void TouchUp(){
	
		if (isDown) {
			isDown = false;

			//Start Generator for Address
			addGen.btnGenerateAddresses_Click (entropyText.text);
			entropyText.text = " ";
			entropyButton.SetActive (false);
			isStarted = false;

			StartCoroutine ("TryAgain");

		}
	
	}

	//REGEN BUTTON
	public void ReGen(){

		addGen.status.text = " ";
		addGen.keyOutput.text = " ";

		StartGenerator ();


	}

	public void Restart(){


		LoadingController.ctrl.RestartExample ();
	}

	#endregion


	void Update() {

		if (isDown) {
		
			Vector2 pointerPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
			entropyValue = pointerPos.x * pointerPos.y;



			entropyText.text = pointerPos.x.ToString() + pointerPos.y.ToString();


		
		}
	}


	// Coroutines

	IEnumerator TryAgain(){


		yield return new WaitForSeconds (1f);


	
		reGenBtn.SetActive (true);

	}
}
