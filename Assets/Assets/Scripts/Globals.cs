using System.Collections.Generic;
using UnityEngine;

public static class GlobalScript
{
	public static List<ChoicesMade> ChoicesMade { get; private set; }
	public static void MarkChoicesMade(ChoicesMade choiceToMark) {
		if (!ChoicesMade.Contains(choiceToMark)) {
			ChoicesMade.Add(choiceToMark);
		}
	}

	public static bool HasChoicesMade(ChoicesMade choiceToTest) {
		return ChoicesMade.Contains(choiceToTest);
	}
}