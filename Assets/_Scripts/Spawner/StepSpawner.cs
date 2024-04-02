using UnityEngine;
namespace Step
{
    public class StepSpawner : Spawner
    {
        // [SerializeField] private static StepSpawner instance;
        static StepSpawner _Instance;
        public static StepSpawner Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = FindObjectOfType<StepSpawner>();
                }
                return _Instance;
            }
        }

        protected override void LoadComponents()
        {
            base.LoadComponents();
            // Singleton
            if (_Instance != null && _Instance != this)
            {
                Debug.Log("Instance already exists, destroying object!");
            }
            else
            {
                _Instance = this;
            }
        }
    }
}
