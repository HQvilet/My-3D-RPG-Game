using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

namespace AdvanceFSM
{
    public class StateMachineDebugger : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI textMesh;
        public void SetState(IState state)
        {
            textMesh.text = state.GetType().ToString().Split(".").Last();
        }
    }

}
