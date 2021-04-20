using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*! \brief A script that controls how a character rotates their head to look at a transform.
 *
 *  [Reusable Script]
 */
public class CharacterLook : MonoBehaviour
{
    [Header("Assets")]
    public Transform characterHead; //!< The character head transform to rotate.
    public Transform target; //!< The target transform to look at.

    [Header("Inputs")]
    public Vector3 targetOffset = new Vector3(0f, 0f, 0f); //!< The target offset.

    void LateUpdate()
    {
        float normalizedHeadZ = characterHead.rotation.eulerAngles.z;
        characterHead.LookAt(target.position + targetOffset);
        characterHead.rotation = Quaternion.Euler(characterHead.rotation.eulerAngles.x, characterHead.rotation.eulerAngles.y, normalizedHeadZ);

        gameObject.transform.LookAt(target.position);
        Vector3 normalizedPrefab = gameObject.transform.rotation.eulerAngles;
        normalizedPrefab.x = 0;
        normalizedPrefab.z = 0;
        gameObject.transform.rotation = Quaternion.Euler(normalizedPrefab);
    }
}
