using UnityEngine;

public class AttachObjectToBone : MonoBehaviour
{
    /// The GameObject to be attached as a child to the bone.
    public GameObject objectToAttach;

    /// The GameObject representing the bone to which the object will be attached.
    public GameObject bone;

    public Vector3 localPositionOffset;
    /// Attaches the `objectToAttach` to the `bone` as a child, resetting its local position and rotation.
    public void Attach()
    {
        if (objectToAttach != null && bone != null)
        {
            objectToAttach.transform.parent = bone.transform;
            objectToAttach.transform.localPosition = localPositionOffset;
            objectToAttach.transform.localRotation = Quaternion.identity;
            Debug.Log("Object attached to bone successfully.");
          
        }
        else
        {
            Debug.LogError("Object to attach or bone is null. Please assign both objects in the Inspector.");
        }
    }
}