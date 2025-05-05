using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace G2048_3D.Core
{
    public class SceneLoader
    {
        public async UniTask Load(LoadSceneParameters parameters)
        {
            if (SceneManager.GetActiveScene().name == parameters.Name && !parameters.CanReload)
            {
                parameters.OnLoaded?.Invoke();
                return;
            }

            AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(parameters.Name);
            await UniTask.WaitUntil(() => asyncOperation.isDone);

            parameters.OnLoaded?.Invoke();
        }
    }
}
    
