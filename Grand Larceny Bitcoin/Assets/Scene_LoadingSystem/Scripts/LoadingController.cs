// All the top level LoadingScene Members


using UnityEngine;
using System.Collections;

public class LoadingController : MonoBehaviour {


	public static LoadingController ctrl;
	//Canvases
	public GameObject loadingCanvas;
	public GameObject addressCanvasExample;


	void Awake () {

		if (ctrl == null) {
			ctrl = this;
		}

	}

	void Start () {

		RestartExample ();

	}

	public void ShowAddressTest (){

		loadingCanvas.SetActive (false);
		addressCanvasExample.SetActive (true);

	}

	public void RestartExample (){

		addressCanvasExample.SetActive (false);
		loadingCanvas.SetActive (true);


	}


}
