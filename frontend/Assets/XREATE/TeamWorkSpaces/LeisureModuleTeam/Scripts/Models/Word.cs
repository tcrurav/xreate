using UnityEngine;
using TMPro;

namespace TeamWorkSpaces.LeisureModule
{
    public class Word : MonoBehaviour
    {
        // 🔗 References
        private TextMeshPro textMeshPro;
        private BoxCollider boxCollider;

        // 🌟 States
        private string wordText;
        public bool isBeingAbsorbed = false; // Prevent multiple absorptions
        public bool isCorrectWord = false; // ✅ Indicates if this word belongs to a correct answer

        // 🎨 Visual Effects
        public Material defaultMaterial;
        public Material grabbedMaterial;
        public Material correctMaterial; // Material when the word is correct
        public Transform planeTransform;

        private void Awake()
        {
            // 🔎 Get component references
            textMeshPro = GetComponentInChildren<TextMeshPro>();
            boxCollider = GetComponent<BoxCollider>();

            if (textMeshPro == null)
            {
                Debug.LogWarning("[Word] ⚠️ TextMeshPro component is missing.");
            }

            if (boxCollider == null)
            {
                Debug.LogWarning("[Word] ⚠️ BoxCollider component is missing.");
            }
        }

        private void Start()
        {
            UpdateColliderSize(); // 📏 Adjust collider to fit text
            UpdatePlaneSize(); // 🖼 Adjust plane to match text
        }

        private void OnEnable()
        {
            UpdateColliderSize();
            UpdatePlaneSize();
        }

        // 📜 Set the word text and convert it to vertical orientation
        public void SetWord(string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                Debug.LogWarning("[Word] ⚠️ SetWord received an empty or null string.");
                return;
            }

            wordText = text;
            string verticalText = string.Join("\n", wordText.ToCharArray());

            if (textMeshPro != null)
            {
                textMeshPro.text = verticalText;
                textMeshPro.ForceMeshUpdate();
                // Debug.Log($"[Word] ✅ Word set to: {verticalText}");
                Debug.Log($"[Word] ✅ Word set to: {wordText}"); // ✅ Esto imprime en horizontal
            }

            UpdateColliderSize();
            UpdatePlaneSize();
        }

        // 🔍 Returns the word text without new lines
        public string GetWord()
        {
            return wordText;
        }

        // ✋ Visual feedback when grabbed
        public void OnGrabbed()
        {
            UpdateVisuals(grabbedMaterial);
        }

        // ✋ Visual feedback when released
        public void OnReleased()
        {
            UpdateVisuals(defaultMaterial);
        }

        // ✅ Set the word as correct or incorrect
        public void SetWordAsCorrect(bool isCorrect)
        {
            isCorrectWord = isCorrect;
            ApplyCorrectMaterial();
        }

        // 🎨 Applies correct material if the word is correct
        private void ApplyCorrectMaterial()
        {
            if (isCorrectWord)
            {
                UpdateVisuals(correctMaterial);
            }
        }

        // 📏 Update the collider size to fit the rendered text
        private void UpdateColliderSize()
        {
            if (textMeshPro == null || boxCollider == null) return;

            textMeshPro.ForceMeshUpdate();
            var bounds = textMeshPro.textBounds;
            boxCollider.size = new Vector3(bounds.size.x, bounds.size.y, 0.1f); // Make sure depth isn't zero
            boxCollider.center = bounds.center;
        }

        // 🖼 Adjust the plane size to match the text size
        private void UpdatePlaneSize()
        {
            if (planeTransform != null && textMeshPro != null)
            {
                var bounds = textMeshPro.textBounds;
                planeTransform.localScale = new Vector3(bounds.size.x, 1, bounds.size.y);
            }
        }


        // 🎭 Change the material to reflect the current state
        private void UpdateVisuals(Material material)
        {
            var renderer = GetComponentInChildren<MeshRenderer>();
            if (renderer != null && material != null)
            {
                renderer.material = material;
            }
        }
    }
}
