using UnityEngine;

public enum HumanHealthChangedValue
{
    Raised,
    Dropped
}

public class HumanHealth : MonoBehaviour
{
    public static HumanHealth instance;
    public int startingHealth = 10;
    public int CurrentHealth;
    public float regenerateHealthRate;
    private bool _isRegenerating = false;

    public GameEvent OnHumaneHealthUpdate;

    private void OnEnable()
    {
        if (instance == null){
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this);
        }         
    }

    private void Start()
    {
        CurrentHealth = startingHealth;
    }

    public void OnHumanHealthChanged(Component sender, object value)
    {
        if (sender is AnimationEvents && value is HumanHealthChangedValue)
        {
            if ((HumanHealthChangedValue)value == HumanHealthChangedValue.Dropped)
            {
                if(CurrentHealth > 0)
                {
                    CurrentHealth --;
                    OnHumaneHealthUpdate?.Raise_SingleParam(this, CurrentHealth);
                }
            }
            else
            {
                if(CurrentHealth < startingHealth)
                {
                    CurrentHealth ++;
                    OnHumaneHealthUpdate?.Raise_SingleParam(this, CurrentHealth);
                }
            }

            if (CurrentHealth < startingHealth && !_isRegenerating)
            {
                InvokeRepeating("RegenerateHealth", regenerateHealthRate, regenerateHealthRate);
                _isRegenerating = true;
            }
        }
    }

    private void RegenerateHealth()
    {
        CurrentHealth ++;
        OnHumaneHealthUpdate?.Raise_SingleParam(this, CurrentHealth);

        if (CurrentHealth == startingHealth)
        {
            CancelInvoke("RegenerateHealth");
            _isRegenerating = false;
        }
            
    }
}
