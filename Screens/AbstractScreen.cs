using UnityEngine;
using System.Collections;

public abstract class AbstractScreen : MonoBehaviour {

    protected ScreenManager.ScreenEnum screen;

    protected void Init(ScreenManager.ScreenEnum screen)
    {
        this.screen = screen;
        ScreenManager.screens.Add(screen, new ScreenManager.ScreenData { go = gameObject, ascreen = this});
		gameObject.SetActive (false);
	}

    public abstract void OnShow();

    public abstract void OnHide();
  
    public void OnDestroy()
    {
        ScreenManager.screens.Remove(screen);
        Debug.Log("Destroyed");
    }
}
