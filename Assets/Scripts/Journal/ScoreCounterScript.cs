using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[System.Serializable]
public class CScoreData
{
	public string Content;
	public int CounterTarget;
}

public class ScoreCounterScript : MonoBehaviour {

	private GameObject m_ScoreIncrementerParentGameObject;
	private GameObject m_ImpactScoreParentGameObject;
	private GameObject m_SecondStepParentGameObject;
	private Text m_ImpactScore_Text;
	private Text m_ScoreIncrementer_Text;
	private Text m_ScoreIncrementerContent_Text;
	private Text m_TotalScore_Text;
	private Text m_Malus_Text;
	private int m_Counter = 0;
	public float m_CountingSpeed = 400.0f;
	private int m_CounterTarget = 503;
	public float m_StartPositionY = -2.1f;
	public float m_EndPositionY = -1.6f;
	public float m_PositionYMovementSpeed = 1.0f;
	private float m_PositionY = 0.0f;

	public CScoreData [] m_ScoreDataArray;
	private int m_ScoreDataArrayIndex = 0;

	private int ImpactScore = 0;
	private int m_Malus = -563;
	private int m_TotatScore = 0;

	// Use this for initialization
	void Start ()
	{
		m_ScoreIncrementerParentGameObject	= GameObject.Find("ScoreIncrementerParent");
		m_ImpactScoreParentGameObject		= GameObject.Find("ImpactScoreParent");
		m_SecondStepParentGameObject		= GameObject.Find("SecondStepParent");
		m_ScoreIncrementer_Text				= GameObject.Find("ScoreIncrementer_Text").GetComponent<Text>();
		m_ScoreIncrementerContent_Text		= GameObject.Find("ScoreIncrementerContent_Text").GetComponent<Text>();
		m_ImpactScore_Text					= GameObject.Find("ImpactScore_Text").GetComponent<Text>();
		m_TotalScore_Text					= GameObject.Find("TotalScore_Text").GetComponent<Text>();
		m_Malus_Text						= GameObject.Find("Malus_Text").GetComponent<Text>();
		InitialiseScoreIncrementer();
		m_SecondStepParentGameObject.SetActive(false);
	}
	
	// Update is called once per frame
	void Update ()
	{
		if(UpdateScoreIncrementer())
		{
			if (InitialiseScoreIncrementer())
			{
				m_SecondStepParentGameObject.SetActive(true);
			}
		}

		// Updating Impact score
		string ImpactScoreString	= ImpactScore.ToString();
		m_ImpactScore_Text.text		= ImpactScoreString;

		// Malus
		string MalusString	= m_Malus.ToString();
		m_Malus_Text.text	= MalusString;

		// Total Score
		m_TotatScore = ImpactScore + m_Malus;
		string TotalScoreString = m_TotatScore.ToString();
		m_TotalScore_Text.text = TotalScoreString;

		// Positionning score line properly on screen (Screen ratio handling)
		Vector3 BottomLeftWorldPos = Camera.main.ViewportToWorldPoint(new Vector3(0.0f, 0.0f, Camera.main.nearClipPlane));
		m_ScoreIncrementerParentGameObject.transform.position = new Vector3(BottomLeftWorldPos.x, m_ScoreIncrementerParentGameObject.transform.position.y, m_ScoreIncrementerParentGameObject.transform.position.z);

		Vector3 BottomRightWorldPos = Camera.main.ViewportToWorldPoint(new Vector3(0.95f, 0.0f, Camera.main.nearClipPlane));
		m_ImpactScoreParentGameObject.transform.position = new Vector3(BottomRightWorldPos.x, m_ImpactScoreParentGameObject.transform.position.y, m_ImpactScoreParentGameObject.transform.position.z);
		
	}

	bool UpdateScoreIncrementer()
	{
		float Increment = Time.deltaTime * m_CountingSpeed;
		int IntIncrement = (int)Increment;
		if (IntIncrement > 1)
		{
			m_Counter += IntIncrement;
			if (m_Counter > m_CounterTarget)
			{
				m_Counter = m_CounterTarget;
			}
			else
			{
				ImpactScore += IntIncrement;
			}
		}

		m_ScoreIncrementer_Text.text = ((int)m_Counter).ToString();

		// Y Position of score line 
		if (m_Counter == m_CounterTarget)
		{
			m_PositionY += Time.deltaTime * m_PositionYMovementSpeed;
		}
		else
		{
			m_PositionY = m_StartPositionY;
		}
		m_PositionY = Mathf.Min(m_PositionY, m_EndPositionY);

		// Fade 
		float Ratio = (m_PositionY - m_StartPositionY) / (m_EndPositionY - m_StartPositionY);
		m_ScoreIncrementer_Text.color			= new Color(m_ScoreIncrementer_Text.color.r, m_ScoreIncrementer_Text.color.g, m_ScoreIncrementer_Text.color.b, 1.0f - Ratio);
		m_ScoreIncrementerContent_Text.color	= new Color(m_ScoreIncrementerContent_Text.color.r, m_ScoreIncrementerContent_Text.color.g, m_ScoreIncrementerContent_Text.color.b, 1.0f - Ratio);

		// 
		m_ScoreIncrementerParentGameObject.transform.position = new Vector3(m_ScoreIncrementerParentGameObject.transform.position.x, m_PositionY, m_ScoreIncrementerParentGameObject.transform.position.z);

		// Is finished ?
		return Ratio >= 1.0f;
	}

	bool InitialiseScoreIncrementer()
	{
		if (m_ScoreDataArrayIndex == m_ScoreDataArray.Length)
			return true;
		m_Counter = 0;

		CScoreData ScoreData = m_ScoreDataArray[m_ScoreDataArrayIndex];
		m_ScoreDataArrayIndex++;

		m_ScoreIncrementer_Text.color			= new Color(m_ScoreIncrementer_Text.color.r, m_ScoreIncrementer_Text.color.g, m_ScoreIncrementer_Text.color.b, 1.0f);
		m_ScoreIncrementerContent_Text.color	= new Color(m_ScoreIncrementerContent_Text.color.r, m_ScoreIncrementerContent_Text.color.g, m_ScoreIncrementerContent_Text.color.b, 1.0f);
		m_ScoreIncrementer_Text.text			= "0";
		m_ScoreIncrementerContent_Text.text		= ScoreData.Content;
		m_CounterTarget							= ScoreData.CounterTarget;

		return false;
	}
}
