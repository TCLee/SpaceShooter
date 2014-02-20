using UnityEngine;
using System.Collections;

/// <summary>
/// Destroys a game object when it exits the level's boundaries.
/// </summary>
public class DestroyByBoundary : MonoBehaviour 
{
	void OnTriggerExit(Collider other)
	{
		Destroy(other.gameObject);
	}
}
