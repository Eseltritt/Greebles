using UnityEngine;

public class HealthTracker : MonoBehaviour
{
    public static HealthTracker instance;

    public int HumanHealth { get; private set; }


    void OnEnable(){
        if (instance == null){
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this);
        }         
    }

    /* public void UpdateHumanHealth(Component sender, object value)
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
    } */
}
