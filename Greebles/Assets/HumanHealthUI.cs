using UnityEngine;
using UnityEngine.UI;


public class HumanHealthUI : MonoBehaviour
{
    /* private HumanHealth _human; */
    private int _startingHealth;
    public int humanHealth;

    private Image image;
    public Sprite[] images;

    public void Start()
    {
        /* _human = GameObject.FindGameObjectWithTag("Human").GetComponent<HumanHealth>(); */
        _startingHealth = HumanHealth.instance.startingHealth;;
        humanHealth = _startingHealth;

        image = GetComponent<Image>();

        image.sprite = images[0];
    }

    public void UpdateHumanHealth(Component sender, object value)
    {
        if(sender is HumanHealth && value is int)
        {
            humanHealth = (int)value;

            float healthFraction = (float)humanHealth / _startingHealth;

            if(healthFraction < 0.2f)
                image.sprite = images[3];
            else if (healthFraction < 0.4f)
                image.sprite = images[2];
            else if (healthFraction < 0.8f)
                image.sprite = images[1];
            else if (healthFraction > 0.8f)
                image.sprite = images[0];

        }
    }
    
}
