using UnityEngine;

public class RiddleController : MonoBehaviour
{
    [SerializeField] GameObject secretObject;
    public bool solved = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        HideSecret();
    }


    public void RevealSecret()
    {
        secretObject.SetActive(true);
        solved = true;
    }

    public void HideSecret()
    {
        secretObject.SetActive(false);
        solved = false;
    }
}
