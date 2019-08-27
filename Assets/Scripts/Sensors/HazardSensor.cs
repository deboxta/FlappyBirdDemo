using UnityEngine;

namespace Game
{
    public class HazardSensor : MonoBehaviour
    {
        public event SensorEventHandler OnHit;
        //lors de l<utilisation de cette fonction, on a aussi besoin d<un colider dans le gameobject de unity
        private void OnTriggerEnter2D(Collider2D other)
        {
            //description de la collision
            var stimuli = other.gameObject.GetComponent<HazardStimuli>();
            if (stimuli != null)
            {
                //DO A THING
                //Debug.Log("Collision with" + other.gameObject.name);

                //Pour v/rifier s<il y a un parent ou non
                var parent = other.transform.parent;
                var gameObject = parent != null ? parent.gameObject : other.gameObject;
                
                if (OnHit != null) OnHit(gameObject);
            }
        }
    }
}