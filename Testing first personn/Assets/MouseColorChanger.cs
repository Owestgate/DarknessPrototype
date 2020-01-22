using UnityEngine;
using UnityEngine.UI;

public class MouseColorChanger : MonoBehaviour
{
	public Color startColor;
	public Color batMouseOverColor;
	public Color evidenceMouseOverColor;
	bool mouseOver = false;
	public GameObject ui;

	void EvidenceOnMouseEnter()
	{
		mouseOver = true;
		GetComponent<Renderer>().material.SetColor("_Color", evidenceMouseOverColor);
	}

	void EvidenceOnMouseExit()
	{
		mouseOver = false;
		GetComponent<Renderer>().material.SetColor("_Color", startColor);
	}
}
