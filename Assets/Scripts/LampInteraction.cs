using UnityEngine;

public class LampInteraction : MonoBehaviour
{
    public Light lampLight; // 连接到灯的 Light 组件
    public KeyCode toggleKey = KeyCode.E; // 用于开关灯的键

    private bool isOn = false; // 追踪灯的状态

    void Start()
    {
        if (lampLight == null)
        {
            Debug.LogError("No light component assigned to LampInteraction script.");
        }
        else
        {
            lampLight.enabled = isOn; // 初始化灯的状态
        }
    }

    void Update()
    {
        // 检查玩家是否按下了指定的键
        if (Input.GetKeyDown(toggleKey))
        {
            ToggleLamp(); // 切换灯的开关状态
        }
    }

    void ToggleLamp()
    {
        isOn = !isOn; // 切换灯的状态
        lampLight.enabled = isOn; // 设置灯的开启或关闭
    }
}
