﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSpawner : MonoBehaviour
{
	//enum des différents type de spawner si "left" la salle parente de ce spawner se trouve à gauche du spawner et possède une porte vers lui (il faudra faire spawn une salle avec une ouverture vers elle)
	public enum OpeningDirection
	{
		Left,
		Top,
		Right,
		Bottom
	}

	public bool fromOverride = false;
	public OpeningDirection openingdir;
	bool spawned = false;								//si ce spawner a déjà fait spawn une room
	bool destroying = false;							//si ce spawner attends d'être détruit
	RoomTemplates roomTemplates;						//référence à la liste des salles disponibles
	float delay;										//delai de spawn

	private void Start()
	{
		roomTemplates = FindObjectOfType<RoomTemplates>();

		if (openingdir == OpeningDirection.Left)					//application de différents délai en fonction de la direction (permet de ne pas avoir de génération simultanée de salle et garanti que la seed génère toujours le même donjon)
			delay = .21f;
		if (openingdir == OpeningDirection.Top)
			delay = .32f;
		if (openingdir == OpeningDirection.Right)
			delay = .54f;
		if (openingdir == OpeningDirection.Bottom)
			delay = .79f;

		Invoke("DefineNeededRoom",delay);							//Execute la fonction après un certain délai

	}

	private void DefineNeededRoom()
	{
		if(roomTemplates.nonSaferoomspawned > roomTemplates.SafeRoomFrequency)
		{

			float safeRoomRandom = UnityEngine.Random.Range(0, 100);
			if (safeRoomRandom < roomTemplates.currentSafeRoomChances)
			{
				if (openingdir == OpeningDirection.Left)                            //si on a besoin d'un salle avec une ouverture vers la gauche, récupère une salle dans la liste des salle avec une ouverture à gauche
					SpawnRoom(roomTemplates.leftSafeRooms);
				if (openingdir == OpeningDirection.Top)                             //etc.
					SpawnRoom(roomTemplates.upSaferooms);
				if (openingdir == OpeningDirection.Right)
					SpawnRoom(roomTemplates.rightSafeRooms);
				if (openingdir == OpeningDirection.Bottom)
					SpawnRoom(roomTemplates.downSafeRooms);

				roomTemplates.nonSaferoomspawned = 0;
				roomTemplates.currentSafeRoomChances = 0;
				return;
			}
			else
			{
				roomTemplates.nonSaferoomspawned += 1;
				roomTemplates.currentSafeRoomChances += roomTemplates.safeRoomIncreaseChance;
			}
		}
		else
		{
			roomTemplates.nonSaferoomspawned += 1;
		}

		if (roomTemplates.spawnedRooms.Count > roomTemplates.dungeonSize)		//génère les salles du donjon tant qu'il n'a pas dépassé sa taille max
		{
			if (!roomTemplates.runeSpawned)
			{
				if (openingdir == OpeningDirection.Left)                            //si on a besoin d'un salle avec une ouverture vers la gauche, récupère une salle dans la liste des salle avec une ouverture à gauche
					SpawnRoom(roomTemplates.leftRuneRooms);
				if (openingdir == OpeningDirection.Top)                             //etc.
					SpawnRoom(roomTemplates.upRuneRooms);
				if (openingdir == OpeningDirection.Right)
					SpawnRoom(roomTemplates.rightRuneRooms);
				if (openingdir == OpeningDirection.Bottom)
					SpawnRoom(roomTemplates.downRuneRooms);

				roomTemplates.runeSpawned = true;
			}
			else
			{
				if (openingdir == OpeningDirection.Left)							//si on a besoin d'un salle avec une ouverture vers la gauche, récupère une salle dans la liste des salle avec une ouverture à gauche
					SpawnRoom(roomTemplates.leftEnds);
				if (openingdir == OpeningDirection.Top)								//etc.
					SpawnRoom(roomTemplates.upEnds);
				if (openingdir == OpeningDirection.Right)
					SpawnRoom(roomTemplates.rightEnds);
				if (openingdir == OpeningDirection.Bottom)
					SpawnRoom(roomTemplates.downEnds);

			}


			FindObjectOfType<PlayerStats>().SetStartPos();

		}
		else																	//si le donjon a dépassé la taille max, place des salles issues d'une liste de cul de sac pour le fermer
		{
			if (openingdir == OpeningDirection.Left)
				SpawnRoom(roomTemplates.LeftEntrances);
			if (openingdir == OpeningDirection.Top)
				SpawnRoom(roomTemplates.upEntrances);
			if (openingdir == OpeningDirection.Right)
				SpawnRoom(roomTemplates.rightEntrances);
			if (openingdir == OpeningDirection.Bottom)
				SpawnRoom(roomTemplates.downEntrances);

			GameObject.FindGameObjectWithTag("Loading").GetComponent<Animator>().Play("Disappear");
		}
	}

	private void SpawnRoom(List<GameObject> roomList)							//Instancie aléatoirement une salle de la liste en paramètre
	{
		if (spawned || destroying)												//si le spawner a déjà instancié une salle ou entre en conflit avec un autre le spawn est annulé
			return;

		int roomID;																//index de la liste de salle
		if (roomTemplates.seedGenerated == false || fromOverride)								//Si le donjon est généré sans seed
		{
			roomID = UnityEngine.Random.Range(0, roomList.Count);				//on choisit aléatoirement une salle et on stock l'ID
			roomTemplates.seed += roomID;										//on ajoute l'id à la seed
		}
		else																	//si le donjon est généré via une seed
		{
			roomID = int.Parse(roomTemplates.tempSeed.Substring(0, 1));				//on récupère le premier caractère de la seed et on s'en sert d'id pour instancier la salle du donjon
			roomTemplates.tempSeed = roomTemplates.tempSeed.Remove(0, 1);				//et on retire le charactère de la chaîne pour la prochaine génération
		}

		GameObject instance = Instantiate(roomList[roomID], transform.position, Quaternion.identity);
		roomTemplates.spawnedRooms.Add( instance);          //on ajoute la salle istancié à la liste des salle du donjon
		roomTemplates.spawnedRoomsName.Add(roomList[roomID].name);
		roomTemplates.spawnedPos.Add(transform.position);
		spawned = true;															//la salle a été spawned
		Destroy(gameObject);													//on détruit le spawner
	}

	private void OnTriggerEnter(Collider other)									
	{
		if (other.tag != "RoomSpawner")											//si le spawner rentre en collision avec un autre salle on le détruit
		{
			Destroy(gameObject);
		}
		else
		{
			if (other.GetComponent<RoomSpawner>().destroying == false )			//s'il rentre en collision avec un autre spawner actif on le désactive et on le détruit
			{
				spawned = true;
				destroying = true;
				Destroy();
			}
		}
	}


	public void Destroy()
	{
		Destroy(gameObject);
	}
}
