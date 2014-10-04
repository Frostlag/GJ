using UnityEngine;
using System.Collections;

public class KeyPressedEvent: GameEvent
{
	public string key;

	public KeyPressedEvent( string Key )
	{
		key = Key;
	}
}
