using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public enum EJournalState
{
	StartImpactScore,
	UpdateImpactScore,
	Event,
}

public class CJournalScript : MonoBehaviour {
	private CEventScreenScript EventScreenScript;
	private CScoreCounterScript ScoreCounterScript;
	private EJournalState JournalState = EJournalState.StartImpactScore;

	// Use this for initialization
	void Start ()
	{
		EventScreenScript = GameObject.Find("EventScreenParent").GetComponent<CEventScreenScript>();
		ScoreCounterScript = GameObject.Find("ScoreParent").GetComponent<CScoreCounterScript>();
		Debug.Log(EventScreenScript);
	}
	
	// Update is called once per frame
	void Update ()
	{
		switch(JournalState)
		{
			case EJournalState.StartImpactScore:
				{
					EventScreenScript.InstantOut("ReferenceEventScreen_Sprite");
					ScoreCounterScript.StartImpactScore();
					JournalState = EJournalState.UpdateImpactScore;
					break;
				}
			case EJournalState.UpdateImpactScore:
				if(ScoreCounterScript.IsImpactScoreFinished())
				{
					EventScreenScript.In("ReferenceEventScreen_Sprite");
					JournalState = EJournalState.Event;
				}
				break;
			case EJournalState.Event:
				break;
		}
	}
}
