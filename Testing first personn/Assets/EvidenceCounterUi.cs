using TMPro;
using UnityEngine;

public class EvidenceCounterUi : MonoBehaviour
{
	public TextMeshProUGUI evidenceText;
    void Start()
    {
		evidenceText.text = PlayerPrefs.GetInt("evidenceCollected") + "/7";
    }
}
