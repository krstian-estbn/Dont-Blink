using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDisplay : MonoBehaviour
{
    public GameObject letterDisplay;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ExitDisplay()
    {
        letterDisplay.gameObject.SetActive(false);
        Time.timeScale = 1f;
    }

    public void EnterDisplay()
    {
        letterDisplay.gameObject.SetActive(true);
        Time.timeScale = 0f;
    }
}
