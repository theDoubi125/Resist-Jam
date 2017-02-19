using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class JournalScript : MonoBehaviour {

	private GameObject ScoreParentGameObject;
	private Text ScoreCounter_Text;
	private Text ScoreCounter_Content;
	private float Counter		= 0.0f;
	public float CountingSpeed	= 10.0f;
	private float CounterTarget = 1240.0f;
	public float StartPositionY = -2.1f;
	public float EndPositionY = -1.6f;
	public float PositionYMovementSpeed = 1.0f;
	private float PositionY = 0.0f;

	// Use this for initialization
	void Start ()
	{
		ScoreParentGameObject = GameObject.Find("ScoreParent");
		ScoreCounter_Text = GameObject.Find("ScoreCounter_Text").GetComponent<Text>();
		ScoreCounter_Content = GameObject.Find("ScoreCounter_Content").GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update ()
	{
		Counter = Counter + Time.deltaTime * CountingSpeed;
		
		if (Counter > CounterTarget)
		{
			Counter = CounterTarget;
		}
		ScoreCounter_Text.text = ((int)Counter).ToString();

		// Y Position of score line 
		if (Counter == CounterTarget)
		{
			PositionY += Time.deltaTime * PositionYMovementSpeed;
		}
		else
		{
			PositionY = StartPositionY;
		}
		PositionY = Mathf.Min(PositionY, EndPositionY);

		// Fade 
		float Ratio = (PositionY - StartPositionY) / (EndPositionY - StartPositionY);
		Color color = 
		ScoreCounter_Text.color = new Color(ScoreCounter_Text.color.r, ScoreCounter_Text.color.g, ScoreCounter_Text.color.b, 1.0f - Ratio);
		ScoreCounter_Content.color = new Color(ScoreCounter_Content.color.r, ScoreCounter_Content.color.g, ScoreCounter_Content.color.b, 1.0f - Ratio);

		// Positionning score line properly on screen
		Vector3 CamWorldPos = Camera.main.ViewportToWorldPoint(new Vector3(0.0f, 0.0f, Camera.main.nearClipPlane));
		ScoreParentGameObject.transform.position = new Vector3(CamWorldPos.x, PositionY, ScoreParentGameObject.transform.position.z);
	}
}
