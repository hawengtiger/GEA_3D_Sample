using UnityEngine;
using TMPro;

public class KillCountUI : MonoBehaviour
{
    public TMP_Text label;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Health.killCount = 0;   // Play 마다 0 부터
    }

    // Update is called once per frame
    void Update()
    {
        label.text = "KILL " + Health.killCount;
    }
}
