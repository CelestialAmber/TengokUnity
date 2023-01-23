using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinTossHand : MonoBehaviour
{
	public Animator handAnimator, coinAnimator, effectsAnimator;
	public GameObject coinObject;
	public delegate void OnFinishAnimations();
	public event OnFinishAnimations onFinishAnimations;

	public bool tossing = false, pickingUpCoin = false;

	// Start is called before the first frame update
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{
		if(handAnimator.GetCurrentAnimatorStateInfo(0).IsName("TossWait")){
			tossing = false;
		}
	}

	public void ShowCoinObject(){
		coinObject.SetActive(true);
	}
	public void StartToss(){
		tossing = true;
		handAnimator.SetTrigger("Toss");
		handAnimator.SetBool("holdingA",false);
	}

	public IEnumerator StartCoinFallAnimation(){
		coinAnimator.SetTrigger("Fall");
		effectsAnimator.SetTrigger("Drop");
		yield return new WaitForEndOfFrame();
		//Wait for the fall animation to end
		while(coinAnimator.GetCurrentAnimatorStateInfo(0).IsName("Fall")){
			yield return new WaitForEndOfFrame();
		}
		pickingUpCoin = true;
		handAnimator.SetTrigger("Pickup Coin");
		while(handAnimator.GetCurrentAnimatorStateInfo(0).IsName("PickupCoin")){
			yield return new WaitForEndOfFrame();
		}
		pickingUpCoin = false;
		onFinishAnimations.Invoke();
		yield return null;
	}

	public IEnumerator StartCatchAnimation(){
		effectsAnimator.SetTrigger("Drop");
		handAnimator.SetTrigger("Catch");
		while(handAnimator.GetCurrentAnimatorStateInfo(0).IsName("Catch")){
			yield return new WaitForEndOfFrame();
		}
		onFinishAnimations.Invoke();
		yield return null;
	}

	public void Toss(){
		effectsAnimator.SetTrigger("Drop");
	}
}
