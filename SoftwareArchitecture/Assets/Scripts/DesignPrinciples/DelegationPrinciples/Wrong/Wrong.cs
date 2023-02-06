using System;
using System.Collections.Generic;
using UnityEngine;

namespace LestaAcademyDemo.DesignPrinciples.DelegationPrinciples.Wrong
{
    public struct UnitType
    {
        public string type;
        public int level;
        public int skin;
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
                // bad solution here
                if (unitView.UnitType.type != unitType.type) { continue; }
                if (unitView.UnitType.level != unitType.level) { continue; }
                if (unitView.UnitType.skin != unitType.skin) { continue; }
                unitViews.RemoveAt(i);
                return unitView;
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
            // bad solution here
            unitController.OnFire += UnitController_OnFire;
            unitController.OnMove += UnitController_OnMove;
        }

        private void UnitController_OnMove()
        {
            unitAnimator.PlayMoveAnimation();
        }

        private void UnitController_OnFire()
        {
            unitAnimator.PlayFireAnimation();
        }
    }

    public class UnitAnimator
    {
        private Animator animator;

        public void PlayFireAnimation()
        {
            animator.Play("fire");
        }

        public void PlayMoveAnimation()
        {
            animator.Play("move");
        }
    }
}
