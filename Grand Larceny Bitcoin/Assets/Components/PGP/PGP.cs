using UnityEngine;
using System.Collections;
using System.IO;
using System;
using Org.BouncyCastle.Crypto.Generators;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Asn1.Pkcs;
using Org.BouncyCastle.Pkcs;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.X509;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Security;



namespace EncryptionHelp{
public class PGP : MonoBehaviour {

		//TODO Create a way to store and send Keyinfo to CryptoHelper

		public string stringToEncrypt;

	// Use this for initialization
	void Start () {
	//FIXME Generate keys. Dev mode. Refactor when working
//			RsaKeyPairGenerator genKey = new RsaKeyPairGenerator();
//			genKey.Init (new KeyGenerationParameters (new Org.BouncyCastle.Security.SecureRandom (), 1024));
//			var pair= genKey.GenerateKeyPair();
//
//			PrivateKeyInfo privateKeyInfo = PrivateKeyInfoFactory.CreatePrivateKeyInfo(pair.Private);
//			byte[] serializedPrivateBytes = privateKeyInfo.ToAsn1Object().GetDerEncoded();
//			string serializedPrivate = Convert.ToBase64String(serializedPrivateBytes);
//
//			SubjectPublicKeyInfo publicKeyInfo = SubjectPublicKeyInfoFactory.CreateSubjectPublicKeyInfo(pair.Public);
//			byte[] serializedPublicBytes = publicKeyInfo.ToAsn1Object().GetDerEncoded();
//			string serializedPublic = Convert.ToBase64String(serializedPublicBytes);
//
//			RsaPrivateCrtKeyParameters privateKey = (RsaPrivateCrtKeyParameters) PrivateKeyFactory.CreateKey(Convert.FromBase64String(serializedPrivate));
//			RsaKeyParameters publicKey = (RsaKeyParameters) PublicKeyFactory.CreateKey(Convert.FromBase64String(serializedPublic));
//
//			Debug.Log (serializedPublic);
//			//FIXME only work in editor. Dev a solution for android
//			System.IO.File.WriteAllText (Application.dataPath + "/pubkey", serializedPublic);
//			System.IO.File.WriteAllText (Application.dataPath + "/msg", stringToEncrypt);
//
////			Stream out txt. For Debug
////						string text = System.IO.File.ReadAllText(Application.dataPath + "/pubkey");
////						Debug.Log (text);
//
////			EncryptFile ();
//			FileEncryptionTasks.Helpers.PGP.PGPEncryptDecrypt.EncryptFile(Application.dataPath + "/msg",Application.dataPath + "/msgEncrypt",Application.dataPath + "/pubkey",true,true);


	}
	
		void EncryptFile(){

//			//Stream out txt. For Debug
//			string text = System.IO.File.ReadAllText(Application.dataPath + "msg");
//			Debug.Log (text);

//			FileEncryptionTasks.Helpers.PGP.PGPEncryptDecrypt(Application.dataPath + "msg",Application.dataPath + "msgEncrypt",



		}





}
}