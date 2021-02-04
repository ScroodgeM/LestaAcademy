using UnityEngine;

public class PresentationGUI : MonoBehaviour
{
    public Transform Sun;
    public Transform Ship;
    public Transform ShipEngines;
    public Material ShipMaterial;
    public Material ShipEnginesMaterial;

    private void Start()
    {
        Application.runInBackground = true;

        Transform ship = Instantiate(Ship, Vector3.zero, Quaternion.identity);
        ship.GetComponent<Renderer>().material = new Material(ShipMaterial);

        Transform shipEngines = Instantiate(ShipEngines);
        shipEngines.GetComponent<Renderer>().material = new Material(ShipEnginesMaterial);

        ship.gameObject.AddComponent<MeshCollider>();
        MouseClickToShader mcts1 = ship.gameObject.AddComponent<MouseClickToShader>();
        mcts1.ShieldMaterials = new Material[] { ship.GetComponent<Renderer>().material, shipEngines.GetComponent<Renderer>().material };

        shipEngines.gameObject.AddComponent<MeshCollider>();
        MouseClickToShader mcts2 = shipEngines.gameObject.AddComponent<MouseClickToShader>();
        mcts2.ShieldMaterials = new Material[] { ship.GetComponent<Renderer>().material, shipEngines.GetComponent<Renderer>().material };

        shipEngines.parent = ship;
        shipEngines.localPosition = Vector3.zero;
        shipEngines.localRotation = Quaternion.identity;

        Camera.main.transform.rotation = Quaternion.Euler(0f, 90f, 0f);
        Camera.main.transform.position = -Camera.main.transform.forward * 300f;

        Vector3 sunPosition = new Vector3(-1000f, 200f, -1000f);
        Transform sun = Instantiate(Sun, sunPosition, Quaternion.LookRotation(ship.position - sunPosition));
        sun.localScale = Vector3.one * 100f;
    }

    private void Update()
    {
        Vector3 camMove = Vector3.zero;
        if (Input.GetKey(KeyCode.W)) { camMove += Vector3.right; }
        if (Input.GetKey(KeyCode.S)) { camMove -= Vector3.right; }
        if (Input.GetKey(KeyCode.D)) { camMove -= Vector3.up; }
        if (Input.GetKey(KeyCode.A)) { camMove += Vector3.up; }
        if (Input.GetKey(KeyCode.Z)) { camMove -= Vector3.forward; }
        if (Input.GetKey(KeyCode.X)) { camMove += Vector3.forward; }
        float rotationSpeed = Input.GetKey(KeyCode.LeftShift) == true ? 1f : 30f;
        Camera.main.transform.RotateAround(Vector3.zero, Camera.main.transform.TransformDirection(camMove), Time.deltaTime * rotationSpeed);
    }
}
