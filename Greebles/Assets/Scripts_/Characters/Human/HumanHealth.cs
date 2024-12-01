using UnityEngine;

public enum HumanHealthChangedValue
{
    Raised,
    Dropped
}

public class HumanHealth : MonoBehaviour
{
    public static HumanHealth instance;
    [Tooltip("This should be an int. Each Greeble Attack lowers health by 1")]
    public int startingHealth = 10;
    public int CurrentHealth { get; private set; }
    [Tooltip("The time (in seconds) it takes to regenerate 1 HP. If set to 0, it is defaulted to 5 seconds")]
    public float regenerateRate;
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

        if(regenerateRate <= 0)
            regenerateRate = 5;
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
                InvokeRepeating("RegenerateHealth", regenerateRate, regenerateRate);
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
