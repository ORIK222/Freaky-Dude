using UnityEngine;


public class Parallax : MonoBehaviour
{
    [SerializeField] private Vector2 parallaxEffectMultiplier;
    [SerializeField] private bool infiniteHorizontal;
    [SerializeField] private bool infiniteVertical;
   
       private Transform cameraTransform;
       private Vector3 lastCameraPosition;
       private float textureUnitSizeX;
       private float textureUnitSizeY;
       private void Start()
       {
           cameraTransform = Camera.main.transform;
           lastCameraPosition = cameraTransform.position;
           if (GetComponent<SpriteRenderer>())
           {
               Sprite sprite = GetComponent<SpriteRenderer>().sprite;
               Texture2D texture = sprite.texture;
               textureUnitSizeX = texture.width * transform.localScale.x / sprite.pixelsPerUnit;
               textureUnitSizeY = texture.height * transform.localScale.y / sprite.pixelsPerUnit;
           }
       }
   
       private void LateUpdate() {
           Vector3 deltaMovement = Vector3.left;
           transform.position += new Vector3
               (deltaMovement.x * Time.deltaTime * parallaxEffectMultiplier.x, deltaMovement.y * Time.deltaTime * parallaxEffectMultiplier.y, 0);
           lastCameraPosition = cameraTransform.position;
   
           if (infiniteHorizontal) {
               if (Mathf.Abs(cameraTransform.position.x - transform.position.x) >= textureUnitSizeX) 
               {
                   float offsetPositionX = (cameraTransform.position.x - transform.position.x) % textureUnitSizeX;
                   transform.position = new Vector3(cameraTransform.position.x + offsetPositionX, transform.position.y, 2);
               }
           }
   
           if (infiniteVertical) {
               if (Mathf.Abs(cameraTransform.position.y - transform.position.y) >= textureUnitSizeY) {
                   float offsetPositionY = (cameraTransform.position.y - transform.position.y) % textureUnitSizeY;
                   transform.position = new Vector3(transform.position.x, cameraTransform.position.y + offsetPositionY);
               }
           }
       }

       public void SetSpeed(float speed)
       {
           parallaxEffectMultiplier.x = speed;
       }
       
}
