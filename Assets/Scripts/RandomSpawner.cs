using UnityEngine;

public class RandomSpawner : MonoBehaviour
{
	[SerializeField] private Transform[] _spawnTransforms;

	[SerializeField] private GameObject _objectToSpawn;

	[SerializeField] private Transform _holder;

	public void Spawn()
	{
		var randomSpawn = _spawnTransforms[Random.Range(0, _spawnTransforms.Length)];
		Instantiate(_objectToSpawn, randomSpawn.position, randomSpawn.rotation, _holder);
	}
}
