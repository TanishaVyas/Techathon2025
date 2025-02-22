using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NewBehaviourScript : MonoBehaviour
{
	public void LoadScene(string sceneName){
		if(sceneName == "homepage" || sceneName == "knee 1")
		{
			if(GameObject.FindWithTag("bleasd"))
			{
				GameObject.FindWithTag("bleasd").GetComponent<newblescript>().DisconnectDevice();
			}
		}
		 SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
	}
	public void LoadProfile(string profileName){
		SceneManager.LoadScene("Profile", LoadSceneMode.Single);
	}
	public void exercisedata()
	{
		SceneManager.LoadScene("OwnData", LoadSceneMode.Single);
	}
}
