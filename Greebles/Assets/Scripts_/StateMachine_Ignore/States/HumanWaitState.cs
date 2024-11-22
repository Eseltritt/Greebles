using UnityEngine;

public class HumanWaitState : BaseState
{
    public Human _human { get; private set; }
    public HumanWaitState(Human human) :base()
    {
        _human = human;
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
