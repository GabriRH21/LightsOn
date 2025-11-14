using System.Collections.Generic;
using UnityEngine;

public static class Globals 
{
	public static List<ChoicesMade> ChoicesMade { get; private set; }
}

public class GlobalScript
{
	public void MarkChoicesMade(ChoicesMade choiceToMark) {
		if (!Globals.ChoicesMade.Contains(choiceToMark)) {
			Globals.ChoicesMade.Add(choiceToMark);
		}
	}

	public bool HasChoicesMade(ChoicesMade choiceToTest) {
		return Globals.ChoicesMade.Contains(choiceToTest);
	}
}