﻿using System;
using UnityEngine;

	public class CrateControlScript : MonoBehaviour
	{
		private SurpriseMaker _surpriseMaker;
		private GameObject _triggeredObject;
		public GameObject CrateDestroyParticle;
		public GameObject Player;
		private MeshRenderer _meshRenderer;
		private const int Score = 5;
		
		private void Start()
		{
			_meshRenderer = GetComponent<MeshRenderer>();
		}

		public void DestroyCrate()
		{
			Player = GameObject.Find("Player1(Clone)");

			ResetPlayerOnCrateCollision();
			DeductPlayerEnergy();
			AddPlayerScore();

			_meshRenderer.enabled = false;
			var tempGameObject = (GameObject)Instantiate(CrateDestroyParticle,transform.position , Quaternion.identity);
			transform.tag = "MoveCollider";
			Destroy(tempGameObject, .3f);
			Invoke("Destroy", .4f);
		}

		private void Destroy()
		{
			Destroy(gameObject);
			_surpriseMaker = GameObject.Find("SurpriseMaker").GetComponent<SurpriseMaker>();
			_surpriseMaker.PlaceBonus(transform.position);
		}

		private void ResetPlayerOnCrateCollision()
		{
				Player.GetComponent<PlayerCollisionScript>().ResetOnCrate();
		}

		private void DeductPlayerEnergy()
		{
			Player.GetComponent<PlayerStatusScript>().AddPlayerScore(Score);
			Player.GetComponent<PlayerStatusScript>().DeductEnergy();
		}

		private void AddPlayerScore()
		{
			Player.GetComponent<PlayerStatusScript>().AddPlayerScore(Score);	
			Player.GetComponent<PlayerStatusScript>().AddPlayerScore(Score);
		}
	}

