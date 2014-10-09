using UnityEngine;
using System.Collections;

public class KeyPressEvent: GameEvent
{
	public string actionName;

	public KeyPressEvent( string ActionName )
	{
		actionName = ActionName;
	}
}

