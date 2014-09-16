using UnityEngine;
using System.Collections;

public abstract class AbstractPooledObject : MonoBehaviour {


	void Awake(){
		ObjectPool.instance.poolDictionary.Add(gameObject.GetInstanceID(), this);
	}

	public abstract void OnExtracted();
	
	public abstract void OnReset();
	
	public void OnDestroy()
	{
		ObjectPool.instance.poolDictionary.Remove(GetInstanceID());
	}

}
