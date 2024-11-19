using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Make sure that any child object that needs to be tracked by a ChildTracker
/// has a method to set a reference to that tracker
/// </summary>
public interface ITrackedChild
{
    void SetTracker(ChildTracker manager);
}
