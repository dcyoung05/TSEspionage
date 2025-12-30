/*
 * This Source Code Form is subject to the terms of the Mozilla Public
 * License, v. 2.0. If a copy of the MPL was not distributed with this
 * file, You can obtain one at https://mozilla.org/MPL/2.0/.
 */

using GameData;
using UnityEngine;
using UnityEngine.UI;
using System;

namespace TSEspionage
{
    /**
     * Activates and deactivates an additional region control bar that shows the scoring for the region in final
     * scoring. It is only active when Shuttle Diplomacy is in effect.
     */
    public class RegionControlBar : MonoBehaviour
    {
        private const float InfluenceBarHeight = 50;
        private const float InfluenceBarPadding = 2;

        private GameObject _controlBar;
        private Transform _regionSummary;
        private Image _imageUssr;
        private Image _imageUsa;
        private float _animateTime;

        private ushort _usaScore;
        private ushort _ussrScore;

        public RegionControlBar(IntPtr ptr) : base(ptr) {}

        public void Init(GameObject controlBar, float mAnimateTime)
        {
            _controlBar = controlBar;
            _controlBar.SetActive(false);
            _regionSummary = controlBar.transform.parent;
            _imageUsa = controlBar.transform.Find("Influence_US01").GetComponent<Image>();
            _imageUssr = controlBar.transform.Find("Influence_USSR01").GetComponent<Image>();
            _animateTime = mAnimateTime;
        }

        public void HandleRegionScore(GameFinalRegionScoreState regionScore)
        {
            if (regionScore.shuttle_diplomacy_in_play > 0 && !_controlBar.activeInHierarchy)
            {
                // Activate the additional region control bar and grow the parent
                _controlBar.SetActive(true);
                ResizeRegionSummary(InfluenceBarHeight, InfluenceBarPadding);
            }

            if (regionScore.shuttle_diplomacy_in_play < 1 && _controlBar.activeInHierarchy)
            {
                // Deactivate the additional region control bar and shrink the parent
                _controlBar.SetActive(false);
                ResizeRegionSummary(-InfluenceBarHeight, -InfluenceBarPadding);
            }

            if (_controlBar.activeInHierarchy)
            {
                UpdateControlBar(regionScore);
            }
        }

        private void ResizeRegionSummary(float height, float padding)
        {
            var summaryRectTransform = _regionSummary.GetComponent<RectTransform>();
            var sizeDelta = summaryRectTransform.sizeDelta;
            summaryRectTransform.sizeDelta = new Vector2(sizeDelta.x, sizeDelta.y + height + padding);

            var button = _regionSummary.Find("Button");

            var influenceRectTransform = _regionSummary.transform.Find("Influence").GetComponent<RectTransform>();
            var influenceDelta = influenceRectTransform.sizeDelta;
            influenceRectTransform.sizeDelta = new Vector2(influenceDelta.x, influenceDelta.y - height);
            var influencePosition = influenceRectTransform.anchoredPosition;
            influenceRectTransform.anchoredPosition = new Vector2(
                influencePosition.x,
                influencePosition.y + height / 2.0f);
        }

        private void UpdateControlBar(GameFinalRegionScoreState regionScore)
        {
            if (_usaScore != regionScore.player_score_state_usa
                || _ussrScore != regionScore.player_score_state_ussr)
            {
                var sp = new ScorePanel();
                StartCoroutine(sp.AnimateBar(_imageUsa,
                    _imageUsa.fillAmount,
                    regionScore.player_score_state_usa * 0.25f,
                    _animateTime));
                StartCoroutine(sp.AnimateBar(_imageUssr,
                    _imageUssr.fillAmount,
                    regionScore.player_score_state_ussr * 0.25f,
                    _animateTime));
            }

            _usaScore = regionScore.player_score_state_usa;
            _ussrScore = regionScore.player_score_state_ussr;
        }
    }
}
