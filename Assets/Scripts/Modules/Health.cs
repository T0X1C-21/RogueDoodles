using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour {
    
    protected float healthPoints;
    protected int maxHealthPoints;
    private Slider healthSlider;

    protected virtual void Awake() {
        healthPoints = maxHealthPoints;
        healthSlider = this.GetComponentInChildren<Slider>();
    }

    private void Update() {
        CalculateHealthSlider();
    }

    // temp health slider
    public void CalculateHealthSlider() {
        float healthSliderValue = Mathf.InverseLerp(0f, maxHealthPoints, healthPoints);
        healthSlider.value = healthSliderValue;
    }

    public virtual void TakeDamage(float damageAmount) {
        healthPoints -= damageAmount;
    }

}