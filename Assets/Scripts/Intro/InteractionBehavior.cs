using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class InteractionBehavior : MonoBehaviour {

	[SerializeField]
	protected UnityEvent onInteraction;
	protected float distanceToInteraction;
	protected Transform player;

	public void Awake(){

		player = GameObject.FindWithTag ("Player").transform;
	
	}




}
