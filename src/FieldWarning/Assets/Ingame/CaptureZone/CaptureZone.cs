﻿/**
 * Copyright (c) 2017-present, PFW Contributors.
 *
 * Licensed under the Apache License, Version 2.0 (the "License"); you may not use this file except in
 * compliance with the License. You may obtain a copy of the License at
 *
 * http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software distributed under the License is
 * distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied. See
 * the License for the specific language governing permissions and limitations under the License.
 */

using PFW.Model.Game;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace PFW.Ingame.UI
{
    public class CaptureZone : MonoBehaviour
    {
        public Material Red;
        public Material Blue;
        public Material Neutral;
        //How many Points per tick this Zone gives
        public int Worth = 3;

        //Maybe take out all that owner stuff and simply use an int or otherwise shorten the code
        private PlayerData _owner;
        //Vehicles Currently in the Zone (Maybe exclude all non-commander vehicles),maybe replace list with Array
        private List<VehicleBehaviour> _vehicles = new List<VehicleBehaviour>();

        // Start is called before the first frame update
        void Start()
        {
        }

        // Update is called once per frame
        void Update()
        {
            //Check if Blue Red None or Both Occupy the Zone
            bool redIncluded = false;
            bool blueIncluded = false;
            PlayerData newOwner = null;
            for (int i = 0; i < _vehicles.Count; i++) {
                VehicleBehaviour vehicle = _vehicles.ToArray()[i];
                if (vehicle.AreOrdersComplete()) {
                    newOwner = vehicle.Platoon.Owner;
                    //Names are USSR and NATO
                    if (newOwner.Team.Name == "USSR") {
                        redIncluded = true;
                    } else {
                        blueIncluded = true;
                    }
                }
            }
            if (redIncluded && blueIncluded || (!redIncluded && !blueIncluded)) {
                if (_owner != null) {
                    changeOwner(null);
                }
            } else if (redIncluded) {
                if (_owner != newOwner) {
                    changeOwner(newOwner);
                }
            } else {
                if (_owner != newOwner) {
                    changeOwner(newOwner);
                }
            }
        }

        //needs to play sound
        private void changeOwner(PlayerData newOwner)
        {
            if (_owner != null) {
                _owner.IncomeTick -= Worth;
            }
            if (newOwner != null) {
                newOwner.IncomeTick += Worth;
            }
            _owner = newOwner;
            if (_owner != null) {
                if (_owner.Team.Name == "USSR") {
                    this.GetComponent<MeshRenderer>().material = Red;
                } else {
                    this.GetComponent<MeshRenderer>().material = Blue;
                }
            } else {
                this.GetComponent<MeshRenderer>().material = Neutral;
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.transform.parent != null && other.transform.parent.GetComponent<VehicleBehaviour>() != null && other.transform.parent.GetComponent<VehicleBehaviour>().isActiveAndEnabled) {
                _vehicles.Add(other.transform.parent.GetComponent<VehicleBehaviour>());
            }
        }

        // TODO: If the unit is killed, it will never be removed from the zone:
        private void OnTriggerExit(Collider other)
        {
            if (other.transform.parent.GetComponent<VehicleBehaviour>() != null) {
                _vehicles.Remove(other.transform.parent.GetComponent<VehicleBehaviour>());
            }
        }
    }
}
