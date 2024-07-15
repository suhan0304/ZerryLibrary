using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class CompassManager : MonoBehaviour
{
    public static CompassManager Instance;
    public RawImage CompassImage;

    public RectTransform CompassObjectiveParent;
    public GameObject CompassObjectivePrefab;
    private readonly List<CompassObjective> _currentObjectives = new List<CompassObjective>();

    private IEnumerator Start() {
        WaitForSeconds updateDelay = new WaitForSeconds(1);

        while (enabled) {
            SortCompassObjectives();
            yield return updateDelay;
        }
    }

    private void SortCompassObjectives() {
        if (PlayerController.Instance == null) { return; }

        CompassObjective[] orderedObjectives = _currentObjectives.Where(o => o.WorldGameObject != null)
            .OrderByDescending(o => Vector3.Distance(PlayerController.Instance.transform.position, o.WorldGameObject.position)).ToArray();

        for (int i = 0; i < orderedObjectives.Length; i++) {
            orderedObjectives[i].UpdateUiIndex(i);
        }
    }

    public void AddObjectiveForObject(GameObject compassObjectiveGameObject, Color color, Sprite sprite) =>
        _currentObjectives.Add(Instantiate(CompassObjectivePrefab, CompassObjectiveParent, false).GetComponent<CompassObjective>().Configure(compassObjectiveGameObject, color, sprite));

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (Instance != null)
        {
            Destroy(gameObject);
        }
    }

    private void LateUpdate() => UpdateCompassHeading();

    private void UpdateCompassHeading()
    {
        if (PlayerController.Instance == null) {
            return;
        }

        Vector2 compassUvPosition = Vector2.right * (PlayerController.Instance.transform.rotation.eulerAngles.y / 360);

        CompassImage.uvRect = new Rect(compassUvPosition, Vector2.one);
    }

    public void RemoveCompassObjective(CompassObjective compassObjective) {
        _currentObjectives.Remove(compassObjective);
        Destroy(compassObjective.gameObject);
    }
}