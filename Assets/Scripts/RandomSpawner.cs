using UnityEngine;

public class RandomSpawner : MonoBehaviour
{
	[SerializeField] private Transform[] _spawnTransforms;

	[SerializeField] private GameObject _objectToSpawn;

	private void Start()
	{
		var randomSpawn = _spawnTransforms[Random.Range(0, _spawnTransforms.Length)];
		Instantiate(_objectToSpawn, randomSpawn.position, randomSpawn.rotation, randomSpawn);
	}
}
