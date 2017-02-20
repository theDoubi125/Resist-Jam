using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public enum EEventScreenState
{
	ImageInInitialize,
	ImageIn,
	TitleInInitialize,
	TitleIn,
	TitleFadeInInitialize,
	TitleFadeIn,
	Disabled,
}

public class CEventScreenScript : MonoBehaviour
{
	private Vector3 m_SpriteStartPosition = new Vector3(0,0,0);
	private Vector3 m_SpriteEndPosition = new Vector3(0, 0, 0);
	private Vector3 m_TitleStartPosition = new Vector3(0, 0, 0);
	private Vector3 m_TitleEndPosition = new Vector3(0, 0, 0);
	public float m_SpriteSpeed = 1.0f;
	public float m_SpriteScreenOffset = 5.0f;
	public float m_TitleFadeInSpeed = 2.0f;
	private EEventScreenState m_State = EEventScreenState.Disabled;
	private float m_Ratio = 0.0f;

	private GameObject m_SpriteGameObject;
	private GameObject m_TitleParentGameObject;
	private Text m_MainTitleText;
	private Text m_MainTitleShadowText;
	private Text m_SecondaryTitleText;

	private float m_MainTitleShadowStartAlpha = 1.0f;

	public void In(string SpriteGameObjectName)
	{
		m_SpriteGameObject = GameObject.Find(SpriteGameObjectName);
		m_State = EEventScreenState.ImageInInitialize;
	}

	public void InstantOut(string SpriteGameObjectName = "")
	{
		GameObject SpriteGameObject = m_SpriteGameObject;
		if(SpriteGameObjectName != "")
		{
			SpriteGameObject = GameObject.Find(SpriteGameObjectName);
		}
		SpriteGameObject.transform.position = m_SpriteStartPosition;
		m_TitleParentGameObject.transform.position = m_TitleStartPosition;
		SetTitleAlpha(0.0f);
	}

	// Use this for initialization
	void Start()
	{
		m_SpriteEndPosition		= GameObject.Find("ReferenceEventScreen_Sprite").transform.position;
		m_SpriteStartPosition	= m_SpriteEndPosition;
		m_SpriteStartPosition.x = m_SpriteStartPosition.x + m_SpriteScreenOffset;

		m_TitleParentGameObject = GameObject.Find("TitleParent");
		m_TitleEndPosition		= m_TitleParentGameObject.transform.position;
		m_TitleStartPosition	= m_TitleEndPosition;
		m_TitleStartPosition.x	= m_TitleStartPosition.x + m_SpriteScreenOffset;

		m_MainTitleText			= GameObject.Find("MainTitle_Text").GetComponent<Text>();
		m_MainTitleShadowText	= GameObject.Find("MainTitleShadow_Text").GetComponent<Text>();
		m_SecondaryTitleText	= GameObject.Find("SecondaryTitle_Text").GetComponent<Text>();
		

		m_MainTitleShadowStartAlpha = m_MainTitleShadowText.color.a;
	}

	// Update is called once per frame
	void Update()
	{
		bool done = false;
		while (done == false)
		{
			switch (m_State)
			{
				case EEventScreenState.ImageInInitialize:
					m_Ratio = 0.0f;
					m_State = EEventScreenState.ImageIn;
					break;
				case EEventScreenState.ImageIn:
					{
						m_Ratio += Time.deltaTime * m_SpriteSpeed;

						if (m_Ratio >= 1.0f)
						{
							m_Ratio = 1.0f;
							m_State = EEventScreenState.TitleInInitialize;
						}

						Vector3 SpritePosition = Vector3.Lerp(m_SpriteStartPosition, m_SpriteEndPosition, m_Ratio);
						m_SpriteGameObject.transform.position = SpritePosition;
						done = true;
						break;
					}
				case EEventScreenState.TitleInInitialize:
					m_Ratio = 0.0f;
					m_State = EEventScreenState.TitleIn;
					break;
				case EEventScreenState.TitleIn:
					{
						m_Ratio += Time.deltaTime * m_SpriteSpeed;

						if (m_Ratio >= 1.0f)
						{
							m_Ratio = 1.0f;
							m_State = EEventScreenState.TitleFadeInInitialize;
						}

						Vector3 SpritePosition = Vector3.Lerp(m_TitleStartPosition, m_TitleEndPosition, m_Ratio);
						m_TitleParentGameObject.transform.position = SpritePosition;
						done = true;
						break;
					}
				case EEventScreenState.TitleFadeInInitialize:
					m_Ratio = 0.0f;
					m_State = EEventScreenState.TitleFadeIn;
					break;
				case EEventScreenState.TitleFadeIn:
					{
						m_Ratio += Time.deltaTime * m_TitleFadeInSpeed;

						if (m_Ratio >= 1.0f)
						{
							m_Ratio = 1.0f;
							m_State = EEventScreenState.Disabled;
						}
						SetTitleAlpha(m_Ratio);
						done = true;
						break;
					}
				case EEventScreenState.Disabled:
					// Do Nothing
					done = true;
					break;
			}
		}
	}
	void SetTitleAlpha(float Alpha)
	{
		m_MainTitleText.color = new Color(m_MainTitleText.color.r, m_MainTitleText.color.g, m_MainTitleText.color.b, Alpha);
		m_MainTitleShadowText.color = new Color(m_MainTitleShadowText.color.r, m_MainTitleShadowText.color.g, m_MainTitleShadowText.color.b, Alpha * m_MainTitleShadowStartAlpha);
		m_SecondaryTitleText.color = new Color(m_SecondaryTitleText.color.r, m_SecondaryTitleText.color.g, m_SecondaryTitleText.color.b, Alpha);
	}
}
