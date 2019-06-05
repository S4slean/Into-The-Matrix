using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonOverride
{
	public enum Override
	{
		Enviro, Enemies, Spawn
	}

	public Vector2 spritePos;
	public int overridesIndex;

	//public void PlayOverride()
	//{
	//	RoomCameraTrigger[] rooms = FindObjectsOfType<RoomCameraTrigger>();
	//	foreach(RoomCameraTrigger room in rooms)
	//	{
	//		if(room.transform.position == new Vector3(spritePos.x /21/14 , 0, spritePos.y / 21 / 14))
	//		{
	//			switch (overrides)
	//			{
	//				case Override.Enviro:
	//					{
	//						room.OverrideTraps();
	//						break;
	//					}
	//				case Override.Enemies:
	//					{
	//						room.OverrideEnemies();
	//						break;
	//					}
	//				case Override.Spawn:
	//					{
	//						Instantiate(Resources.Load("Resources/LD/Spawner" ) as GameObject, new Vector3(spritePos.x / 21 / 14, 0, spritePos.y / 21 / 14), Quaternion.identity);
	//						Destroy(room);
	//						break;
	//					}
	//			}
	//		}
	//	}
		
	//}
}
