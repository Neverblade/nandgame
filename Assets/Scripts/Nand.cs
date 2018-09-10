using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nand : Circuit {

    protected override void FastEvaluate() {
        output[0] = !input[0] || !input[1];
    }
}
