using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GUIControls : MonoBehaviour {

    public Slider resolution;
    public Slider frequency;
    public Slider octaves;
    public Slider lacunarity;
    public Slider persistence;
    public Slider dimensions;
    public Slider scale;

    public Dropdown noiseType;

    public InputField xOffset;
    public InputField yOffset;
    public InputField zOffset;
    public InputField xRotation;
    public InputField yRotation;
    public InputField zRotation;

    public Toggle damping;
    public Toggle scaleWithColour;
    public Toggle showNormals;
    public Toggle showWireframe;

    public SurfaceGenerator surface;

    public void updateSliders() {
        if (surface.resolution != resolution.value) {
            surface.resolution = (int)resolution.value;
        }
        if (surface.frequency != frequency.value) {
            surface.frequency = frequency.value;
        }
        if (surface.octaves != octaves.value) {
            surface.octaves = (int)octaves.value;
        }
        if (surface.lacunarity != lacunarity.value) {
            surface.lacunarity = lacunarity.value;
        }
        if (surface.persistence != persistence.value) {
            surface.persistence = persistence.value;
        }
        if (surface.dimensions != dimensions.value) {
            surface.dimensions = (int)dimensions.value;
        }
        if (surface.scaleFactor != scale.value) {
            surface.scaleFactor = scale.value;
        }
        surface.Refresh();
    }

    public void updateDropdowns() {
        surface.type = (NoiseMethodType)noiseType.value;
        surface.Refresh();
    }

    public void updateOffsetFields() {
        Vector3 currentOffset = new Vector3(int.Parse(xOffset.text), int.Parse(yOffset.text), int.Parse(zOffset.text));
        surface.noiseOffset = currentOffset;
        surface.Refresh();
    }

    public void updateRotationFields() {
        Vector3 currentRotation = new Vector3(int.Parse(xRotation.text), int.Parse(yRotation.text), int.Parse(zRotation.text));
        surface.rotation = currentRotation;
        surface.Refresh();
    }

    public void updateToggles() {
        if (surface.damping != damping.isOn)
            surface.damping = damping.isOn;
        if (surface.scaleWithColour != scaleWithColour.isOn)
            surface.scaleWithColour = scaleWithColour.isOn;
        if (surface.displayNormals != showNormals.isOn)
            surface.displayNormals = showNormals.isOn;
        surface.Refresh();
    }
}
