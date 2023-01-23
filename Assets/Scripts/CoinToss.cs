using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinToss : MonoBehaviour
{
	public enum GameState{
		HoldingCoin,
		TossingCoin,
		CatchingCoin,
		PickingUpCoin
	}

	public CoinTossHand coinTossHand;
	AudioManager audioManager;
	public GameObject coinTossTimerObject;
	CoinTossTimer coinTossTimer;
	public bool coinTossTimerEnabled = true;
	public bool grabbing = false;
	public GameState gameState;
	public float coinFallTime = 3; //Current amount of time the coin stays in the air for
	public float coinFallTimer;
	public float hitThreshold = 0.1f; //How much earlier/later to consider an input as still being a hit

	public AudioClip tossSfx, catchSfx;

	// Start is called before the first frame update
	void Start()
	{
		grabbing = false;
		coinTossHand.onFinishAnimations += ResetGame;
		coinTossTimer = coinTossTimerObject.GetComponent<CoinTossTimer>();
		audioManager = GameObject.Find("Audio Manager").GetComponent<AudioManager>();
	}

	void ResetGame(){
		gameState = GameState.HoldingCoin;
		coinFallTimer = 0;
	}

	// Update is called once per frame
	void Update()
	{
		if(gameState == GameState.TossingCoin){
			coinFallTimer += Time.deltaTime;

			if(coinTossTimerEnabled){
				coinTossTimer.UpdateTimer(coinFallTimer);
			}

			if(coinFallTimer > coinFallTime - hitThreshold){
				//If the player pressed the grab button within the allowed range, the player caught the coin
				if(Input.GetKeyDown(KeyCode.Z) && !grabbing){
					gameState = GameState.CatchingCoin;
					audioManager.Stop();
					audioManager.PlaySound(catchSfx);
					StartCoroutine(coinTossHand.StartCatchAnimation());
				}
			}

			//If the timer has gone past the accepted threshold, the player missed
			if(coinFallTimer > coinFallTime + hitThreshold){
				gameState = GameState.PickingUpCoin;
				StartCoroutine(coinTossHand.StartCoinFallAnimation());
			}
		}

		//Only allow input if we're not doing the picking up coin animation or catching coin animation
		if(gameState != GameState.CatchingCoin && !coinTossHand.pickingUpCoin){
		if(Input.GetKeyDown(KeyCode.Z) && !grabbing){
			coinTossHand.handAnimator.SetBool("holdingA",true);
			grabbing = true;
		}else if(Input.GetKeyUp(KeyCode.Z)){
			coinTossHand.handAnimator.SetBool("holdingA",false);
			grabbing = false;
		}

		if(Input.GetKey(KeyCode.Z) && grabbing && !coinTossHand.tossing){
			//If the player is holding down the grab button and presses the toss button, do the toss animation.
			if(Input.GetKeyDown(KeyCode.X)){
				Debug.Log("Doing toss anim");
				coinTossHand.StartToss();
				if(gameState == GameState.HoldingCoin){
					audioManager.PlaySound(tossSfx);
					coinTossTimer.ResetTimer();
					gameState = GameState.TossingCoin;
					coinFallTimer = 0;
				}
			}
		}
		}
	}
}
