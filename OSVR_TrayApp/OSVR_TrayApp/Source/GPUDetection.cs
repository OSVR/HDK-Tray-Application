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

using System.Management;

namespace HDK_TrayApp
{
    public class GPUDetection
    {
        public enum GraphicsCardType { NVIDIA, AMD, UNKNOWN };

        public static GraphicsCardType Detect()
        {
            ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT * FROM Win32_VideoController");

            foreach (ManagementObject mo in searcher.Get())
            {
                foreach (PropertyData property in mo.Properties)
                {
                    if (property.Name == "AdapterCompatibility")
                    {
                        switch (property.Value.ToString())
                        {
                            case "NVIDIA":
                                return GraphicsCardType.NVIDIA;

                            case "Advanced Micro Devices, Inc.":
                                return GraphicsCardType.AMD;

                            case "Intel Corporation":
                            default:
                                break;
                        }
                    }
                }
            }

            return GraphicsCardType.UNKNOWN;
        }
    }
}
