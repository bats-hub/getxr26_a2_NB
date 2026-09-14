using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem; //To activate Keyboard.current

/// <summary>
/// GETXR 2026 - Assignment 2: Gameplay and Spawners
/// Attach to a dedicated GameObject in the scene (the "Spawner" object is already
/// there for you) - not to the Player, and not to the thing you're spawning.
/// Complete the TODOs so it instantiates `prefabToSpawn` on a timed interval while
/// `isActive`, keeps track of every active instance in `spawnedObjects` up to
/// `maxActiveObjects`, and can be toggled on/off at runtime.
/// </summary>

public class Spawner : MonoBehaviour
{
    [Header("Spawn Settings")]
    [Tooltip("The prefab this Spawner instantiates. Assign it in the Inspector.")]
    [SerializeField] private GameObject prefabToSpawn;

    [Tooltip("Seconds between spawns.")]
    [SerializeField] private float spawnInterval = 2.0f;

    // TODO: add your own field(s) for WHERE new instances appear - a fixed point
    // relative to this transform, or a random position within some bounds. Your choice.

    //I want random position - more natural and challenging for the game.

    [Tooltip("Max. distance where the coin can spawm on the XZ plane.")]
    [SerializeField] private float spawnMaxDist = 10.0f; //Same way as done above.

    [Header("Activation")]
    [Tooltip("Whether the Spawner is currently instantiating. Toggle this at runtime " +
             "(key press or trigger switch, your choice) to pause/resume spawning.")]
    [SerializeField] private bool isActive = true;

    [Header("Tracking")]
    [Tooltip("Every active instance this Spawner has created and not yet destroyed.")]
    [SerializeField] private List<GameObject> spawnedObjects = new List<GameObject>();

    [Tooltip("Hard cap on simultaneous active instances - stop spawning once this is reached.")]
    [SerializeField] private int maxActiveObjects = 10;

    private float timer = 0f;

    void Update()
    {
        // TODO: pick a way to flip `isActive` at runtime - a key press
        // (e.g. Keyboard.current.spaceKey.wasPressedThisFrame) is the simplest,
        // but a trigger switch the Player walks into also satisfies this.
        // Toggling isActive alone doesn't finish the requirement below - the
        // timer and spawning logic still need to actually respect it.
        //Here using wasPressedThisTime instead of isPressed because otherwise it would sam all the
        //time when held like 60 times per second.

        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            isActive = !isActive;
        }

        // TODO: only accumulate Time.deltaTime into `timer` while `isActive` is
        // true. When inactive, leave `timer` exactly where it was (don't reset
        // it to 0 - resuming should continue counting, not restart the interval).

        if (isActive)
        {
            timer += Time.deltaTime;
        } //Not nested, they ar two independent checks.

        // TODO: once `timer` reaches `spawnInterval` AND spawnedObjects.Count is
        // below `maxActiveObjects`, instantiate `prefabToSpawn` at a position of
        // your choosing, add the new GameObject to `spawnedObjects`, and reset
        // `timer` back to 0. Don't call Instantiate() unconditionally every frame,
        // and don't spawn past the capacity limit even if the timer is ready.

        spawnedObjects.RemoveAll(item => item == null); //for each item in the list - check: is it null? if yes - remove
        
        if (timer >= spawnInterval && spawnedObjects.Count < maxActiveObjects) //Both true at the same time.
        {
            //Now, I wanna create a vector that will point to a random spawn location
            //but that is still closer to spawner than spawnMaxDist.

            float randomX = Random.Range(-spawnMaxDist, spawnMaxDist);
            float randomZ = Random.Range(-spawnMaxDist, spawnMaxDist);
            Vector3 spawnPosition = transform.position + new Vector3 (randomX, 0f, randomZ);
            //It sums two vectors - vector pointing to where spawner is and a new random spawn location.
            
            GameObject newObject = Instantiate(prefabToSpawn, spawnPosition, Quaternion.identity); 
            //spawnPosition - spawn it in the earlier picked random spawn position
            //Quarternion - default/no rotation

            spawnedObjects.Add(newObject); //Coin will get counted.
            
            timer = 0f; //Reseting countdown.
        }

        // TODO: periodically remove destroyed (null) entries from `spawnedObjects`
        // - Destroy(obj) does not remove obj from a List<GameObject> for you, and
        // a stale full list will block new spawns even after objects are gone.

        //Since Destroy(obj) makes it only a null entry, we need to remove them somehow else.
        //But it can't be at the bottom - because it needs to happen before we are counting the spawned objects.

    }
}
