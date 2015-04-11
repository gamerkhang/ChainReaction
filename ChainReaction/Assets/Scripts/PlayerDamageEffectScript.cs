using UnityEngine;
using System.Collections;

public class PlayerDamageEffectScript : ColorChangeScript {

	public float particleDuration = 0.25f;
	public float factor = 2000;

	private float currentDur = 0;

	private bool wasZero = false;

	// Use this for initialization
	void Start () {

	}

	public override Color MyColor {
		get;
		set;
	}
	
	// Update is called once per frame
	void Update () {
		if (currentDur > 0) {
			if(!gameObject.GetComponent<ParticleSystem>().isPlaying){
				gameObject.GetComponent<ParticleSystem>().Play();
			}
			currentDur-=Time.deltaTime;
			if(currentDur<=0){
				//transform.particleSystem.Pause();
				gameObject.GetComponent<ParticleSystem>().Stop();
			}
		}
	}

	public override void applyDamage (Color damage, float multiplier)
	{
		base.applyDamage (damage, multiplier);
		gameObject.GetComponent<ParticleSystem>().startColor = new Color(damage.r, damage.g, damage.b);
		float emitFactor = factor;
		float runningSum = 0;
		if (multiplier > 0.05) {
			runningSum+=emitFactor * multiplier / 2;
			emitFactor /= 2;
		}
		if (multiplier > 0.15) {
			runningSum+=emitFactor * multiplier / 2;
			emitFactor /= 2;
		}
		transform.GetComponent<ParticleSystem>().emissionRate = runningSum + multiplier * emitFactor;
		//gameObject.particleSystem.Emit ((int)(multiplier * 1000));
		currentDur = particleDuration;
	}
}
