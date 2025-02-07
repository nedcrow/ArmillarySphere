using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmillarySphere : MonoBehaviour
{
    public GameObject Outlines;
    public GameObject SaYouCircle;
    public GameObject SaYouCircleClip;
    public GameObject SaYouCircleLeft;
    public GameObject SaYouCircleRight;
    public List<Transform> SaYous;

    Transform subTarget;

    int currentStep=0;
    float rotationSpeed = 15f;

    private void Start()
    {
        SaYous = new List<Transform>();
        SaYous.Add(SaYouCircle.transform);
        SaYous.Add(SaYouCircleClip.transform);
        SaYous.Add(SaYouCircleLeft.transform);
        SaYous.Add(SaYouCircleRight.transform);

        subTarget = new GameObject().GetComponent<Transform>();
        subTarget.transform.position = Vector3.zero;
    }

    //private void Update()
    //{
    //    //transform.Rotate(0, 0.001f, 0);
    //    subTarget.transform.Rotate(0, 0.5f, 0);
    //}

    public void StepUp()
    {
        if (currentStep > 6) { currentStep = 0; return; }
        currentStep++;

        StopAllCoroutines();

        switch (currentStep)
        {
            case 1:
                StartCoroutine(StepOne(Outlines.transform));
                break;
            case 2:
                StartCoroutine(StepTwo(transform));
                break;
            case 3:
                Outlines.transform.localRotation = Quaternion.Euler(transform.localEulerAngles.x, 0f, 0f);
                subTarget.transform.localEulerAngles = Vector3.right * 53;
                StartCoroutine(StepThree(subTarget));
                break;
            case 4:
                foreach (Transform sayou in SaYous) sayou.transform.localEulerAngles = Vector3.right * 53;
                subTarget.transform.localRotation = SaYous[0].transform.localRotation;
                subTarget.transform.Rotate(0, 45f, 0);
                //SaYous[0].transform.Rotate(0, 45f, 0);
                StartCoroutine(StepFour(subTarget));
                break;
            default:
                // reset
                break;
        }

    }

    IEnumerator StepOne(Transform target)
    {
        float dist = Quaternion.Angle(SaYous[0].transform.localRotation, target.transform.localRotation);
        while (dist > 0.05f)
        {
            float currentAngle = SaYous[0].transform.localEulerAngles.x;
            dist = Quaternion.Angle(SaYous[0].transform.localRotation, target.transform.localRotation);

            float newAngle = Mathf.LerpAngle(currentAngle, target.localEulerAngles.x, rotationSpeed * Time.deltaTime);

            foreach (Transform sayou in SaYous) sayou.transform.localRotation = Quaternion.Euler(newAngle, 0f, 0f);

            yield return new WaitForSeconds(0.04f);
        }
        foreach (Transform sayou in SaYous) sayou.transform.localRotation = Quaternion.Euler(target.localEulerAngles.x, 0f, 0f);
    }

    IEnumerator StepTwo(Transform target)
    {
        foreach (Transform sayou in SaYous) sayou.transform.localRotation = Quaternion.Euler(Outlines.transform.localEulerAngles.x, 0f, 0f);

        float dist = Quaternion.Angle(SaYous[0].transform.localRotation, target.transform.localRotation);
        while (dist > 0.05f)
        {
            float currentAngle = SaYous[0].transform.localEulerAngles.x;
            dist = Quaternion.Angle(SaYous[0].transform.localRotation, target.transform.localRotation);

            float newAngle = Mathf.LerpAngle(currentAngle, target.localEulerAngles.x, rotationSpeed * Time.deltaTime);

            Outlines.transform.localRotation = Quaternion.Euler(newAngle, 0f, 0f);
            foreach (Transform sayou in SaYous) sayou.transform.localRotation = Quaternion.Euler(newAngle, 0f, 0f);

            yield return new WaitForSeconds(0.04f);
        }
        Outlines.transform.localRotation = Quaternion.Euler(target.localEulerAngles.x, 0f, 0f);
        foreach (Transform sayou in SaYous) sayou.transform.localRotation = Quaternion.Euler(target.localEulerAngles.x, 0f, 0f);
    }

    IEnumerator StepThree(Transform target)
    {
        foreach (Transform sayou in SaYous) sayou.transform.localRotation = Quaternion.Euler(Outlines.transform.localEulerAngles.x, 0f, 0f);

        float dist = Quaternion.Angle(SaYous[0].transform.localRotation, target.transform.localRotation);
        while (dist > 0.1f)
        {
            float currentAngle = SaYous[0].transform.localEulerAngles.x;
            dist = Quaternion.Angle(SaYous[0].transform.localRotation, target.transform.localRotation);

            float newAngle = Mathf.LerpAngle(currentAngle, target.localEulerAngles.x, rotationSpeed * Time.deltaTime);

            foreach (Transform sayou in SaYous) sayou.transform.localRotation = Quaternion.Euler(newAngle, 0f, 0f);

            yield return new WaitForSeconds(0.04f);
        }
        foreach (Transform sayou in SaYous) sayou.transform.localRotation = Quaternion.Euler(target.localEulerAngles.x, 0f, 0f);
    }

    IEnumerator StepFour(Transform target)
    {
        float dist = Quaternion.Angle(SaYous[0].transform.localRotation, target.transform.localRotation);

        while (dist > 1f)
        {
            dist = Quaternion.Angle(SaYous[0].transform.localRotation, target.transform.localRotation);
            
            float lerpValue = Mathf.LerpAngle(SaYous[0].transform.localEulerAngles.y, target.transform.localEulerAngles.y, rotationSpeed * Time.deltaTime);


            foreach (Transform sayou in SaYous) sayou.transform.Rotate(0, lerpValue, 0);

            yield return new WaitForSeconds(0.04f);
        }
        foreach (Transform sayou in SaYous) sayou.transform.localRotation = target.localRotation;
    }
}
