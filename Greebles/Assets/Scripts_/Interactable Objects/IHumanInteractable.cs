using UnityEngine;

public interface IHumanInteractable
{
    bool ToBeCorrected { get; set; }
    bool IsMisplaceable { get; set; }

    Vector3 InitialPosition { get; set; }
    Quaternion InitialRotation { get; set; }

    public abstract void CorrectInteractable();
}
