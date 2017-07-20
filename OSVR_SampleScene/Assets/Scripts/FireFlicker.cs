// Copyright 2017 Razer, Inc.
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using UnityEngine;

public class FireFlicker : MonoBehaviour {
    private float nextFlicker = 0f;

    private float defaultNoFlickerIntensity;
    private Vector3 defaultPosition;

    public bool Enabled = false;
    public float FlickerIntensityRange = 0.25f;
    public float FlickerTranslationRange = 0.3f;
    public float FlickerRateSecondsMax = 0.08f;
    public float BaseIntensity = 1.2f;

    public Light PointLight;

	void Start ()
    {
        defaultNoFlickerIntensity = PointLight.intensity;
        defaultPosition = PointLight.transform.position;
	}

	void Update ()
    {
        if (Enabled && Time.time > nextFlicker)
        {
            PointLight.intensity = BaseIntensity + Random.Range(-FlickerIntensityRange, FlickerIntensityRange);

            PointLight.transform.position = defaultPosition + new Vector3(RandomTranslationOneAxis(), RandomTranslationOneAxis(), RandomTranslationOneAxis());

            nextFlicker = Time.time + Random.value * FlickerRateSecondsMax;
        }

        if (Input.GetButtonDown("Toggle Fire Flicker"))
        {
            Enabled = !Enabled;

            if (!Enabled)
            {
                PointLight.intensity = defaultNoFlickerIntensity;
                PointLight.transform.position = defaultPosition;
            }
        }
    }

    private float RandomTranslationOneAxis()
    {
        return Random.Range(-FlickerTranslationRange, FlickerTranslationRange);
    }
}
