using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
namespace com.dotdothorse.zoochef
{
    public class AnimalController : MonoBehaviour
    {
        [SerializeField] private List<AnimalAnimation> _animals;

        public void StartWalking()
        {
            foreach (AnimalAnimation animal in _animals)
            {
                animal.Walk();
            }
        }

        public void StartEating()
        {
            foreach (AnimalAnimation animal in _animals)
            {
                animal.Eat();
            }
        }

        public void StartIdling()
        {
            foreach (AnimalAnimation animal in _animals)
            {
                animal.Idle();
            }
        }
    }
}