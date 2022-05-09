using UnityEngine;

// Handles the universe
public class Universe : MonoBehaviour
{
    public GameObject player;
    public GameObject attractorPrefab;
    public Material blank;

    private Rigidbody[] attractors = new Rigidbody[0];
    private Vector3[] forces = new Vector3[0];

    private int numGenerate = 410;
    private float worldSize = 60;
    private Vector2 massRange = new Vector2(0.1f, 25.0f);

    private void Start()
    {
        PopulateUniverse();
    }

    private void FixedUpdate()
    {
        if (attractors.Length > 0)
        {
            ApplyGravity();
        }
    }

    // Applies gravity to each attractor object
    private void ApplyGravity()
    {
        for (int i = 0; i < attractors.Length - 1; i++)
        {
            Rigidbody a = attractors[i];

            for (int j = i + 1; j < attractors.Length; j++)
            {
                Vector3 force = Utility.CalcForce(a, attractors[j]);
                forces[i] += force;
                forces[j] += -force;
            }

            a.AddForce(forces[i]);
        }

        attractors[attractors.Length - 1].AddForce(forces[forces.Length - 1]);

        forces = new Vector3[forces.Length];
    }

    // Adds a given attractor to the array
    public void AddAttractor(Rigidbody attractor)
    {
        Rigidbody[] newAttractors = new Rigidbody[attractors.Length + 1];
        Vector3[] newForces = new Vector3[attractors.Length + 1];
        for (int i = 0; i < attractors.Length; i++)
        {
            newAttractors[i] = attractors[i];
        }
        newAttractors[attractors.Length] = attractor;

        attractors = newAttractors;
        forces = newForces;
    }

    // Halts the velocity of each attractor
    public void Halt()
    {
        foreach (Rigidbody attractor in attractors)
        {
            attractor.velocity = Vector3.zero;
            attractor.angularVelocity = Vector3.zero;
        }
    }

    // Pauses time
    public bool TimeStop(bool isStopped)
    {
        Time.timeScale = 1;
        if (isStopped)
            Time.timeScale = 0;

        return !isStopped;
    }

    // Deletes all attractors
    public void DeleteAll()
    {
        foreach (Rigidbody attractor in attractors)
        {
            if (attractor != null)
                Destroy(attractor.gameObject);
        }

        attractors = new Rigidbody[0];
        forces = new Vector3[0];
    }

    // Populates the universe with attractors
    public void PopulateUniverse()
    {
        player.transform.position = new Vector3(0, 0, 0);
        DeleteAll();

        attractors = new Rigidbody[numGenerate];
        forces = new Vector3[numGenerate];

        for (int i = 0; i < numGenerate; i++)
        {
            float x = Random.Range(-worldSize, worldSize);
            float y = Random.Range(-worldSize, worldSize);
            float z = Random.Range(-worldSize, worldSize);
            Vector3 pos = new Vector3(x, y, z);

            float mass = Random.Range(massRange.x, massRange.y);
            float diameter = Utility.CalcDiameter(mass);

            Material mat = new Material(blank);
            Color32 randomColor = new Color32(128, 128, (byte)(Random.Range(0, 256)), 255);
            mat.color = randomColor;

            GameObject thisAttractor = Instantiate(attractorPrefab, pos, Quaternion.identity);

            thisAttractor.transform.localScale = new Vector3(diameter, diameter, diameter);
            thisAttractor.GetComponent<Renderer>().material = mat;

            attractors[i] = thisAttractor.GetComponent<Rigidbody>();
        }
    }
}
