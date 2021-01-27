using System;
using UnityEngine;

public interface IMyGameInput
{
    Vector2 MovementCommand { get; }
    event Action OnJumpCommand;
    event Action OnFireCommand;
    event Action OnUseSpecialSkillCommand;
}

