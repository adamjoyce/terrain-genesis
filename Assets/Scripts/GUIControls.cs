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
        if (surface.type != (NoiseMethodType)noiseType.value)
            surface.type = (NoiseMethodType)noiseType.value;
        surface.Refresh();
    }

    public void updateFields() {
        Vector3 currentOffset = new Vector3(int.Parse(xOffset.text), int.Parse(yOffset.text), int.Parse(zOffset.text));
        if (currentOffset != surface.noiseOffset) {
            surface.noiseOffset = currentOffset;
        }
        surface.Refresh();
    }
}
