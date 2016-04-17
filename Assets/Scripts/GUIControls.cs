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
    public GameObject surface;

	public void updateValues() {
        SurfaceGenerator script = surface.GetComponent<SurfaceGenerator>();

        // Sliders.
        if (script.resolution != resolution.value) {
            script.resolution = (int)resolution.value;
        }
        if (script.frequency != frequency.value)
            script.frequency = frequency.value;
        if (script.octaves != octaves.value)
            script.octaves = (int)octaves.value;
        if (script.lacunarity != lacunarity.value)
            script.lacunarity = lacunarity.value;
        if (script.persistence != persistence.value)
            script.persistence = persistence.value;
        if (script.dimensions != dimensions.value)
            script.dimensions = (int)dimensions.value;
        if (script.scaleFactor != scale.value)
            script.scaleFactor = scale.value;
        
        // Dropdown menus.
        if (script.type != (NoiseMethodType)noiseType.value) {
            script.type = (NoiseMethodType)noiseType.value;
        }



        script.Refresh();
    }
}
