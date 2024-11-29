using UnityEngine;

public enum HumanHealthChangedValue
{
    Raised,
    Dropped
}

public class HumanHealth : MonoBehaviour
{
    public int startingHealth = 10;
    public int CurrentHealth;
    public float regenerateHealthRate = 3;

    public GameEvent OnHumaneHealthUpdate;

    private void Start()
    {
        CurrentHealth = startingHealth;
    }

    public void OnHumanHealthChanged(Component sender, object value)
    {
        if (sender is AnimationEvents && value is HumanHealthChangedValue)
        {
            if ((HumanHealthChangedValue)value == HumanHealthChangedValue.Dropped)
                CurrentHealth --;
            else
                CurrentHealth ++;

            OnHumaneHealthUpdate?.Raise_SingleParam(this, CurrentHealth);
        }

        if (CurrentHealth < startingHealth)
            InvokeRepeating("RegenerateHealth", regenerateHealthRate, regenerateHealthRate);
        else
            CancelInvoke("RegenerateHealth");

    }

    private void RegenerateHealth()
    {
        if (CurrentHealth < startingHealth)
            CurrentHealth ++;
    }
}
