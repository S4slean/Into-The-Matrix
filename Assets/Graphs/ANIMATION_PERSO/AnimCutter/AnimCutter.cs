using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class AnimCutter : MonoBehaviour
{
	public AnimationClip originalClip;
	public AnimationClip cloneClip;
	private AnimationCurve storedCurve;
	public float animDelay = 0.1f;


    [ContextMenu("GetInfo")]
    public void GetInfo()
    {
        storedCurve = AnimationUtility.GetEditorCurve(originalClip, AnimationUtility.GetCurveBindings(originalClip)[0]);
        Debug.Log(storedCurve.keys[storedCurve.length - 1].time);
        Debug.Log(storedCurve.keys[storedCurve.length - 1].time / animDelay);
    }

    [ContextMenu("SpliceAnim")]
	public void SliceAnimation()
	{
        for (int j = 0; j < AnimationUtility.GetCurveBindings(originalClip).Length; j++)
        {
            storedCurve = AnimationUtility.GetEditorCurve(originalClip, AnimationUtility.GetCurveBindings(originalClip)[j]);
            AnimationCurve recaCurve = new AnimationCurve();
            
            for (int i = 0; i < storedCurve.keys[storedCurve.length - 1].time / animDelay + 1; i++)
            {
                recaCurve.AddKey(i * animDelay, storedCurve.Evaluate(i * animDelay));
                AnimationUtility.SetKeyLeftTangentMode(recaCurve, i, AnimationUtility.TangentMode.Constant);
                AnimationUtility.SetKeyRightTangentMode(recaCurve, i, AnimationUtility.TangentMode.Constant);
                Debug.Log("curve number: " + j +" , key" + i);
            }
            AnimationUtility.SetAnimationEvents(cloneClip, originalClip.events);
            AnimationUtility.SetEditorCurve(cloneClip, AnimationUtility.GetCurveBindings(originalClip)[j], recaCurve);
        }
	}
}
