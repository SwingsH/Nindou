using UnityEngine;
using System.Collections;
using System.Collections.Generic;
/*
 */
public class GameMaster : MonoBehaviour {
    Stack States;
	// Use this for initialization
	void Start () {
        States = new Stack();
		States.Push(gameObject.AddComponent<BattleManager>());
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

public abstract class GameState : MonoBehaviour
{
    public interface IGameStateObserver
    {
		void StateIsEnd();
		//public void StateIsStart();
    }
	List<IGameStateObserver> observers;
	public void RegisterObserver(IGameStateObserver obs)
	{
		if(observers == null)
			observers = new List<IGameStateObserver>();
		if(!observers.Contains(obs))
			observers.Add(obs);
	}
	public void UnregisterObserver(IGameStateObserver obs)
	{
		if (observers != null)
			observers.Remove(obs);
	}
    public abstract void StartState();
    public abstract void EndState();
	public virtual void Pause()
	{
		enabled = false;
	}
	public virtual void Resume()
	{
		enabled = true;
	}
}