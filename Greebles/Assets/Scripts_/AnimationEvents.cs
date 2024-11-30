using UnityEngine;

public class AnimationEvents : MonoBehaviour
{
    [SerializeField] private GameEvent OnHumanHealthChanged;

    public void AttackCompleted()
    {
        OnHumanHealthChanged?.Raise_SingleParam(this, HumanHealthChangedValue.Dropped);
    }
}
