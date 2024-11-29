using UnityEngine;

public class AnimationEvents : MonoBehaviour
{
    [SerializeField] private GameEvent OnHumanHealthChanged;

    public void AttackCompleted()
    {
        Debug.Log("attacked");
        OnHumanHealthChanged?.Raise_SingleParam(this, HumanHealthChangedValue.Dropped);
    }
}
