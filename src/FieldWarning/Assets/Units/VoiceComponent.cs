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

using UnityEngine;

namespace PFW.Units
{
    public class VoiceComponent : MonoBehaviour
    {
        [SerializeField]
        private AudioClip _selectAudio, _moveAudio, _attackAudio;
        [SerializeField]
        private AudioSource _audioSource;

        //If we want to use AddComponent, than we should get rid of it at the end of the method with Object.Destroy()
        public void PlayUnitSelectionVoiceline(bool selected)
        {
            _audioSource.clip = _selectAudio;
            if (selected) {
                _audioSource.Play();
            }
        }

        public void PlayMoveCommandVoiceline()
        {
            _audioSource.clip = _moveAudio;
            _audioSource.Play();
        }

        public void PlayAttackCommandVoiceline()
        {
            _audioSource.clip = _attackAudio;
            _audioSource.Play();
        }
    }
}