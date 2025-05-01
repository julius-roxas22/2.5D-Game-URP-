using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IndieGamePractice
{
    public class DeathAnimationManager : Singleton<DeathAnimationManager>
    {
        private DeathLoader loader;
        private List<RuntimeAnimatorController> candidates = new List<RuntimeAnimatorController>();

        private void setUpDeathLoader()
        {
            if (null == loader)
            {
                GameObject obj = Instantiate(Resources.Load("DeathLoader", typeof(GameObject))) as GameObject;
                loader = obj.GetComponent<DeathLoader>();
            }
        }

        public RuntimeAnimatorController _GetDeathController(BodyPart damagedPart, AttackInfo info)
        {
            candidates.Clear();
            setUpDeathLoader();

            foreach (DeathData data in loader._DeathData)
            {
                if (info._DeathType == data._DeathType)
                {
                    if (info._DeathType != DeathType.None)
                    {
                        candidates.Add(data._DeathAnimationController);
                    }
                    else if (!info._MustCollide)
                    {
                        candidates.Add(data._DeathAnimationController);
                    }
                    else
                    {
                        foreach (BodyPart part in data._DamagedParts)
                        {
                            if (part == damagedPart)
                            {
                                candidates.Add(data._DeathAnimationController);
                                break;
                            }
                        }
                    }
                }
            }

            int index = Random.Range(0, candidates.Count);
            return candidates[index];
        }
    }
}