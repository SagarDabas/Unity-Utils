using System;
using UnityEngine;
using System.Collections.Generic;

public class UniqueRandomNumber
{
		List<int> numbers = new List<int> ();

		//min and max is inclusive
		public UniqueRandomNumber (int min, int max)
		{
				for (int i = min; i <= max; i++) {
						numbers.Add (i);		
				}
		}

		public int GetRandomNumber ()
		{
				int cardIndex = UnityEngine.Random.Range (0, numbers.Count - 1);
				int cardNumber = numbers [cardIndex];
				numbers.RemoveAt (cardIndex);
				return cardNumber;
		}
}

