using System.Collections.Generic;
using UnityEngine;

public class DrawDamageTrail : MonoBehaviour {

    private struct TrailPoint {
        public Vector3 position;
        public float spawnTime;
    }

    [SerializeField] private bool drawGizmos;

    private LineRenderer damageTrailLineRenderer;
    private List<TrailPoint> trailPointList = new List<TrailPoint>();
    private float lifeTimePerPoint;
    private int trailDamageAmount;
    private Vector2 damageBoxSize = new Vector2(0.1f, 0.1f);
    private float trailDamageCooldown;
    private float trailDamageCounter;

    private void Awake() {
        damageTrailLineRenderer = this.GetComponentInChildren<LineRenderer>();

        EnemyData enemyData = DataManager.Instance.GetEnemyData();
        lifeTimePerPoint = enemyData.cursedChalkStick.lifeTimePerDamageTrailPoint;
        trailDamageAmount = enemyData.cursedChalkStick.trailDamageAmount;
        trailDamageCooldown = enemyData.cursedChalkStick.trailDamageCooldown;
    }

    private void OnEnable() {
        trailPointList.Clear();
    }

    private void Update() {
        CountDamageTimer();
        DealDamageToPlayer();
    }

    private void FixedUpdate() {
        DrawAndUpdateTrail();
    }

    private void DrawAndUpdateTrail() {
        // Adding a new trail point
        TrailPoint trailPoint = new TrailPoint {
            position = this.transform.position,
            spawnTime = Time.time
        };

        trailPointList.Add(trailPoint);

        // Remove old trail points
        for (int i = trailPointList.Count - 1; i >= 0; i--) {
            if (Time.time - trailPointList[i].spawnTime >= lifeTimePerPoint) {
                trailPointList.Remove(trailPointList[i]);
            }
        }

        // Apply all the trail points from the list to the line renderer
        damageTrailLineRenderer.positionCount = trailPointList.Count;
        for (int i = 0; i < trailPointList.Count; i++) {
            damageTrailLineRenderer.SetPosition(i, trailPointList[i].position);
        }
    }

    private void DealDamageToPlayer() {
        foreach(TrailPoint trailPoint in trailPointList) {
            Collider2D hit = Physics2D.OverlapBox(trailPoint.position, damageBoxSize, 0f);

            if(hit == null || trailDamageCounter > 0f) {
                return;
            }

            if(hit.TryGetComponent(out PlayerHealth playerHealth)) {
                playerHealth.TakeDamage(trailDamageAmount);
                Debug.Log("Damaged by trail " + trailDamageAmount);
                trailDamageCounter = trailDamageCooldown;
                return;
            }
        }
    }

    private void CountDamageTimer() {
        trailDamageCounter -= Time.deltaTime;
    }

    private void OnDrawGizmos() {
        if (!drawGizmos) {
            return;
        }

        Gizmos.color = Color.orange;

        foreach(TrailPoint trailPoint in trailPointList) {
            Gizmos.DrawWireCube(trailPoint.position, damageBoxSize);
        }
    }
}
