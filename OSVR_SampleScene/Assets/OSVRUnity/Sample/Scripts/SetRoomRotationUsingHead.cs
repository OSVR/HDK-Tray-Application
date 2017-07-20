/// OSVR-Unity
///
/// http://sensics.com/osvr
///
/// <copyright>
/// Copyright 2016 Sensics, Inc.
///
/// Licensed under the Apache License, Version 2.0 (the "License");
/// you may not use this file except in compliance with the License.
/// You may obtain a copy of the License at
///
///     http://www.apache.org/licenses/LICENSE-2.0
///
/// Unless required by applicable law or agreed to in writing, software
/// distributed under the License is distributed on an "AS IS" BASIS,
/// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
/// See the License for the specific language governing permissions and
/// limitations under the License.
/// </copyright>

// Copyright 2016 Razer, Inc.
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

using System;
using UnityEngine;

namespace OSVR
{
    namespace Unity
    {
        public class SetRoomRotationUsingHead : MonoBehaviour
        {
            private ClientKit _clientKit;
            private DisplayController _displayController;

            private enum RecenterState { Initial, Automatic, User }
            private RecenterState _recenterState = RecenterState.Initial;

            public GameObject FollowHelpText, World, PoseSource;
            public float FollowHelpTextDistance = 6.5f, FollowHelpTextHeightOffset = -0.25f, DefaultHeight = 1.4f;

            void Awake()
            {
                _clientKit = ClientKit.instance;
                _displayController = FindObjectOfType<DisplayController>();
            }

            void Start()
            {
                World.SetActive(false); // Hide world and show splash screen until user presses space to recenter
            }

            void Update()
            {
                switch (_recenterState)
                {
                    // Run recenter once as soon as RenderManager is available
                    case RecenterState.Initial:
                        // Check if rendering is setup
                        if (_displayController.RenderManager == null || _displayController.transform.Find("VREye1/VRSurface0") == null)
                            break;

                        // Run recenter once (this code isn't helpful now but will be if we remove the "press space to continue" feature)
                        try
                        {
                            Recenter();
                        }
                        catch (Exception e)
                        {
                            Debug.LogError("Unable to set room rotation at launch:\n\tMessage: " + e.Message + "\nTrace:\n" + e.StackTrace);
                        }

                        _recenterState = RecenterState.Automatic;

                        // Show colored background rather than skybox
                        foreach (Camera cam in _displayController.GetComponentsInChildren<Camera>())
                            cam.clearFlags = CameraClearFlags.Color;

                        break;


                    // Help text follows look until user recenters once manually
                    case RecenterState.Automatic:
                        // Update help text pose
                        FollowHelpText.transform.position = PoseSource.transform.position +
                                                            PoseSource.transform.forward.normalized * FollowHelpTextDistance +
                                                            PoseSource.transform.up.normalized * FollowHelpTextHeightOffset;
                        FollowHelpText.transform.rotation = Quaternion.LookRotation(PoseSource.transform.forward, PoseSource.transform.up);

                        // Once user recenters, hide follow help text and show world
                        if (CheckUserRecentered())
                        {
                            _recenterState = RecenterState.User;

                            FollowHelpText.SetActive(false);
                            World.SetActive(true);

                            foreach (Camera cam in _displayController.GetComponentsInChildren<Camera>())
                                cam.clearFlags = CameraClearFlags.Skybox;
                        }
                        break;


                    // Process further recenter requests
                    case RecenterState.User:
                        CheckUserRecentered();
                        break;
                }
            }

            private bool CheckUserRecentered()
            {
                if (Input.GetButtonDown("Recenter Look Rotation"))
                {
                    Recenter();
                    return true;
                }

                return false;
            }

            private void Recenter()
            {
                if (_displayController != null && _displayController.UseRenderManager)
                    _displayController.RenderManager.SetRoomRotationUsingHead();
                else
                    _clientKit.context.SetRoomRotationUsingHead();
                
                Vector3 translated_origin = -PoseSource.transform.position;
                translated_origin.y = DefaultHeight;
                PoseSource.transform.root.position = translated_origin;
            }
        }
    }
}