using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*! \brief A script that controls how an equippable object sways.
 *
 *  Independent
 */
public class SwayForEquippable : MonoBehaviour
{
    [Header("Player")]
    public Transform playerHead; //!< The player head transform.

    [Header("Sway")]
    public float swaySpeed = 25f; //!< The sway speed of the equippable.

    public Vector3 equippablePositionOffset; //!< The position offset from the player head.
    public Vector3 equippableRotationOffset; //!< The rotation offset from the player head.

    void Update()
    {
        gameObject.transform.position = new Vector3(playerHead.TransformPoint(equippablePositionOffset).x, (playerHead.position + equippablePositionOffset).y, playerHead.TransformPoint(equippablePositionOffset).z);
        gameObject.transform.rotation = Quaternion.Slerp(gameObject.transform.rotation, Quaternion.Euler(playerHead.rotation.eulerAngles + equippableRotationOffset), swaySpeed * Time.deltaTime);
    }
}
