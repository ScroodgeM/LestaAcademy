using System;
using System.Collections.Generic;
using UnityEngine;

namespace LestaAcademyDemo.DesignPrinciples.DelegationPrinciples.Correct
{
    public struct UnitType
    {
        public string type;
        public int level;
        public int skin;

        public static bool operator ==(UnitType a, UnitType b)
        {
            if (a.type != b.type) { return false; }
            if (a.level != b.level) { return false; }
            if (a.skin != b.skin) { return false; }
            return true;
        }

        public static bool operator !=(UnitType a, UnitType b) => (a == b) == false;
    }

    public class GameControiller
    {
        private UnitViewPool unitViewPool;

        public UnitController CreateUnit(UnitType unitType)
        {
            UnitController unitController = new UnitController(unitType);

            CreateUnitView(unitType, unitController);

            return unitController;
        }

        private void CreateUnitView(UnitType unitType, UnitController unitController)
        {
            UnitView unitView = unitViewPool.TryGetView(unitType);

            if (unitView == null)
            {
                unitView = new UnitView(unitType);
            }

            unitView.Initialize(unitController);
        }
    }

    public class UnitController
    {
        public event Action OnFire = () => { };
        public event Action OnMove = () => { };

        private UnitType unitType;

        public UnitController(UnitType unitType)
        {
            this.unitType = unitType;
        }
    }

    public class UnitViewPool
    {
        private List<UnitView> unitViews;

        public UnitView TryGetView(UnitType unitType)
        {
            for (int i = 0; i < unitViews.Count; i++)
            {
                UnitView unitView = unitViews[i];
                if (unitView.UnitType == unitType)
                {
                    unitViews.RemoveAt(i);
                    return unitView;
                }
            }

            return null;
        }
    }

    public class UnitView
    {
        private UnitType unitType;
        private UnitAnimator unitAnimator;

        public UnitType UnitType => unitType;

        public UnitView(UnitType unitType)
        {
            this.unitType = unitType;

            // apply some exterior based on unit type
        }

        public void Initialize(UnitController unitController)
        {
            unitAnimator.Initialize(unitController);
        }
    }

    public class UnitAnimator
    {
        private Animator animator;

        public void Initialize(UnitController unitController)
        {
            unitController.OnFire += PlayFireAnimation;
            unitController.OnMove += PlayMoveAnimation;
        }

        private void PlayFireAnimation()
        {
            animator.Play("fire");
        }

        private void PlayMoveAnimation()
        {
            animator.Play("move");
        }
    }
}
