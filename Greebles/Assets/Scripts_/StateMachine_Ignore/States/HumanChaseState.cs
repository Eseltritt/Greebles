using UnityEngine;

public class HumanChaseState : BaseState
{
    public Human _human { get; private set; }
    public HumanChaseState(Human human) :base()
    {
        _human = human;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
