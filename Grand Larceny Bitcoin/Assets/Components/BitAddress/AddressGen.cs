//THE MAGIC HAPPENS HERE FOR BIT ADDRESS AND ASSOCIATED LIBS
// This was an old open source windows form application, hence the namespaces

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Casascius.Bitcoin;
using System;
using System.ComponentModel;
using UnityEngine.UI;


namespace BtcAddress.Forms{
public class AddressGen : MonoBehaviour {

		// BOOLS

		// Range of valid keys according to secp256k1 standard
		// https://en.bitcoin.it/wiki/Private_key#Range_of_valid_ECDSA_private_keys

		// Forget these three for time being, could be used for future:
		private bool rdoEncrypted = false;
		private bool rdoDeterministicWallet = false;
		private bool rdoTwoFactor = false;
		[Header("Mini Key Format")]
		// Used where space is critical like QR codes and in physical bitcoins
		public bool rdoMiniKeys = false;
		[Header("Wallet Import Format")]
		// Base58 Wallet Import Format (WIF) Most common way to represent private keys
		public bool rdoRandomWallet = false;

		[Header("Should Save Private Key")]
		public bool RetainPrivateKeys = false;

		// UI
		public Text status;
		public Text keyOutput;
		public InputField inputText;

		//Data
		private string usEntr;


		// FROM SRC
	private enum GenChoices {
		Minikey, WIF, Encrypted, Deterministic, TwoFactor
	}

	private GenChoices GenChoice;

	private bool Generating = false;
	private bool GeneratingEnded = false;

	private bool StopRequested = false;

	private bool PermissionToCloseWindow = false;

	

	private string UserText;

	private int RemainingToGenerate = 0;


	public List<KeyCollectionItem> GeneratedItems = new List<KeyCollectionItem>();

	private Bip38Intermediate[] intermediatesForGeneration;

	private int intermediateIdx;


		public void btnGenerateAddresses_Click(string toHash) {

			usEntr = toHash;

			if (Generating) {
				StopRequested = true;
				status.text = "Stopping...";
				return;
			}

			if (rdoEncrypted && inputText.text == "") {
				Debug.Log("An encryption passphrase is required. Choose a different option if you don't want encrypted keys. Passphrase missing");
				return;
			}
			if (rdoDeterministicWallet && inputText.text == "") {
				Debug.Log("A deterministic seed is required.  If you do not intend to create a deterministic " +
					"wallet or know what one is used for, it is recommended you choose one of the other options.  An inappropriate seed can result " +
					"in the unexpected theft of funds. Seed missing");
				return;
			}

				intermediatesForGeneration = null;
//			}

			//no longer using threads
//			GenerationThread = new Thread(new ThreadStart(GenerationThreadProcess));

			//TODO I HAVE NO IDEA ABOUT THIS: 
			RemainingToGenerate = 1;
			UserText = inputText.text;


			if (rdoDeterministicWallet) GenChoice = GenChoices.Deterministic;
			if (rdoEncrypted) {
				GenChoice = GenChoices.Encrypted;
				// intermediate codes start with "passphrasek" thru "passphrases"
				string ti = inputText.text.Trim();

			}
			if (rdoMiniKeys) GenChoice = GenChoices.Minikey;
			if (rdoRandomWallet) GenChoice = GenChoices.WIF;
			if (rdoTwoFactor) {
				GenChoice = GenChoices.TwoFactor;
			}

			//TODO A graphic showing a timer while generation occurs, or run while load screen is running


	
			Generating = true;
			GeneratingEnded = false;
			StopRequested = false;
		
			StartCoroutine("GenerationThreadProcess");
		}

		//USED TO BE AN ACTUAL THREAD, But turned it into a coroutine for unity (2sec gen time on android)
		IEnumerator GenerationThreadProcess() {
			Debug.Log ("Generating");
			Bip38Intermediate intermediate = null;
			if (GenChoice == GenChoices.Encrypted) {
				intermediate = new Bip38Intermediate(UserText, Bip38Intermediate.Interpretation.Passphrase);                
			}

			int detcount = 1;

			while (RemainingToGenerate > 0 && StopRequested == false) {
				KeyCollectionItem newitem = null;
				switch (GenChoice) {
				case GenChoices.Minikey:
					MiniKeyPair mkp = MiniKeyPair.CreateRandom (ExtraEntropy.GetEntropy ());
					string s = mkp.AddressBase58; // read the property to entice it to compute everything
					newitem = new KeyCollectionItem (mkp);

					break;
				case GenChoices.WIF:
					//Main Gen (Others are left in for future use)
					KeyPair kp = KeyPair.CreateFromString (usEntr);
					s = kp.AddressBase58;
					newitem = new KeyCollectionItem (kp);
					Debug.Log (s);
					break;
				case GenChoices.Deterministic:
					kp = KeyPair.CreateFromString(UserText + detcount);
					detcount++;
					s = kp.AddressBase58;
					newitem = new KeyCollectionItem(kp);
					break;
				case GenChoices.Encrypted:
					Bip38KeyPair ekp = new Bip38KeyPair(intermediate);
					newitem = new KeyCollectionItem(ekp);
				
					break;
				case GenChoices.TwoFactor:
					ekp = new Bip38KeyPair(intermediatesForGeneration[intermediateIdx++]);
					if (intermediateIdx >= intermediatesForGeneration.Length) intermediateIdx = 0;
					newitem = new KeyCollectionItem(ekp);
					break;
				}

				lock (GeneratedItems) {
					GeneratedItems.Add(newitem);
					RemainingToGenerate--;
				}
			}
			GeneratingEnded = true;
			Debug.Log ("Generation Ended");
			yield return new WaitForEndOfFrame ();
			CheckGen ();
		
		}

		private void CheckGen() {

			//TODO Error checking and responses
			if (GeneratingEnded) {
				Generating = false;
				GeneratingEnded = false;
	
				usEntr = " ";
				status.text = "Address generated: ";

				foreach (KeyCollectionItem key in GeneratedItems) {
				
					keyOutput.text = key.GetAddressBase58 ();
				
				
				}
			
			}
				
		}

	
}
}