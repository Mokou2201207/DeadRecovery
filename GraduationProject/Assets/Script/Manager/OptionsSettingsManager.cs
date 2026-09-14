using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// 操作設定などの処理
/// </summary>
public class OptionsSettingsManager : MonoBehaviour
{
    [Header("UIパーツの割り当て")]
    [Header("マウス感度Xのスライダー"), SerializeField]
    private Slider mouseSensitivityXSlider;
    [Header("マウス感度Yのスライダー"), SerializeField]
    private Slider mouseSensitivityYSlider;
    [Header("BGMのボリュームのスライダー")]
    [SerializeField] private Slider bgmVolumeSlider;

    [Header("手振れボタンON,OFFのボタン")]
    [SerializeField] private Button headBobOnButton;
    [SerializeField] private Button headBobOffButton;

    [Header("連携するscript")]
    [Header("MouseLookのscript※自動"), SerializeField]
    private MouseLook mouseLookScript;

    [Header("選択時の色設定")]
    [SerializeField] private Color selectedColor = Color.green;
    [SerializeField] private Color normalColor = Color.white;

    [Header("BGM設定")]
    [Header("AudioSource")]
    [SerializeField] private AudioSource bgmAudioSource;
    [Header("曲")]
    [SerializeField] private AudioClip musicClip1;
    [SerializeField] private AudioClip musicClip2;
    [Header("BGMの名前")]
    [SerializeField] private Text bgmText;
    [Header("曲の選択ボタン")]
    [SerializeField] private Button prevBgmButton;
    [SerializeField] private Button nextBgmButton;


    private int currentBgmIndex = 0;

    private void Start()
    {
        //mouseLookScriptがなければTagで取得
        if (mouseLookScript == null)
        {
            GameObject player = GameObject.FindWithTag("MainCamera");
            if (player != null)
            {
                mouseLookScript = player.GetComponent<MouseLook>();

                if (mouseLookScript != null)
                {
                    float currentSensX = mouseLookScript.GetSensitivityX();
                    float currentSensY = mouseLookScript.GetSensitivityY();
                }
            }
        }

        InitializeHeadBobDefault();

        //X軸用
        if (mouseSensitivityXSlider != null && mouseLookScript != null)
        {
            //スライダーの初期値を、MouseLookの現在の感動に合わせる
            mouseSensitivityXSlider.value = mouseLookScript.GetSensitivityX();

            //スライダーが動かされたときに呼ばれる関数を登録
            mouseSensitivityXSlider.onValueChanged.AddListener(OnMouseSensitivityXChanged);
        }

        //Y軸用
        if (mouseSensitivityYSlider != null && mouseLookScript != null)
        {
            //スライダーの初期値を、MouseLookの現在の感動に合わせる
            mouseSensitivityYSlider.value = mouseLookScript.GetSensitivityY();

            //スライダーが動かされたときに呼ばれる関数を登録
            mouseSensitivityYSlider.onValueChanged.AddListener(OnMouseSensitivityYChanged);
        }

        // BGM切り替えボタンのイベント登録
        if (prevBgmButton != null)
        {
            prevBgmButton.onClick.AddListener(OnPrevBgmClicked);
        }
        if (nextBgmButton != null)
        {
            nextBgmButton.onClick.AddListener(OnNextBgmClicked);
        }

        // BGM音量スライダーのイベント登録
        if (bgmVolumeSlider != null && bgmAudioSource != null)
        {
            bgmVolumeSlider.value = bgmAudioSource.volume;
            bgmVolumeSlider.onValueChanged.AddListener(OnBgmVolumeChanged);
        }

        // 初期状態のBGMを適用
        UpdateBGMState();
    }

    /// <summary>
    /// 最初はONにしておく初期化処理
    /// </summary>
    private void InitializeHeadBobDefault()
    {
        if (mouseLookScript != null)
        {
            mouseLookScript.SetHeadBobActive(true);
        }
        // 最初はONボタンを緑に、OFFを白にする
        SetButtonColor(headBobOnButton, selectedColor);
        SetButtonColor(headBobOffButton, normalColor);
    }

    /// <summary>
    /// 感度X軸を置き換える処理
    /// </summary>
    /// <param name="sliderValue"></param>
    public void OnMouseSensitivityXChanged(float sliderValue)
    {
        //mouseLookScriptがあれば感度を置き換える
        if (mouseLookScript != null)
        {
            mouseLookScript.SetSensitivityX(sliderValue);
        }
    }

    /// <summary>
    /// 感度Y軸を置き換える処理
    /// </summary>
    /// <param name="sliderValue"></param>
    public void OnMouseSensitivityYChanged(float sliderValue)
    {
        //mouseLookScriptがあれば感度を置き換える
        if (mouseLookScript != null)
        {
            mouseLookScript.SetSensitivityY(sliderValue);
        }
    }

    public void RegisterMouseLook(MouseLook lookScript)
    {
        mouseLookScript = lookScript;

        // スライダーの初期値設定などをここで行う（X軸）
        if (mouseSensitivityXSlider != null && mouseLookScript != null)
        {
            mouseSensitivityXSlider.value = mouseLookScript.GetSensitivityX();
            mouseSensitivityXSlider.onValueChanged.AddListener(OnMouseSensitivityXChanged);
        }

        // スライダーの初期値設定などをここで行う（Y軸）
        if (mouseSensitivityYSlider != null && mouseLookScript != null)
        {
            mouseSensitivityYSlider.value = mouseLookScript.GetSensitivityY();
            mouseSensitivityYSlider.onValueChanged.AddListener(OnMouseSensitivityYChanged);
        }

        // スポーン時も手ブレをON状態に合わせる
        InitializeHeadBobDefault();
    }

    /// <summary>
    /// 手振れボタンのONを押した時
    /// </summary>
    public void OnHeadBobOnClicked()
    {
        //揺れをオンに
        if (mouseLookScript != null)
        {
            mouseLookScript.SetHeadBobActive(true);
        }

        //ボタンの色を切り替える
        SetButtonColor(headBobOnButton, selectedColor);
        SetButtonColor(headBobOffButton, normalColor);
    }

    /// <summary>
    /// 手振れボタンのOFFを押した時
    /// </summary>
    public void OFFHeadBobOnClicked()
    {
        //揺れをオフに
        if (mouseLookScript != null)
        {
            mouseLookScript.SetHeadBobActive(false);
        }

        //ボタンの色を切り替える
        SetButtonColor(headBobOffButton, selectedColor); // OFFを緑に
        SetButtonColor(headBobOnButton, normalColor);
    }

    /// <summary>
    /// ボタンのImageの色を変更する補助関数
    /// </summary>
    private void SetButtonColor(Button targetButton, Color color)
    {
        if (targetButton != null)
        {
            Image img = targetButton.GetComponent<Image>();
            if (img != null)
            {
                img.color = color;
            }
        }
    }

    /// <summary>
    /// 前へのボタン処理
    /// </summary>
    public void OnPrevBgmClicked()
    {
        currentBgmIndex = (currentBgmIndex - 1 + 3) % 3;
        UpdateBGMState();
    }

    /// <summary>
    /// 次へのボタン処理
    /// </summary>
    public void OnNextBgmClicked()
    {
        currentBgmIndex = (currentBgmIndex + 1) % 3;
        UpdateBGMState();
    }

    /// <summary>
    /// 曲の切り替えとBGMを流す処理
    /// </summary>
    private void UpdateBGMState()
    {
        if (bgmAudioSource == null) return;

        // テキストをプログラム側で動的に設定
        if (bgmText != null)
        {
            if (currentBgmIndex == 0)
                bgmText.text = musicClip1 != null ? musicClip1.name : "環境音";
            else if (currentBgmIndex == 1)
                bgmText.text = musicClip2 != null ? musicClip2.name : "カフェBGM";
            else
                bgmText.text = "無音";
        }

        // BGMの再生/停止処理
        if (currentBgmIndex == 2)
        {
            bgmAudioSource.Stop();
        }
        else
        {
            AudioClip targetClip = (currentBgmIndex == 0) ? musicClip1 : musicClip2;
            if (targetClip != null)
            {
                if (bgmAudioSource.clip != targetClip || !bgmAudioSource.isPlaying)
                {
                    bgmAudioSource.clip = targetClip;
                    bgmAudioSource.Play();
                }
            }
            else
            {
                bgmAudioSource.Stop();
            }
        }
    }

    /// <summary>
    /// ボリュームをスライダーに対応
    /// </summary>
    /// <param name="value"></param>
    public void OnBgmVolumeChanged(float value)
    {
        if (bgmAudioSource != null)
        {
            bgmAudioSource.volume = value;
        }
    }
}
