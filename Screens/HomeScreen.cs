using UnityEngine;

public class HomeScreen : AbstractScreen
{

		
		void Awake ()
		{
			
		}

		// Use this for initialization
		void Start ()
		{
				base.Init (ScreenManager.ScreenEnum.HomeScreen);
				ScreenManager.ShowScreen (screen);
				Application.targetFrameRate = 60;
		}

		public override void OnShow ()
		{
				
		}


		public override void OnHide ()
		{

		}

		// Update is called once per frame
		void Update ()
		{

		}
}
