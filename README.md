# Mikko

Editor/スマホのタッチ動作を抽象化したラッパー

## 使い方

```cs
using UnityEngine;
using System.Collections;
using GuP;

public class Hoge : MonoBehaviour {
    void Update () {
        ITouch t = Mikko.input.touch;
        print(t.position);
        print(t.deltaPosition);
        print(t.info); // None, Began, Moved, Ended, Stationary
    }
}
```