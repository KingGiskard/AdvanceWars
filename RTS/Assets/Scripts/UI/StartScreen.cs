using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class StartScreen : MonoBehaviour {
    public Button startRtsText;
    public Button startMapText;
    // Use this for initialization
    void Start () {
        startRtsText = startRtsText.GetComponent<Button>();
        startMapText = startMapText.GetComponent<Button>();
    }
	
	public void onRtsClick()
    {
        Application.LoadLevel(1);
    }

    public void onMapClick()
    {
        Application.LoadLevel (2);
    }
}
