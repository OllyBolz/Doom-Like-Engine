using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    private NavMeshAgent enemyAgent; //'This Enemy' NavMeshAgent
    [SerializeField] private Transform playerTr; //Player coordinates
    [SerializeField] private Transform cameraTr;
    private Transform probeTr;


    private Vector3 lookPosition; // Direction to look at
    [SerializeField] private float radialDistance = 100f; //Distance from player to be calculated below

	[SerializeField] private float trackDistance = 10f; //Distance that enemies will follow after
	private float trackSpeed = 0.1f; //Rate that enemies reassign follow direction
    private bool isTracking = false;

    private float roamSpeed = 1f;

    public int enemyHealth = 3;
    void Awake()
    {
        enemyAgent = GetComponent<NavMeshAgent>();  //Auto-assign of NavMeshAgent
        probeTr = GameObject.Find("Probe").GetComponent<Transform>();
	}

	void FixedUpdate() //FixedUpdate is used so enemies pause.
    {
        lookPosition = transform.position - playerTr.position; //Code to make Gameobject face player
		transform.localRotation = Quaternion.LookRotation(lookPosition);

		//Use of pythagorean theorem to determine distance from player
		radialDistance = Mathf.Sqrt(Mathf.Pow((transform.position.x - playerTr.position.x), 2) + Mathf.Pow((transform.position.y - playerTr.position.y), 2) + Mathf.Pow((transform.position.z - playerTr.position.z), 2));

		if (radialDistance >= trackDistance && !isTracking)
		{
			StartCoroutine(EnemyRoam());
		}
		else if (radialDistance < trackDistance && !isTracking)
        {
            StartCoroutine(FollowPlayer()); //This coroutine call follows player
			probeTr.position = new Vector3(0f, 0f, 0f);
			isTracking = true;
        }
		else if (isTracking)
		{
			StartCoroutine(FollowPlayer()); //This coroutine call follows player
		}
	}

    private IEnumerator FollowPlayer() //The coroutine in question
    {
        while (enabled)
        {
            enemyAgent.SetDestination(playerTr.position);
            yield return new WaitForSeconds(trackSpeed);
        }
    }

	private IEnumerator EnemyRoam() 
	{
		while (enabled)
		{
            float randomx = Random.Range(-1f, 1f);
			float randomz = Random.Range(-1f, 1f);

            probeTr.position = new Vector3(probeTr.position.x + randomx, probeTr.position.y, probeTr.position.z + randomz);

			if (probeTr.position.x > 1f)
			{ probeTr.position = new Vector3(1f, probeTr.position.y, probeTr.position.z); }
			if (probeTr.position.x < -1f)
            { probeTr.position = new Vector3(-1f, probeTr.position.y, probeTr.position.z); }
			if (probeTr.position.z > 1f)
			{ probeTr.position = new Vector3(probeTr.position.x, probeTr.position.y, 1f); }
			if (probeTr.position.z < -1f)
			{ probeTr.position = new Vector3(probeTr.position.x, probeTr.position.y, -1f); }

			enemyAgent.SetDestination(probeTr.position);
			yield return new WaitForSeconds(roamSpeed);
		}
	}
}
