using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Circuit : MonoBehaviour {

    [Header("Internal State")]
    public int inputSize;
    public int outputSize;
    public bool[] input;
    public bool[] output;

    [Header("External State")]
    public Connection[] connections;

    [Header("Evaluation")]
    public bool fastEvaluate;
    public Circuit inputCircuit;
    public Circuit outputCircuit;

    /* Takes in input and processes it. */
    public void AcceptInput(int index, bool value) {
        if (index < 0 || index >= inputSize) {
            print("Invalid input length.");
            return;
        }

        input[index] = value;
        if (fastEvaluate) {
            FastEvaluate();
        } else {
            Evaluate();
        }

        DistributeOutput();
    }

    /* Takes the current output and distributes it to all its outgoing connections. */
    public void DistributeOutput() {
        foreach (Connection c in connections) {
            c.connectingCircuit.AcceptInput(c.inputIndex, output[c.outputIndex]);
        }
    }

    void Start() {
        input = new bool[inputSize];
        output = new bool[outputSize];
    }

    /* Runs the circuit on the current input and update the output. */
    protected void Evaluate() {
        for (int i = 0; i < inputSize; i++) {
            inputCircuit.AcceptInput(i, input[i]);
        }
        output = outputCircuit.output;
    }

    /* Same as Evaluate(), but does so with a closed-form solution. */
    protected virtual void FastEvaluate() {}

    // Represents a "wire" connection between two circuits.
    [System.Serializable]
    public struct Connection {
        // What circuit this one connects to
        public Circuit connectingCircuit;

        // The output index that's being emitted
        public int outputIndex;

        // The input index it's being sent to
        public int inputIndex;

        public Connection(Circuit connectingCircuit, int outputIndex, int inputIndex) {
            this.connectingCircuit = connectingCircuit;
            this.outputIndex = outputIndex;
            this.inputIndex = inputIndex;
        }
    }
}
