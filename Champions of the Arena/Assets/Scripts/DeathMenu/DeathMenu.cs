using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DeathMenu : MonoBehaviour {

	public Text scoreText;
	public Image backgroundImg;

	private bool isShowned = false;
	private float transition = 0.0f;
	public bool visible = false;

	private void Awake()
	{
		//Debug.Log("Belepett, active");
			//ToggleEndMenu(0);
		
			
	}
	private void OnEnable()
	{
		//Debug.Log("Belepett, active");
		//GameObject.Find("DeathCanvas").SetActive(false);
		//ToggleEndMenu(0);
	}

	// Use this for initialization
	void Start ()
	{
		gameObject.SetActive(false);
		//GameObject.Find("Canvas").SetActive(false);

	}
	
	// Update is called once per frame
	void Update () {
		if (!isShowned)
		{
			return;
		}
		transition += (Time.deltaTime)/2;
		backgroundImg.color = Color.Lerp(new Color(0, 0, 0, 0),new Color(142, 100, 7, 1), transition);

	}
	public void ToggleEndMenu(float score)
	{
		//gameObject.SetActive(true);
		scoreText.text = ((int)score).ToString();
		isShowned = true;
	}
	public void Restart()
	{
		PhotonNetwork.LeaveRoom();
		PhotonNetwork.Disconnect();
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}

	public void ToMenu()
	{
		PhotonNetwork.LeaveRoom();
		PhotonNetwork.Disconnect();
		SceneManager.LoadScene("Menu");
	}
}
