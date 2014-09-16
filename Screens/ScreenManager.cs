using UnityEngine;
using System.Collections.Generic;

public class ScreenManager : MonoBehaviour{

    public enum ScreenEnum { None, HomeScreen, MapScreen, GameplayScreen }
    public struct ScreenData { public GameObject go; public AbstractScreen ascreen;};
    public static Dictionary<ScreenEnum, ScreenData> screens;
    public static ScreenEnum currentScreen;

    static ScreenManager()
    {
        screens = new Dictionary<ScreenEnum, ScreenData>();
    }

    void Start()
    {
        //Adjust GUI textures
//        Object[] objects = Object.FindObjectsOfType(typeof(GUITexture));
//        foreach (Object component in objects)
//        {
//            GUITexture guiTexture = (GUITexture)component;
//            guiTexture.pixelInset = PseudoRect.SetRect(guiTexture.pixelInset.x, guiTexture.pixelInset.y, guiTexture.pixelInset.width, guiTexture.pixelInset.height);
//        }
    }

    public static void ShowScreen(ScreenEnum screen)
    {
        //if (!destroy)
        if (screens.ContainsKey(currentScreen))
        {
            screens[currentScreen].ascreen.OnHide();
            screens[currentScreen].go.SetActive(false);
        }
        //else
        //    GameObject.Destroy(screens[currentScreen]);
        currentScreen = screen;
		screens [currentScreen].go.SetActive (true);
		screens [currentScreen].ascreen.OnShow ();
    }
}
