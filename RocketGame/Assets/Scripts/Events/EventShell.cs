using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EventShell : MonoBehaviour {

	public virtual void startEvent() {

	}

	public virtual void disableEvent() {

	}

	public virtual void resetEvent() {

	}

	public virtual bool isRunning() {
		return false;
	}

	public virtual void togglePause(bool paused) {

	}

}
